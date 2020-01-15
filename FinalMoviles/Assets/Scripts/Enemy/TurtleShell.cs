using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTreeFPS;
using System;

public class TurtleShell : Enemy
{
    // Start is called before the first frame update
    public Pool PoolBullets;
    public int counterAttackDamage;
    public float counterAttackVelocity;

    public void CounterAttack(Vector3 position)
    {
        GameObject go = PoolBullets.GetObject();
        BalisticProjectile balisticProjectile = go.GetComponent<BalisticProjectile>();
        balisticProjectile.turtleShell = this;
        go.transform.position = position;
        go.transform.LookAt(SingletonObject.instanceSingletonObject.transform);
       
        balisticProjectile.GetComponent<Rigidbody>().AddForce(balisticProjectile.transform.forward * counterAttackVelocity);
    }
}
