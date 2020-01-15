

using UnityEngine;

namespace DarkTreeFPS
{
    public class BalisticProjectile : MonoBehaviour
    {
        public ParticleSystem blood;
        public float initialVelocity = 180;
        public enum TypeBullet
        {
            Pistol,
            Rifle,
        }
        public TypeBullet typeBullet; 
        [HideInInspector]
        public float airResistance = 0.1f;

        private float time;

        private float livingTime = 1f;

        private bool collisionEnemy;
        Vector3 lastPosition;

        public Weapon weapon;
        private void OnEnable()
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * initialVelocity);

            lastPosition = transform.position;
            RaycastHit hit;
            if (Physics.Raycast(lastPosition, transform.forward, out hit, initialVelocity))
            {
                if (hit.transform.tag == "Enemy")
                {
                    Enemy e = hit.transform.gameObject.GetComponent<Enemy>();
                    e.blood.gameObject.SetActive(true);
                    e.blood.transform.position = hit.point;
                    float range;
                    if (typeBullet == TypeBullet.Rifle)
                    {
                        range = Random.Range(10, 15);
                        if (e.scalerBloodVar > 0)
                        {
                            range = range / (e.scalerBloodVar);
                        }
                        e.blood.gameObject.transform.localScale = new Vector3(range, range, range);
                    }
                    else if (typeBullet == TypeBullet.Pistol)
                    {
                        range = Random.Range(5, 10);
                        if (e.scalerBloodVar > 0)
                        {
                            range = range / (e.scalerBloodVar);
                        }
                        e.blood.gameObject.transform.localScale = new Vector3(range, range, range);
                    }
                    e.blood.Play();
                    e.life = e.life - Random.Range(weapon.damageMin, weapon.damageMax);

                    gameObject.SetActive(false);
                }
                if (hit.transform.tag == "Shild")
                {
                    Shild s = hit.transform.gameObject.GetComponent<Shild>();
                    s.shildFBX.gameObject.SetActive(true);
                    s.shildFBX.transform.position = hit.point;
                    s.shildFBX.Play();
                    gameObject.SetActive(false);
                }
            }
        }

        private void Update()
        {
            time += Time.deltaTime;

            
            if (time > livingTime)
            {
                gameObject.SetActive(false);
            }
            if (collisionEnemy && blood.isPaused)
            {
                gameObject.SetActive(false);
            }
        }
        /*private void OnCollisionEnter(Collision other)
        {
            if (other.transform.tag == "Enemy")
            {
                blood.gameObject.SetActive(true);
                //transform.Rotate(Vector3.up, 180)
                blood.Play();
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                
                collisionEnemy = true;
                Debug.Log("ENTRE");
            }
        }*/

        private void OnDisable()
        {
            //Debug.Log("DISABLE");
            collisionEnemy = false;
            time = 0;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = Vector3.zero;
        }
    }
}
