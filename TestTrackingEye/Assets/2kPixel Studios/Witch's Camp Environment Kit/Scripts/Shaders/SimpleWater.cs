using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelEngine {
	public class SimpleWater : MonoBehaviour {

		public float waveSpeed = 0.015f;
		public float waveScale = 0.20f;

		private new Renderer renderer;
		private Material waterMaterial;

		private float materialWaveScale = 0;

		public void Start() {
			renderer = GetComponent<Renderer>();
			waterMaterial = renderer.material;
		}

		public void Update() {
			waterMaterial.SetFloat("_WaveOffset", waveSpeed * Time.time);
			if (materialWaveScale != waveScale) {
				waterMaterial.SetFloat("_WaveScale", 1 / waveScale);
				materialWaveScale = waveScale;
			}
		}
	}
}