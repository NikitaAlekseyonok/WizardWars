using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviourPun
{
    public static float move;
    public float movementSpeed = 1f;
    public float moveSpeed;

    Rigidbody2D rbody;
    private Animator animator;
    private PhotonView _photonView;

    public Vector2 MovementDirection;
    public Vector2 ForShoot;
    public static Vector2 shootingDirecthion;

    public float Croas_Firehair = 1.5f;
    public static bool endOfAim;
    private bool takeShoot = true;

    public GameObject firehair;
    public GameObject bullet;

    public float Bullet_Speed = 4f;
    public static int Healse = 3;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rbody = GetComponentInChildren<Rigidbody2D>();
    }

    public void Start()
    {
        _photonView = GetComponentInChildren<PhotonView>();
    }

    public void TakeShoot()
    {
        takeShoot = true;
    }

    public void Update()
    {
        if (!_photonView.IsMine)
        {
            firehair.SetActive(false);
            return;
        }
      
        if (CnInputManager.GetButtonDown("Jump"))
            {
              if (takeShoot == true)
              {
                    endOfAim = true;
                    takeShoot = false;
                    Invoke("TakeShoot", 0.75f);
              }
        }
        else 
           endOfAim = false; 
        Move();
        Anim();
        Aim();
        Shoot();
    }


    public void Move()
    {
        MovementDirection = new Vector2(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"));

        if (CnInputManager.GetAxis("Horizontal") != 0 && CnInputManager.GetAxis("Vertical") != 0)
        {
            if (CnInputManager.GetAxis("Horizontal") > 0.5)
                ForShoot.x = 0.4f;
            else if (CnInputManager.GetAxis("Horizontal") < -0.5)
                ForShoot.x = -0.4f;
            else
                ForShoot.x = 0f;

            if (CnInputManager.GetAxis("Vertical") > 0.5)
                ForShoot.y = 0.5f;
            else if (CnInputManager.GetAxis("Vertical") < -0.5)
                ForShoot.y = -0.6f;
            else
                ForShoot.y = 0f;
        }

        moveSpeed = Mathf.Clamp(MovementDirection.magnitude, 0.0f, 1.0f);
        MovementDirection.Normalize();
        rbody.velocity = MovementDirection * moveSpeed * movementSpeed;
    }

    public void Anim()
    {
        if (MovementDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", MovementDirection.x);
            animator.SetFloat("Vertical", MovementDirection.y);
        }
        animator.SetFloat("Speed", moveSpeed);
    }

    public void Aim()
    {

        if (MovementDirection != Vector2.zero) {
            firehair.transform.localPosition = MovementDirection * Croas_Firehair; 
        }
    }

    public void Shoot()
    {
        shootingDirecthion = firehair.transform.localPosition;
        shootingDirecthion.Normalize();

        if (endOfAim)
        {
            var Bull = PhotonNetwork.Instantiate(bullet.name, 
               new Vector2( transform.position.x + ForShoot.x, transform.position.y + ForShoot.y), Quaternion.identity);
            Bull.GetComponent<Rigidbody2D>().velocity = shootingDirecthion * Bullet_Speed;
            Bull.transform.Rotate(0, 0, Mathf.Atan2(shootingDirecthion.y, shootingDirecthion.x) * Mathf.Rad2Deg);
        }
    }
}
