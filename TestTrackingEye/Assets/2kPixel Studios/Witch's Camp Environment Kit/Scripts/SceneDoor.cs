using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// SceneDoors come in pairs, and are linked together with their Ids
/// </summary>
public class SceneDoor : MonoBehaviour {
	private const string IsOpenParameter = "Is Open";

	[SerializeField] private uint id;
	[SerializeField] private string targetSceneName = "";
	[SerializeField] private Transform spawnPoint;

	private Animator animator;

	public void Awake() {
		animator = GetComponent<Animator>();
	}

	public void Start() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		//This is ugly for now :) <3
		SceneDoor temp = FindObjectOfType<SceneDoor>();
		FirstPersonCamera victim = Camera.main.GetComponentInParent<FirstPersonCamera>();

		victim.transform.position = temp.spawnPoint.position;
		victim.transform.rotation = temp.spawnPoint.rotation;
	}

	public void ChangeScenes() {
		if (targetSceneName == "") {
			Debug.LogWarning("The target scene name was not set -- unable to change scenes.");
			return;
		}

		StartCoroutine(ChangeSceneCoroutine());
	}

	private IEnumerator ChangeSceneCoroutine() {
		if (animator != null) {
			animator.SetBool(IsOpenParameter, true);
			yield return new WaitForSeconds(1.2f);
		}
		
		UIManager.ScreenFadeIn();

		yield return new WaitForSeconds(3);
		SceneManager.LoadScene(targetSceneName);
	}

	public void OnTriggerEnter(Collider other) {
		FirstPersonCamera player = other.GetComponentInChildren<FirstPersonCamera>();
		if (player != null) {
			ChangeScenes();
		}
	}
}
