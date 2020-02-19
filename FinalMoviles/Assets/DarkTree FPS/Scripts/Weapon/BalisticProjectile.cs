using UnityEngine;
using System;

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
        public enum Shooter
        {
            Player,
            Enemy,
        }
        public TypeBullet typeBullet; 
        [HideInInspector]
        public float airResistance = 0.1f;

        private float time;

        private float livingTime = 1f;

        private bool collisionEnemy;
        Vector3 lastPosition;

        public Weapon weapon;

        public TurtleShell turtleShell;

        public Shooter shooter;

        public PlayerStats playerStats;

        public static event Action<BalisticProjectile> OnHitEnemy;
        public static event Action<BalisticProjectile> OnPlayerShoot;
        private void Start()
        {
            
        }
        private void OnEnable()
        {
            if (shooter == Shooter.Player)
            {
                GetComponent<Rigidbody>().AddForce(transform.forward * initialVelocity);
                if (OnPlayerShoot != null)
                {
                    OnPlayerShoot(this);
                }
            }

            lastPosition = transform.position;
            RaycastHit hit;
            if (Physics.Raycast(lastPosition, transform.forward, out hit, initialVelocity))
            {
                if (turtleShell == null && shooter == Shooter.Player)
                {
                    if (hit.transform.tag == "Enemy")
                    {
                        
                            //animator.SetTrigger("Damage");
                        Enemy e = hit.transform.gameObject.GetComponent<Enemy>();
                        e.bloodEffect.gameObject.SetActive(true);
                        e.bloodEffect.transform.position = hit.point;
                        e.rig.velocity = Vector3.zero;
                        e.rig.angularVelocity = Vector3.zero;
                        if (OnHitEnemy != null)
                        {
                            OnHitEnemy(this);
                        }
                        if (e.life > 0)
                        {
                            e.animator.Play("Damage");
                        }
                        float range;
                        if (typeBullet == TypeBullet.Rifle)
                        {
                            range = UnityEngine.Random.Range(10, 15);
                            if (e.scalerBloodVar > 0)
                            {
                                range = range / (e.scalerBloodVar);
                            }
                            e.bloodEffect.gameObject.transform.localScale = new Vector3(range, range, range);
                        }
                        else if (typeBullet == TypeBullet.Pistol)
                        {
                            range = UnityEngine.Random.Range(5, 10);
                            if (e.scalerBloodVar > 0)
                            {
                                range = range / (e.scalerBloodVar);
                            }
                            e.bloodEffect.gameObject.transform.localScale = new Vector3(range, range, range);
                        }
                        e.bloodEffect.Play();
                        if (e.typeEnemy == Enemy.TypeEnemy.defensive)
                        {
                            e.life = e.life - UnityEngine.Random.Range(weapon.damageMin, weapon.damageMax) / e.deffense;
                        }
                        else
                        {
                            e.life = e.life - UnityEngine.Random.Range(weapon.damageMin, weapon.damageMax);
                        }

                        gameObject.SetActive(false);
                    }
                    if (hit.transform.tag == "Shild")
                    {
                        Shild s = hit.transform.gameObject.GetComponent<Shild>();
                        s.shildFBX.gameObject.SetActive(true);
                        s.shildFBX.transform.position = hit.point;
                        s.shildFBX.Play();

                        if (s.turtleShell != null)
                        {
                            //transform.Rotate(Vector3.up, 180);
                            GetComponent<Rigidbody>().velocity = Vector3.zero;
                            s.turtleShell.CounterAttack(hit.point);
                            //transform.Rotate(Vector3.up, 180);
                        }
                        gameObject.SetActive(false);
                    }
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
        private void OnCollisionEnter(Collision other)
        {
            /*if (other.transform.tag == "Enemy")
            {
                blood.gameObject.SetActive(true);
                //transform.Rotate(Vector3.up, 180)
                blood.Play();
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                
                collisionEnemy = true;
                Debug.Log("ENTRE");
            }*/
            if (other.transform.tag == "Player" && turtleShell != null)
            {
                PlayerStats player = other.gameObject.GetComponent<PlayerStats>();
                player.health = player.health - turtleShell.counterAttackDamage;
                player.goMasksBloodScreen[0].SetActive(true);
                gameObject.SetActive(false);
            }
            else if(other.transform.tag != "Inside" && other.transform.tag != "Player" && other.transform.tag != "Enemy" && other.transform.tag != "Shild" && shooter == Shooter.Enemy)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            collisionEnemy = false;
            time = 0;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            transform.position = Vector3.zero;
        }
    }
}
