using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private bool inShake;
    private void Start()
    {
        inShake = false;
    }
    public bool GetInShake()
    {
        return inShake;
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;
        inShake = true;
        while (elapsed < duration)
        {
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed = elapsed + Time.deltaTime;

            yield return null;
        }
        inShake = false;
        transform.localPosition = originalPos;
    }
}
