using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float damping = 1.5f;
    public Vector2 offset = new Vector2(0f, 0f);
    public bool faceLeft;
    private Transform player;
    private int lastX;

    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
    }

    public void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("PlayerWizard").transform;
        lastX = Mathf.RoundToInt(player.position.x);
        transform.position = new Vector2(player.position.x - offset.x, player.position.y + offset.y);
    }

    public void Update()
    {
        FindPlayer();
        if (player)
        {
            int currentX = Mathf.RoundToInt(player.position.x);
            if (currentX > lastX) faceLeft = false;
            else if (currentX < lastX) faceLeft = true;
            lastX = Mathf.RoundToInt(player.position.x);
            Vector3 target;
            target = new Vector2(player.position.x, player.position.y );
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
            currentPosition.z = -5;
            transform.position = currentPosition;  
        }
    }
}
