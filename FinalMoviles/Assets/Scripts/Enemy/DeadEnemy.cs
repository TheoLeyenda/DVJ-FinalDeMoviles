using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Enemy enemy;
    public void Dead()
    {
        enemy.DieEnemy();
    }
}
