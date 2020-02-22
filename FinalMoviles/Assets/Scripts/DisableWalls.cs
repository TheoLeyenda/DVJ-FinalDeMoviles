using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWalls : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> walls;
    public void DisableWall()
    {
        for (int i = 0; i < walls.Count; i++)
        {
            walls[i].SetActive(false);
        }
    }
}
