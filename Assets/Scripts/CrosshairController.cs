using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : Photon.MonoBehaviour {

	private RectTransform rect;

	private float x {
		get {
			return rect.localPosition.x;
		}
		set {
			rect.localPosition = new Vector3(
				Mathf.Clamp(value, -525+40, 525-40), 
				rect.localPosition.y, 
				rect.localPosition.z);
		}
	}

	private float y {
		get {
			return rect.localPosition.y;
		}
		set {
			rect.localPosition = new Vector3(
				rect.localPosition.x, 
				Mathf.Clamp(value, -350+40, 350-40), 
				rect.localPosition.z);
		}
	}

	public void Start() {
		rect = GetComponent<RectTransform>();
	}

	[PunRPC]
	public void MoveUp(float amount) {
		y += amount;
	}

	[PunRPC]
	public void MoveDown(float amount) {
		y -= amount;
	}

	[PunRPC]
	public void MoveLeft(float amount) {
		x -= amount;
	}

	[PunRPC]
	public void MoveRight(float amount) {
		x += amount;
	}

	[PunRPC]
	public void SetX(float position) {
		x = (position*1800)-900;
	}
		
	[PunRPC]
	public void SetY(float position) {
		y = (position*900)-450;
	}

}
