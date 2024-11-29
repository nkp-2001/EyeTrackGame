using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathwayCamera : MonoBehaviour {
	[SerializeField] private Transform pathwayParent;
	[Tooltip("Once the camera reaches the last destination point, should it "
		+ "loop back to the first destination point and restart again?")]
	[SerializeField] private bool wrapAround;

	[Tooltip("Once the camera gets this close (in world units) to its current "
		+ "destination, it will move on to the next destination point.")]
	[SerializeField] private float distanceThreshold = 0.5f;
	[Tooltip("Roughly, the percentage of the way that the camera will turn to look at "
		+ "the destination point in one second, from its current forward looking direction.")]
	[SerializeField] private float rotationLerpSpeed = 0.8f;
	[SerializeField] private float movementSpeed = 2;

	private int currentIndex = 0;
	private Transform currentDestination;

	public void OnValidate() {
		distanceThreshold = Mathf.Max(0, distanceThreshold);

		rotationLerpSpeed = Mathf.Max(0, rotationLerpSpeed);
		movementSpeed = Mathf.Max(0, movementSpeed);
	}

	public void Awake() {
		currentDestination = pathwayParent.GetChild(0);
	}

	public void Update() {
		if (currentIndex >= 0 && currentIndex < pathwayParent.childCount) {
			Vector3 forward = transform.forward;
			Vector3 toDest = Vector3.Normalize(currentDestination.position - transform.position);
			Vector3 newForward = Vector3.Lerp(forward, toDest, rotationLerpSpeed * Time.deltaTime);

			transform.forward = newForward;
			transform.position += movementSpeed * forward * Time.deltaTime;

			if (Vector3.SqrMagnitude(currentDestination.position - transform.position) < distanceThreshold * distanceThreshold)
				SetNextDestinationPoint();
		}
	}

	private void SetNextDestinationPoint() {
		currentIndex++;

		if (currentIndex >= pathwayParent.childCount) {
			if (wrapAround)
				currentIndex = 0;
			else
				return;
		}

		currentDestination = pathwayParent.GetChild(currentIndex);
	}
}
