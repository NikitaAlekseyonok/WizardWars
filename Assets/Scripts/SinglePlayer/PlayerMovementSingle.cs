using UnityEngine;
using CnControls;


public class PlayerMovementSingle : MonoBehaviour
{
    public static float move;
    public float movementSpeed = 1f;
    public float moveSpeed;

    Rigidbody2D rbody;
    private Animator animator;

    public Vector2 MovementDirection;
    public Vector2 ForShoot;
    public static Vector2 shootingDirecthion;
    public static Vector3 PlayerPosition;

    public float Croas_Firehair = 1.5f;
    public static bool endOfAim;

    public GameObject firehair;
    public GameObject bullet;

    public float Bullet_Speed = 4f;
    private bool takeShoot = true;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rbody = GetComponentInChildren<Rigidbody2D>();
    }

    public void TakeShoot()
    {
        takeShoot = true;
    }

    void Update()
    {
        if (DamageSingle.HP1 <= 0)
            return;

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
        PlayerPosition = transform.position;
    }


    void Move()
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

    void Anim()
    {
        if (MovementDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", MovementDirection.x);
            animator.SetFloat("Vertical", MovementDirection.y);
        }
        animator.SetFloat("Speed", moveSpeed);
    }

    void Aim()
    {

        if (MovementDirection != Vector2.zero) {
            firehair.transform.localPosition = MovementDirection * Croas_Firehair; 
        }
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
        }
    }
}
