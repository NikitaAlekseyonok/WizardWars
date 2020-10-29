using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage2 : MonoBehaviourPun
{
    public static int HP3 = 3;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(HP3);
        if (other.gameObject.tag.Equals("Bullet1"))
        {
            GetComponent<PhotonView>().RPC("damage", RpcTarget.AllBuffered);
        }
    }


    [PunRPC]
    public void damage()
    {
        HP3--;
    }
}

