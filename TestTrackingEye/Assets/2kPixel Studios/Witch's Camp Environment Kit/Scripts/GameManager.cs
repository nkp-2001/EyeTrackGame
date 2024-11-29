using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	//Singleton stuff
	private static GameManager instance;

	[SerializeField] private GameObject dontDestroy;

	public void Awake() {
		if (instance != null && instance != this) {
			DestroyImmediate(gameObject);
			return;
		}
		instance = this;

		DontDestroyOnLoad(dontDestroy);
	}
}
