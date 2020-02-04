using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMeteoro : MonoBehaviour
{
    // Start is called before the first frame update
    public Pool poolMeteoro;
    public InputManager inputManager;
    public float rangeRay;

    // Update is called once per frame
    void Update()
    {
        CheckShoot();
    }
    public void CheckShoot()
    {
        //Debug.Log()
        if (gameObject.activeSelf)
        {
            if (Input.GetKeyDown(inputManager.Fire))
            {
                //Debug.Log("DISPARE");
                //Vector3 position;
                //Quaternion rotation;
                //position = new Vector3(Camera.main.ScreenToViewportPoint(Input.mousePosition).x, transform.position.y, Camera.main.ScreenToViewportPoint(Input.mousePosition).z);

                //position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                //Debug.Log(position);
                //rotation = transform.rotation;
                //GameObject go = poolMeteoro.GetObject();
                //go.transform.position = position;
                //go.transform.rotation = rotation;
                //go.GetComponent<Rigidbody>().AddForce(go.transform.forward * speedMeteoro, ForceMode.Impulse);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, rangeRay))
                {
                    if (hit.collider != null)
                    {
                        GameObject go = poolMeteoro.GetObject();
                        go.transform.position = ray.origin;
                        Meteoro meteoro = go.GetComponent<Meteoro>();
                        meteoro.target = hit.point;
                       
                    }
                }


                /*if (Physics.Raycast(ray.origin, ray.direction, out hit, 999, Mask))
                {
                    if (hit.transform.gameObject.tag != "explosion" && Time.timeScale != 0 && !EventSystem.current.IsPointerOverGameObject())
                    {
                        if (!shootOnce)
                        {
                            bulletProperties.isFired = true;
                            bulletProperties.target = hit.point;
                            bulletProperties.meteorSpeed = meteorSpeed;
                            GameObject newBullet = Instantiate(bulletTemplate);
                            newBullet.SetActive(true);
                            couldShoot = true;
                            shootOnce = true;

                            if (OnShootMeteor != null)
                            {
                                OnShootMeteor();
                                if (secondTime)
                                {
                                    if (OnShootMeteorSecond != null)
                                    {
                                        OnShootMeteorSecond();
                                    }
                                }
                            }

                            AkSoundEngine.PostEvent("meteoro_lanza", shootMeteorSound);
                            Debug.Log("METEOR SHOOT");
                            secondTime = true;
                        }
                    }
                }*/
            }
        }
    }
}
