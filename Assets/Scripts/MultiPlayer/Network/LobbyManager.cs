using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks

{
    public GameObject ButtonSoloMulti;
    public GameObject Name;
    public GameObject RoomName;

    public InputField IName;
    public InputField IRoomName;

    private bool Settings = false;

    public void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public void SinglePlayer()
    {
        SceneManager.LoadScene("SinglePlayer");
    }

    public void CreateRoom12()
    {
        var name = IName.text;

        if (name.Length == 0)
            name = "Player " + Random.Range(1, 100);

        PhotonNetwork.NickName = name;
        var roomname = IRoomName.text;

        if (roomname.Length == 0)
            roomname = "WizardWars";

        PhotonNetwork.JoinRoom(roomname);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        var name = IName.text;

        if (name.Length == 0)
            name = "Player " + Random.Range(1, 100);

        PhotonNetwork.NickName = name;
        var roomname = IRoomName.text;

        if (roomname.Length == 0)
            roomname = "WizardWars";
        
       base.OnJoinRoomFailed(returnCode, message);
       PhotonNetwork.CreateRoom(roomname, new Photon.Realtime.RoomOptions
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 3
        });
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public void NetworkСonfiguration()
    {
        if(Settings == false)
        {

            ButtonSoloMulti.SetActive(false);
            Name.SetActive(true);
            RoomName.SetActive(true);
        }

        if(Settings == true)
        {
            ButtonSoloMulti.SetActive(true);
            Name.SetActive(false);
            RoomName.SetActive(false);
        }

        Settings = !Settings;
    }
}
