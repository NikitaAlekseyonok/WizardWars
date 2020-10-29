using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSingle : MonoBehaviourPun
{
    public float speed = 4f;
    public float destroyTime = 2f;

    public void destroyBullet()
    {
        Destroy(gameObject);
    }

    public void Start()
    {
        Invoke("destroyBullet", destroyTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

}

