using UnityEngine;

public class DamageSingle : MonoBehaviour
{
    public static int HP1 = 7;
    public GameObject Wizard;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet2")
        {
            HP1--;
        }     
    }   
}
