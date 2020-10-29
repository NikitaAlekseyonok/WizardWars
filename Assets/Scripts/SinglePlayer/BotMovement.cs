using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    public static float move;
    public float movementSpeed = 0.6f;
    public float moveSpeed;

    Rigidbody2D rbody;
    private Animator animator;

    public Vector2 MovementDirection;
    public Vector2 ForShoot;
    public static Vector2 shootingDirecthion;

    public float Croas_Firehair = 1.5f;
    public static bool endOfAim;
    public bool Stop = false;

    public GameObject firehair;
    public GameObject bullet;

    public float Bullet_Speed = 4f;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rbody = GetComponentInChildren<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Anim();
        Aim();
    }

    public void EndOfAim()
    {
        endOfAim = true;
    }

    void Move()
    {
            var x = Mathf.Abs(PlayerMovementSingle.PlayerPosition.x - transform.position.x);
            var y = Mathf.Abs(PlayerMovementSingle.PlayerPosition.y - transform.position.y);

            if ((x <= 3 && y <= 3) || (x >= 3 && y >= 3))
            {
                Stop = true;
                MovementDirection = PlayerMovementSingle.PlayerPosition - transform.position;
                InvokeRepeating("EndOfAim", 1f, 1.5f);
                Shoot();
            }
            else
            {
                Stop = false;
                MovementDirection = PlayerMovementSingle.PlayerPosition - transform.position;
                moveSpeed = Mathf.Clamp(MovementDirection.magnitude, 0.0f, 1.0f);
                transform.position = Vector2.MoveTowards(transform.position, PlayerMovementSingle.PlayerPosition, Time.deltaTime * movementSpeed);
            }
    }

    void Anim()
    {
        if (Stop == true)
        {
            animator.SetFloat("Horizontal", MovementDirection.x);
            animator.SetFloat("Vertical", MovementDirection.y);
            animator.SetFloat("Speed", 0);
        }
        else 
           animator.SetFloat("Speed", moveSpeed);
    }

    void Aim()
    {

        if (MovementDirection != Vector2.zero) {
            firehair.transform.localPosition = MovementDirection * Croas_Firehair; 
        }

        if (MovementDirection.x > 0)
            ForShoot.x = 0.4f;
        else if (MovementDirection.x < 0)
            ForShoot.x = -0.4f;
        else
            ForShoot.x = 0f;

        if (MovementDirection.y > 0)
            ForShoot.y = 0.5f;
        else if (MovementDirection.y < 0)
            ForShoot.y = -0.6f;
        else
            ForShoot.y = 0f;
    }

    void Shoot()
    {
        shootingDirecthion = firehair.transform.localPosition;
        shootingDirecthion.Normalize();

        if (endOfAim)
        {
            var Bull = Instantiate(bullet, new Vector2(transform.position.x+ForShoot.x, transform.position.y + ForShoot.y), Quaternion.identity);
            Bull.GetComponent<Rigidbody2D>().velocity = shootingDirecthion * Bullet_Speed;
            Bull.transform.Rotate(0, 0, Mathf.Atan2(shootingDirecthion.y, shootingDirecthion.x) * Mathf.Rad2Deg);
            CancelInvoke();
            endOfAim = false;
        }
    }
}
