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
		photonView.RPC("ToggleColorRPC", PhotonTargets.All);
	}

	[PunRPC]
	public void ToggleColorRPC() {
		ColorBlock colors = button.colors;

		colors.highlightedColor = colors.normalColor = new Color((colors.normalColor.r + 1)%2, (colors.normalColor.g + 1)%2, colors.normalColor.b);

		button.colors = colors;
	}
}
