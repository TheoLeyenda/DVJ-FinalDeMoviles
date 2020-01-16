using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMonster : Enemy
{
    // Start is called before the first frame update

    public int DamageConstructions;
    private bool constructionInRange;
    private Transform targetConstruction;
    public float delayAttack;
    public float auxDelayAttack;
    public Pool poolProyectile;
    void Start()
    {
        constructionInRange = false;
    }
    
    // Update is called once per frame
    protected override void Update()
    {
        if (!constructionInRange)
        {
            CheckFinishRouteEnemy();
        }
        else if (constructionInRange)
        {
            CheckDelayAttack();
            MovementLerp(targetConstruction.position, 100);
        }
        CheckDieEnemy();
    }
    public void CheckDelayAttack()
    {
        if (delayAttack > 0)
        {
            delayAttack = delayAttack - Time.deltaTime; 
        }
        else if (delayAttack <= 0)
        {
            Attack();//Cambiar esto por la animacion de ataque cuando esten todas las animaciones.
            delayAttack = auxDelayAttack;
        }
    }
    public override void Attack()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Construccion")
        {
            targetConstruction.position = new Vector3(other.transform.position.x,transform.position.y,other.transform.position.z);
            constructionInRange = true;
            speed = 0;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Construccion")
        {
            constructionInRange = false;
            speed = auxSpeed;
        }
    }
    public void MovementLerp(Vector3 finalPosition, float rootSpeed)
    {
        //transform.position += transform.forward / 50;
        transform.forward = Vector3.Slerp(transform.forward, finalPosition, rootSpeed * Time.deltaTime);
    }
}
