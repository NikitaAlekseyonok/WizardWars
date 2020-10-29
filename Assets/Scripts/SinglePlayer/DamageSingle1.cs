using UnityEngine;

public class DamageSingle1 : MonoBehaviour
{
    public static int HP2 = 3;
    public GameObject Wizard;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet1")
            HP2--;

        if (HP2 <= 0)
            Destroy(Wizard);
    }
}

