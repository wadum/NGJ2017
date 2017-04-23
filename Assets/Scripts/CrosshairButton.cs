using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairButton : MonoBehaviour {

	public float stepAmount = 10;

	public CrosshairController crosshairObject;

	public bool leftButtonPressed;

	public bool rightButtonPressed;

	public void Update() {
		if (leftButtonPressed) {
			MoveDown();
		}

		if (rightButtonPressed) {
			MoveUp();
		}
	}

	public void pressRightButton(){
		rightButtonPressed = true;
	}

	public void pressLeftButton(){
		leftButtonPressed = true;
	}

	public void releaseRightButton(){
		rightButtonPressed = false;
	}

	public void releaseLeftButton(){
		leftButtonPressed = false;
	}

	public void MoveRight(){
		crosshairObject.photonView.RPC("MoveRight", PhotonTargets.MasterClient, stepAmount*Time.deltaTime);
	}

	public void MoveLeft(){
		crosshairObject.photonView.RPC("MoveLeft", PhotonTargets.MasterClient, stepAmount*Time.deltaTime);
	}

	public void MoveUp(){
		crosshairObject.photonView.RPC("MoveUp", PhotonTargets.MasterClient, stepAmount);
	}

	public void MoveDown(){
		crosshairObject.photonView.RPC("MoveDown", PhotonTargets.MasterClient, stepAmount);
	}
}
