using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRoute : MonoBehaviour
{
    [System.Serializable]
    public class Curve
    {
        public GameObject[] points;
    }
    // Start is called before the first frame update
    
    public Curve[] curves;
    
}
