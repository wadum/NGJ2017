using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class playerAmountScript : Photon.MonoBehaviour
{

    public Button StartButton;
    Text textField;

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
        string PlayerAmount = PhotonNetwork.playerList.Length + "";
        photonView.RPC("ChangeNumberClients", PhotonTargets.All, PlayerAmount);
    }

    [PunRPC]
    public void ChangeNumberClients(string PlayerAmount)
    {
        textField.text = PlayerAmount;
		if (PhotonNetwork.player.ID == 1 && PhotonNetwork.playerList.Length > 1)
		{
			StartButton.interactable = true;
		}
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
