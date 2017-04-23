using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class playerAmountScript : Photon.MonoBehaviour
{

    public Button StartButton;
    Text textField;

	public GameObject[] playerImages;

	public Sprite GreenColor;
	public Sprite RedColor;

    public void Start()
    {
        textField = GetComponent<Text>();

		if(PhotonNetwork.player.ID != 1) {
			StartButton.gameObject.SetActive(false);
		} else {
			StartButton.GetComponent<Button>().interactable = false;
		}
    }

    public void ChangeNumber()
    {
        photonView.RPC("ChangeNumberMaster", PhotonTargets.MasterClient);
    }

    [PunRPC]
    public void ChangeNumberMaster()
    {
		photonView.RPC("ChangeNumberClients", PhotonTargets.All, PhotonNetwork.playerList.Length);
    }

    [PunRPC]
    public void ChangeNumberClients(int PlayerAmount)
    {
 
		for (var i = 0; i < 3; i++) {
			playerImages[i].SetActive(i < PlayerAmount);
		}

		StartButton.interactable = PhotonNetwork.player.ID == 1 && PhotonNetwork.playerList.Length > 2;
    }

    public void OnJoinedRoom()
    {
		if(PhotonNetwork.player.ID == 1) {
			StartButton.gameObject.SetActive(true);
			StartButton.GetComponent<Button>().interactable = false;
		}
        ChangeNumber();
    }

	public void ClickStartButton() {
		GameObject.FindGameObjectWithTag("GameController")
			.GetComponent<GameState>()
			.photonView.RPC("StartGame", PhotonTargets.All);
	}
}
