using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonCamera : MonoBehaviour {
	private const string XZOrientationName = "XZ Orientation";

	[SerializeField] private new Camera camera;

	public float movementSpeed = 5;
	public float rotationSpeed = 120;

	public float playerHeight = 1.8f;

	public float rotationLimitX = 70;

	[Space(20)]
	public KeyCode moveLeftKey = KeyCode.A;
	public KeyCode moveRightKey = KeyCode.D;
	public KeyCode moveForwardKey = KeyCode.W;
	public KeyCode moveBackwardKey = KeyCode.S;

	[Space(20)]
	public KeyCode lookLeftKey = KeyCode.LeftArrow;
	public KeyCode lookRightKey = KeyCode.RightArrow;
	public KeyCode lookUpKey = KeyCode.UpArrow;
	public KeyCode lookDownKey = KeyCode.DownArrow;

	public float movementLerpSpeed = 5;
	public float turnLerpSpeed = 7;

	private float targetEulerAngleX; //Only affects the camera's x-rotation
	private float targetEulerAngleY; //Only affects the root transform's y-rotation

	private float horizontalRot, verticalRot;
	private float forwardBack;
	private float leftRight;

	private Transform xzOrientation;

	[Space(20)]
	public bool applyGravity = true;

	private CharacterController controller;

	/// <summary> The camera's instantaneous displacement in the current frame based on input, in world space. </summary>
	private Vector3 displacement;

	/// <summary> The camera's current instantaneous y-velocity in world space. </summary>
	private float vSpeed = 0;

	public void Start() {
		controller = GetComponent<CharacterController>();
		xzOrientation = transform.Find(XZOrientationName);
		if (xzOrientation == null) {
			xzOrientation = new GameObject(XZOrientationName).transform;
			xzOrientation.SetParent(camera.transform, false);
		}

		targetEulerAngleX = camera.transform.localEulerAngles.x;
		targetEulerAngleY = transform.eulerAngles.y;
	}

	public void Update() {
		horizontalRot = Axify(lookLeftKey, lookRightKey);
		verticalRot = Axify(lookDownKey, lookUpKey);

		forwardBack = Axify(moveBackwardKey, moveForwardKey);
		leftRight = Axify(moveLeftKey, moveRightKey);

		HandleRotation();

		Vector3 newDisplacement = new Vector3(leftRight, 0, forwardBack);
		newDisplacement.Normalize();
		newDisplacement *= movementSpeed * Time.deltaTime;
		newDisplacement = xzOrientation.TransformDirection(newDisplacement);

		displacement = displacement + movementLerpSpeed * Time.deltaTime * (newDisplacement - displacement);


		if (applyGravity) {
			if (controller.isGrounded)
				vSpeed = -1; //A slight downward "force" to keep the controller on the ground.
			else
				vSpeed += Physics.gravity.y * Time.deltaTime;
		} else {
			vSpeed = 0;
		}

		Vector3 xzEulerAngles = camera.transform.eulerAngles;
		xzEulerAngles.x = xzEulerAngles.z = 0;
		xzOrientation.eulerAngles = xzEulerAngles;

		controller.Move(displacement + vSpeed * Vector3.up * Time.deltaTime);
	}

	private void HandleRotation() {
		targetEulerAngleY = targetEulerAngleY + rotationSpeed * horizontalRot * Time.deltaTime;
		targetEulerAngleX = EnsureAngleIs0To360(targetEulerAngleX + rotationSpeed * -verticalRot * Time.deltaTime);
		//eulerAngles are going to be in range 0 to 360, so they won't ever be negative
		//clamps our x rotation properly
		if (targetEulerAngleX > rotationLimitX && targetEulerAngleX <= 180)
			targetEulerAngleX = rotationLimitX;
		else if (targetEulerAngleX > 180 && targetEulerAngleX < 360 - rotationLimitX)
			targetEulerAngleX = 360 - rotationLimitX;


		Vector3 rootEulerAngles = transform.eulerAngles;
		rootEulerAngles.y = targetEulerAngleY;
		Vector3 localCameraEulerAngles = camera.transform.localEulerAngles;
		localCameraEulerAngles.x = targetEulerAngleX;

		Quaternion currentRotation = transform.rotation;
		transform.rotation = Quaternion.Lerp(currentRotation, Quaternion.Euler(rootEulerAngles), turnLerpSpeed * Time.deltaTime);
		currentRotation = camera.transform.localRotation;
		camera.transform.localRotation = Quaternion.Lerp(currentRotation, Quaternion.Euler(localCameraEulerAngles), turnLerpSpeed * Time.deltaTime);
	}

	private float Axify(KeyCode negative, KeyCode positive) {
		float value = 0;
		if (Input.GetKey(positive))
			value++;
		if (Input.GetKey(negative))
			value--;
		return value;
	}

	#region Static Helper Functions:
	//Note that C# modulo operator "%" always has the sign of the dividend.
	//Pretend everything is positive, and take the sign of the dividend. Thus -7 % 360 = -7.
	//... Which is problematic, because I'd want -7 degrees to become 353 degrees.

	/// <summary>
	/// <para>Checks angle to make sure it is a positive number from 0 (inclusive) to 360 (exclusive)</para>
	/// </summary>
	public static float EnsureAngleIs0To360(float angle) {
		if (angle < 0)
			return 360 + (angle % 360);
		return (angle >= 360) ? angle % 360 : angle;
	}
	#endregion
}
