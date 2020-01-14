using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTreeFPS;

public class EventStartBattel : EventsGame
{
    // Start is called before the first frame update
    private bool once = true;
    public Dialogue dialogue;
    public EnemyGenerate enemyGenerate;
    public GameManager gm;
    public WeaponManager weaponManager;
    private void Update()
    {
        if (once && enemyGenerate.gameObject.activeSelf)
        {
            CheckEventInitialAttack();
        }
    }
    public void CheckEventInitialAttack()
    {
        if (eventOf == EventOf.Tutorial && gm.InTutorial)
        {
            if (enemyGenerate.delayBetweenWaves <= 0 && once)
            {
                once = false;
                dialogue.BarDialogue.SetActive(true);
                dialogue.CheckDialogue();
                weaponManager.enableShoot = true;
            }
        }
    }
}
