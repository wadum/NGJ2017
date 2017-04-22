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
    }

    public void OnJoinedRoom()
    {
        ChangeNumber();

        if (PhotonNetwork.playerList.Length > 1)
        {
            StartButton.interactable = true;
        }
    }
}
