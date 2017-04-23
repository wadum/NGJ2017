using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour {

	private Image image;
	private RectTransform rect;

	[Range(0,1)]
	public float FadeSpeed = 0.3f;

	[Range(0,1)]
	public float HiddenTime = 0.3f;

	[Range(0,100)]
	public float SecondsAlive = 2;

	private Vector3 startPos;
	private Vector3 endPos;
	private float startTime;

	public Color startColor = Color.green;
	public Color endColor = Color.red;

	void Awake() {
		this.image = GetComponent<Image>();
		this.rect = GetComponent<RectTransform>();
	}

	void OnEnable() {

		startPos = rect.localPosition;
		endPos = new Vector3(Random.Range(-525+40, 525-40),Random.Range(-350+40, 350-40),0);
		startTime = Time.time;

		StartCoroutine(FadeInOut());
	}

	void OnDisable() {
		StopAllCoroutines();
	}

	private static Vector3 lowerBound = new Vector3(-525+40,-350+40);
	private static Vector3 upperBound = new Vector3(525+40,350+40);
	public IEnumerator FadeInOut() {
		while(true){
			
			float alpha = image.color.a - FadeSpeed * Time.deltaTime;
			float life = (Time.time - startTime)/SecondsAlive;

			if (alpha < -HiddenTime) {
				Vector3 tmpPos = Vector3.Slerp(startPos, endPos, life);
				rect.localPosition = Vector3.Max(lowerBound, Vector3.Min(upperBound, tmpPos));
			}
			Color newColor = Color.Lerp(startColor, endColor, life);
			image.color = new Color(newColor.r, newColor.g, newColor.b, alpha < -HiddenTime ? 1 : alpha);
			yield return null;
		}
	}
}
