using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMonster : Enemy
{
    // Start is called before the first frame update

    public int DamageConstructions;
    private bool constructionInRange;
    private Transform targetConstruction;
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
        CheckDieEnemy();
    }
    public override void Attack()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Construccion")
        {
            targetConstruction = other.gameObject.transform;
            constructionInRange = true;
        }
    }
}
