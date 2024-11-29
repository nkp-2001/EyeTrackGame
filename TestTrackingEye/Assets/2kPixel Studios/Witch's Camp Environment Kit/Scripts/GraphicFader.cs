using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Fades out a graphic after a given initial delay time in seconds.
/// </summary>
public class GraphicFader : MonoBehaviour {
	[SerializeField] private float initialDelay = 6;
	[SerializeField] private float fadeDuration = 1.5f;
	[SerializeField] private bool destroyAfterwards = false;

	private Graphic target;

	public void OnValidate() {
		initialDelay = Mathf.Max(0, initialDelay);
		fadeDuration = Mathf.Max(0, fadeDuration);
	}

	public void Awake() {
		target = GetComponent<Graphic>();
		if (target == null) {
			Debug.Log("The " + GetType().Name + " on " + name + " was unable to find a Graphic component on the same GameObject."
				+ "Consider adding one (for example, a Text or Image component )for the fade to work, or remove this component.");
			DestroyImmediate(gameObject);
			return;
		}

		StartCoroutine(FadeCoroutine());
	}

	private IEnumerator FadeCoroutine() {
		if (initialDelay > 0)
			yield return new WaitForSeconds(initialDelay);
		if (!isActiveAndEnabled)
			yield break;

		Color c = target.color;
		float startTime = Time.time;
		while (isActiveAndEnabled) {
			c.a = 1 - ((Time.time - startTime) / fadeDuration);
			target.color = c;

			yield return null;
		}

		c.a = 0;
		target.color = c;

		yield return null;
		if (!isActiveAndEnabled)
			yield break;

		if (destroyAfterwards)
			Destroy(target.gameObject);
	}
}
