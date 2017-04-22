using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ToggleButtonColor : Photon.MonoBehaviour {

	Button button;

	public void Start() {
		this.button = GetComponentInParent<Button>();
	}

	public void ToggleColor() {
		Debug.Log(PhotonNetwork.player.ID);
		photonView.RPC("ToggleColorMaster", PhotonTargets.MasterClient);
	}

	[PunRPC]
	public void ToggleColorMaster() {
		ColorBlock colors = button.colors;
		photonView.RPC("ToggleColorClients", PhotonTargets.All, (colors.normalColor.r + 1)%2, (colors.normalColor.g + 1)%2, colors.normalColor.b);
	}

	[PunRPC]
	public void ToggleColorClients(float r, float g, float b) {
		ColorBlock colors = button.colors;
		colors.highlightedColor = colors.normalColor = new Color(r,g,b);
		button.colors = colors;
	}
}
