using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviourPun
{
    public float speed = 4f;
    public float destroyTime = 2f;

    public void destroyBullet()
    {
        this.GetComponent<PhotonView>().RPC("destroy", RpcTarget.AllBuffered);
    }

    public void Start()
    {
        Invoke("destroyBullet", destroyTime);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
           GetComponent<PhotonView>().RPC("destroy", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void destroy()
    {
        Destroy(gameObject);
    }
}

