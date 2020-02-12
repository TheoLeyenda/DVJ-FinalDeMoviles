using UnityEngine;
using System.Collections;

public class Recoil : MonoBehaviour
{
    public float recoilReleaseSpeed = 2f;
    public CameraShake cameraShake;
    private void Update()
    {
        if (!cameraShake.GetInShake())
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(Vector3.zero), Time.deltaTime * recoilReleaseSpeed);
        }
    }

    public void AddRecoil(Vector3 recoil)
    {
        transform.localRotation *= Quaternion.Euler(recoil);
    }
}