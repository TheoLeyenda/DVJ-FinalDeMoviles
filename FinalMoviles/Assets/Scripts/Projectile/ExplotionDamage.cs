using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionDamage : MonoBehaviour
{
    // Start is called before the first frame update
    private int damage;
    public Meteoro meteoro;
    void Start()
    {
        damage = meteoro.Damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy e = other.GetComponent<Enemy>();
            e.life = e.life - damage;
        }
    }
}
