using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairButton : MonoBehaviour {

	public float stepAmount = 10;

	public CrosshairController crosshairObject;

	public void MoveRight(){
		crosshairObject.photonView.RPC("MoveRight", PhotonTargets.MasterClient, stepAmount);
	}

	public void MoveLeft(){
		crosshairObject.photonView.RPC("MoveLeft", PhotonTargets.MasterClient, stepAmount);
	}

	public void MoveUp(){
		crosshairObject.photonView.RPC("MoveUp", PhotonTargets.MasterClient, stepAmount);
	}

	public void MoveDown(){
		crosshairObject.photonView.RPC("MoveDown", PhotonTargets.MasterClient, stepAmount);
	}
}
