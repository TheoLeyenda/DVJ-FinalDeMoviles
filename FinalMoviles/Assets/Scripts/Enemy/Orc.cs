using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : Enemy
{
    public float delayMagicCuration;
    public SphereHealing sphereHealing;
    [Header("Rangos del delay de curacion")]
    public float minDelayMagicCuration;
    public float maxDelayMagicCuration;
    protected override void Start()
    {
        delayMagicCuration = Random.Range(minDelayMagicCuration, maxDelayMagicCuration);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        CheckCuration();
    }
    public void CheckCuration()
    {
        if (life > 0)
        {
            if (delayMagicCuration > 0)
            {
                delayMagicCuration = delayMagicCuration - Time.deltaTime;
            }
            else if (delayMagicCuration <= 0)
            {
                if (life > 0)
                {
                    MagicCuration();
                }
                delayMagicCuration = Random.Range(minDelayMagicCuration, maxDelayMagicCuration);
            }
        }
    }
    public void MagicCuration()
    {
        switch (sphereHealing.addCountLife)
        {
            case SphereHealing.AddCountLife.MaxLife:
                life = life + maxLife;
                auxLife = life;
                break;
            case SphereHealing.AddCountLife.OneQuarter:
                life = life + maxLife / 4;
                auxLife = life;
                break;
            case SphereHealing.AddCountLife.OneThird:
                life = life + maxLife / 3;
                auxLife = life;
                break;
            case SphereHealing.AddCountLife.OneTwo:
                life = life + maxLife / 2;
                auxLife = life;
                break;
        }
        if (life > maxLife)
        {
            life = maxLife;
        }
        //animator.Play("Jump");
        animator.SetTrigger("Jump");
        sphereHealing.gameObject.SetActive(true);
    }
}
