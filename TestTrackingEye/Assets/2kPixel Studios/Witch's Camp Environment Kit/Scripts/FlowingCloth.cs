using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cloth))] //C# Attribute
public class FlowingCloth : MonoBehaviour {
	[Header("Movement Curves")]
	public AnimationCurve xMovement;
	public AnimationCurve yMovement;
	public AnimationCurve zMovement;

	[Header("Acceleration & Frequency")]
	public float accelerationScale = 1;
	public Vector3 perAxisAcceleration = Vector3.one;

	public float frequencyScale = 1;
	public Vector3 perAxisFrequency = Vector3.one;


	private Cloth cloth;
	private Vector3 acceleration;

	private Vector3 currentTime = Vector3.zero;


	public void OnValidate() {
		CheckCurve(xMovement);
		CheckCurve(yMovement);
		CheckCurve(zMovement);
	}

	private void CheckCurve(AnimationCurve a) {
		if (a == null)
			return;
		Keyframe[] keys = a.keys;

		bool has0 = false;
		bool has1 = false;
		for (int i = 0; i < keys.Length; i++) {
			if (keys[i].time == 0)
				has0 = true;
			else if (keys[i].time == 1)
				has1 = true;
		}

		if (!has0)
			a.AddKey(0, 0);
		if (!has1)
			a.AddKey(1, 1);
	}

	public void Awake() {
		cloth = GetComponent<Cloth>();
	}

	public void Update() {
		acceleration.x = xMovement.Evaluate(currentTime.x);
		acceleration.y = yMovement.Evaluate(currentTime.y);
		acceleration.z = zMovement.Evaluate(currentTime.z);
		acceleration.Scale(perAxisAcceleration);
		acceleration *= accelerationScale;

		cloth.externalAcceleration = acceleration;

		UpdateGraphTimes();
	}

	private void UpdateGraphTimes() {
		for (int i = 0; i < 3; i++)
			currentTime[i] = (currentTime[i] + (frequencyScale * perAxisFrequency[i] * Time.deltaTime)) % 1f;
	}
}
