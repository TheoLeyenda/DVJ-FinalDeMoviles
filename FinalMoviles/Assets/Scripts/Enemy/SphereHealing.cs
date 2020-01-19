using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereHealing : MonoBehaviour
{
    // Start is called before the first frame update
    public enum AddCountLife
    {
        OneQuarter,
        OneThird,
        OneTwo,
        MaxLife,

    }
    public AddCountLife addCountLife;
    public float timeLife = 0.5f;
    public float auxTimeLife = 0.5f;
    private void Update()
    {
        if (timeLife > 0)
        {
            timeLife = timeLife - Time.deltaTime;
        }
        else if (timeLife <= 0)
        {
            gameObject.SetActive(false);
            timeLife = auxTimeLife;
        }
    }
}
