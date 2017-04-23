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

	public IEnumerator FadeInOut() {
		while(true){
			
			float alpha = image.color.a - FadeSpeed * Time.deltaTime;
			if (alpha < -HiddenTime) {
				rect.localPosition = Vector3.Slerp(startPos, endPos, (Time.time - startTime)/SecondsAlive);
			}
			image.color = new Color(image.color.r, image.color.g, image.color.b, alpha < -HiddenTime ? 1 : alpha);
			yield return null;
		}
	}
}
