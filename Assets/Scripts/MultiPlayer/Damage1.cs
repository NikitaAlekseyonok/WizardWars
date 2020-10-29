using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage1 : MonoBehaviourPun
{
    public static int HP2 = 3;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(HP2);
        if (other.gameObject.tag.Equals("Bullet1"))
        {
            GetComponent<PhotonView>().RPC("damage", RpcTarget.AllBuffered);
        }
    }


    [PunRPC]
    public void damage()
    {
        HP2--;
    }
}

