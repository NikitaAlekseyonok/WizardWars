using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GManager : MonoBehaviourPunCallbacks
{

    public GameObject PlayerPrefab1;
    public GameObject PlayerPrefab2;
    public GameObject PlayerPrefab3;

    public GameObject Spawn1;
    public GameObject Spawn2;
    public GameObject Spawn3;

    public void FixedUpdate()
    {
        var PlayerList = PhotonNetwork.PlayerList;

        if (Damage.HP1 <= 0 && Damage2.HP3 <= 0)
        {
            Invoke("Leave", 5.0f);
            GameObject.Find("Canvas/Text").GetComponent<Text>().text = "     Game over!\n" + PlayerList[1].NickName+  " won!";
        }

        if (Damage.HP1 <= 0 && Damage1.HP2 <= 0)
        {
            Invoke("Leave", 5.0f);
            GameObject.Find("Canvas/Text").GetComponent<Text>().text = "     Game over!\n" + PlayerList[2].NickName + " won!";
        }

        if (Damage2.HP3 <= 0 && Damage1.HP2 <= 0)
        {
            Invoke("Leave", 5.0f);
            GameObject.Find("Canvas/Text").GetComponent<Text>().text = "     Game over!\n" + PlayerList[0].NickName + " won!";
        }
    }

    public void Update()
    {
        var PlayerList = PhotonNetwork.PlayerList;
        string str = "" ;

        if (PhotonNetwork.PlayerList.Length == 1)
            str = PlayerList[0].NickName + "  " + Damage.HP1 + "\n";
        else if (PhotonNetwork.PlayerList.Length == 2)
            str = PlayerList[0].NickName + "  " + Damage.HP1 + "\n" + PlayerList[1].NickName + "  " + Damage1.HP2;
        else if (PhotonNetwork.CountOfPlayersInRooms == 3)
            str = PlayerList[0].NickName + "  " + Damage.HP1 + "\n" + PlayerList[1].NickName + "  " + Damage1.HP2 + "\n" + PlayerList[1].NickName + "  " + Damage2.HP3;

        GameObject.Find("Canvas/Hp").GetComponent<Text>().text = str;
    }

    public void Leave()
    {
        Damage.HP1 = 3;
        Damage1.HP2 = 3;
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }

    public void Start()
    {
        if (PhotonNetwork.PlayerList.Length == 1)
        {
            PhotonNetwork.Instantiate(PlayerPrefab1.name, Spawn1.transform.position, Quaternion.identity);
        }
        else if (PhotonNetwork.PlayerList.Length  == 2)
        {
            PhotonNetwork.Instantiate(PlayerPrefab2.name, Spawn2.transform.position, Quaternion.identity);
        }
        else if (PhotonNetwork.PlayerList.Length == 3)
        {
            PhotonNetwork.Instantiate(PlayerPrefab3.name, Spawn3.transform.position, Quaternion.identity);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player {0} enterd room" + newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log("Player {0} left room" + otherPlayer.NickName);
    }
}
