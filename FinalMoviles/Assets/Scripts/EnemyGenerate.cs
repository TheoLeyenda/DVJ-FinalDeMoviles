using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    // Start is called before the first frame update
    enum NameEnemys
    {

    }
    private NameEnemys nameEnemys;
    public float delayGeneration;
    public List<Enemy> enemysForGenerate;
    public int[] countEnemysForWave;
    private bool swarm;//Enjambre (boleano que controla si los enemigos a salir salen en enjambre o no)

    // Update is called once per frame
    void Update()
    {
        
    }
}
