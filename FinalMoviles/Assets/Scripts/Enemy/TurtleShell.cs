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
    public BoxCollider boxColliderShild;
    public BoxCollider boxColliderDamage1;
    public BoxCollider boxColliderDamage2;

    protected override void Update()
    {
        if (stateEnemy != StateEnemy.stune)
        { 
            if (animator.enabled)
            {
                CheckMeleAttack();
            }
            if (life <= 0 && nameEnemy == "TurtleShell")
            {
                boxColliderDamage1.enabled = false;
                boxColliderDamage2.enabled = false;
                boxColliderShild.enabled = false;
            }
            if (!boxColliderDamage1.enabled && !boxColliderDamage2.enabled && !boxColliderShild.enabled)
            {
                nameEnemy = "";
            }
        }
        base.Update();
        CheckState();
    }
    private void OnDisable()
    {
        nameEnemy = "TurtleShell";
        boxColliderDamage1.enabled = true;
        boxColliderDamage2.enabled = true;
        boxColliderShild.enabled = true;
    }
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
