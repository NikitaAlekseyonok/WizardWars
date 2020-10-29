using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GManagerSingle : MonoBehaviour
{
    public GameObject Player;
    public GameObject Bot;

    public GameObject Spawn1;
    public GameObject Spawn2;

    void Start()
    {
        Instantiate(Player, Spawn1.transform.position, Quaternion.identity);
        Instantiate(Bot, Spawn2.transform.position, Quaternion.identity);
    }

    public void Update()
    {
        GameObject.Find("Canvas/Hp").GetComponent<Text>().text = "Lives " + DamageSingle.HP1;

        if (DamageSingle.HP1 <= 0)
        {
            GameObject.Find("Canvas/Text").GetComponent<Text>().text = "Game over!\nYou lose!";
        }

        var Bots = GameObject.FindGameObjectsWithTag("BotWizard");

        if (Bots.Length == 0)
        {
            GameObject.Find("Canvas/Text").GetComponent<Text>().text = "Game Over!\nYou Won!";
        }
    } 

    public void LiveRoom()
    {
        DamageSingle1.HP2 = 3;
        DamageSingle.HP1 = 7;
        SceneManager.LoadScene("Menu");
    }
}
