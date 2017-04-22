using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairSlider : MonoBehaviour {

	public CrosshairController crosshairObject;

	public void SetX(float value){
		crosshairObject.photonView.RPC("SetX", PhotonTargets.MasterClient, value);
	}

	public void SetY(float value){
		crosshairObject.photonView.RPC("SetY", PhotonTargets.MasterClient, value);
	}
}
