using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class ServerManagerScript : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        DontDestroyOnLoad(gameObject);
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
    }
    public void EnterRandomly()
    {
        PhotonNetwork.LoadLevel(1);
        PhotonNetwork.JoinRandomRoom();
    }
    public void BecomeAHost()
    {
        PhotonNetwork.LoadLevel(1);
        string roomName = Random.Range(0, 91239131).ToString();
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true },TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        InvokeRepeating("checkTheInfo", 0, 1f);
        GameObject myObject = PhotonNetwork.Instantiate("Player",Vector3.zero,Quaternion.identity,0,null);
        myObject.GetComponent<PhotonView>().Owner.NickName = PlayerPrefs.GetString("PlayerIDExist");

        if(PhotonNetwork.PlayerList.Length == 1)
        {
            myObject.gameObject.tag = "Player1";
        }
        else
        {
            myObject.gameObject.tag = "Player2";
        }
    }
    public override void OnLeftRoom()
    {
    } 
    public override void OnLeftLobby()
    {
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        InvokeRepeating("checkTheInfo", 0, 1f);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
    }
    void checkTheInfo()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            GameObject.FindWithTag("PlayerIsWaiting").SetActive(false);
            GameObject.FindWithTag("PlayerName1").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[0].NickName;
            GameObject.FindWithTag("PlayerName2").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[1].NickName;
            CancelInvoke("checkTheInfo");
        }
        else
        {
            GameObject.FindWithTag("PlayerIsWaiting").SetActive(true);
            GameObject.FindWithTag("PlayerName1").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[0].NickName;
            GameObject.FindWithTag("PlayerName2").GetComponent<TextMeshProUGUI>().text = "............";
        }
       
    }
}
