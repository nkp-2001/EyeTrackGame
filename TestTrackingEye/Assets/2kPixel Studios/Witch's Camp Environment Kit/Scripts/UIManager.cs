using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
	private const string ShowingParameter = "Showing";

	private static UIManager instance;

	private RectTransform screenCanvas;
	private Animator screenFader;

	public bool ScreenFaderIsShowing {
		get { return instance.screenFader.GetBool(ShowingParameter); }
	}

	public void Awake() {
		if (instance != null && instance != this) {
			DestroyImmediate(gameObject);
			return;
		}
		instance = this;

		screenCanvas = (RectTransform) GameObject.Find("Screen Canvas").transform;
		DontDestroyOnLoad(screenCanvas.gameObject);

		screenFader = screenCanvas.Find("Screen Fader").GetComponent<Animator>();
	}

	public void Start() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		if (ScreenFaderIsShowing)
			ScreenFadeOut();
	}

	public static void ScreenFadeIn() {
		instance.screenFader.SetBool(ShowingParameter, true);
	}

	public static void ScreenFadeOut() {
		instance.screenFader.SetBool(ShowingParameter, false);
	}
}
