using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public bool iAmSoon;
    [HideInInspector]
    public int myGenerator;
    public Animator animator;
    public int DamageMeleConstruction;//daño a estructuras por defencto.
    public int DamageLifes; //daño a las vidas del jugador al pasar de punto A a punto B. 
    public ParticleSystem blood;
    public float scalerBloodVar;
    public GameObject EnemyPrefab;
    public FollowRoute auxFollowRoute;
    public FollowRoute followRoute;
    public float speed;
    protected float auxSpeed;
    protected float auxAceleration;
    public float acceletartion;
    public float maxLife = 100;
    public float life = 100;
    protected float auxLife;
    public bool inPool;
    public string nameEnemy;
    private bool finishRoute;
    public float deffense;
    private bool dead = false;
    [Header("Data Mele Attack")]
    public float rangeMeleAttack;
    public float delayMeleAttack;
    public float auxDelayMeleAttack;
    protected bool meleAttack;
    [Header("Data Stune")]
    public float delayStune = 7;
    public float auxDelayStune = 7;
    [HideInInspector]
    public Construction construction;
    [HideInInspector]
    public Rigidbody rig;

    public enum StateEnemy
    {
        none,
        stune,
    }
    public enum TypeEnemy
    {
        none,
        defensive,
    }
    protected StateEnemy stateEnemy;
    public TypeEnemy typeEnemy;

    public static event Action<Enemy> OnDieAction;
    public static event Action<Enemy> OnFinishRoute;
    public static event Action<Enemy> LifeIsZero;
    protected virtual void Start()
    {
        rig = GetComponent<Rigidbody>();
#if UNITY_ANDROID
        speed = speed / 1.2;
#endif
        finishRoute = false;
        acceletartion = acceletartion * 10;
        auxAceleration = acceletartion;
        auxSpeed = speed;
        followRoute = GetComponent<FollowRoute>();
        //Debug.Log(followRoute);
        if (followRoute == null && auxFollowRoute != null)
        {
            followRoute = auxFollowRoute;
            followRoute.GetAgent().speed = speed;
            followRoute.GetAgent().acceleration = acceletartion;
        }
        auxLife = life;

    }
    private void OnEnable()
    {
        life = maxLife;
        auxLife = life;
        dead = false;
        if (followRoute != null)
        {
            followRoute.GetAgent().speed = speed;
        }
        meleAttack = false;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (stateEnemy != StateEnemy.stune)
        {
            CheckFinishRouteEnemy();
            //CheckDieEnemy();
            CheckMeleAttack();
        }
        CheckAnimations();
        CheckState();
    }
    public void CheckFinishRouteEnemy()
    {
        if (followRoute.CheckFinishRoute() || finishRoute)
        {
            //SI ANDA MAL LA DESAPARICION O APARICION DEL ENEMIGO PROGRAMAR EL TEMA DEL RECICLADO DEL POOL
            finishRoute = false;
            if (EnemyPrefab != null)
            {
                if (OnDieAction != null && !iAmSoon)
                    OnDieAction(this);
                if (OnFinishRoute != null)
                    OnFinishRoute(this);

                EnemyPrefab.SetActive(false);
            }
            else
            {
                if (OnDieAction != null && !iAmSoon)
                    OnDieAction(this);
                if (OnFinishRoute != null)
                    OnFinishRoute(this);

                gameObject.SetActive(false);
            }
        }
    }
    public void CheckMeleAttack()
    {
        if (animator.isActiveAndEnabled)
        {
            if (life > 0)
            {
                if (nameEnemy != "BoximonFiery" && nameEnemy != "StoneMonster")
                {
                    RaycastHit hit;
                    Vector3 position = transform.position;
                    if (nameEnemy == "TurtleShell")
                    {
                        position = position + new Vector3(0, 0, 2);
                    }
                    if (Physics.Raycast(position, Vector3.forward, out hit, rangeMeleAttack))
                    {

                        if (hit.transform.tag == "MeleTarget")
                        {
                            Wall wall = hit.transform.gameObject.GetComponent<Wall>();
                            if (wall != null)
                            {
                                animator.SetBool("Idle", false);
                                animator.SetBool("Move", false);
                                construction = wall.construction;
                                followRoute.GetAgent().speed = 0;
                            }
                            else
                            {
                                construction = null;
                                followRoute.GetAgent().speed = speed;
                            }
                        }
                    }
                    if (construction != null)
                    {
                        if (delayMeleAttack > 0)
                        {
                            delayMeleAttack = delayMeleAttack - Time.deltaTime;
                        }
                        else
                        {
                            construction.life = construction.life - DamageMeleConstruction;
                            delayMeleAttack = auxDelayMeleAttack;
                            animator.Play("MeleAttack");
                        }
                    }
                }
            }
        }
    }
    public void CheckState()
    {
        if (stateEnemy == StateEnemy.stune)
        {
            if (delayStune > 0)
            {
                delayStune = delayStune - Time.deltaTime;
            }
            else if (delayStune <= 0)
            {
                delayStune = auxDelayStune;
                stateEnemy = StateEnemy.none;
                followRoute.GetAgent().speed = speed;
            }
        }
    }
    public void CheckDieEnemy()
    {
        if (life <= 0)
        {
            
            //SI ANDA MAL LA DESAPARICION O APARICION DEL ENEMIGO PROGRAMAR EL TEMA DEL RECICLADO DEL POOL
            DieEnemy();
        }
    }
    public void DieEnemy()
    {
        if (LifeIsZero != null)
        {
            LifeIsZero(this);
        }
        if (EnemyPrefab != null)
        {
            if (OnDieAction != null && !iAmSoon)
                OnDieAction(this);
            EnemyPrefab.SetActive(false);
        }
        else
        {
            if (OnDieAction != null && !iAmSoon)
                OnDieAction(this);
            gameObject.SetActive(false);
        }
    }
    public virtual void Attack() { }
    private void OnTriggerEnter(Collider other)
    {
        //ACA PROGRAMAR LA SUBIDA DE VIDA.
        if (other.tag == "LifeAura")
        {
            SphereHealing sphereHealing = other.GetComponent<SphereHealing>();
            if (sphereHealing != null)
            {
                switch (sphereHealing.addCountLife)
                {
                    case SphereHealing.AddCountLife.MaxLife:
                        life = life + maxLife;
                        auxLife = life;
                        break;
                    case SphereHealing.AddCountLife.OneQuarter:
                        life = life + maxLife / 4;
                        auxLife = life;
                        break;
                    case SphereHealing.AddCountLife.OneThird:
                        life = life + maxLife / 3;
                        auxLife = life;
                        break;
                    case SphereHealing.AddCountLife.OneTwo:
                        life = life + maxLife / 2;
                        auxLife = life;
                        break;
                }
                
                if (life > maxLife)
                {
                    life = maxLife;
                }
            }
        }
        if (other.tag == "Nuke")
        {
            life = 0;
        }
        if (other.tag == "Ice")
        {
            followRoute.GetAgent().speed = 0;
            stateEnemy = StateEnemy.stune;
        }
        if (other.tag == "DestroyEnemy")
        {
            life = 0;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ice")
        {
            followRoute.GetAgent().speed = speed;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "FinishPoint")
        {
            finishRoute = true;
        }
        //ESTO VERIFICA QUE SI EL ENEMIGO COLISIONA CON ALGUNO DE ESTOS TAGS LA VELOCIDAD DEL MISMO SERA 0
        //(SI ESTO NO FUNCIONA COPIAR Y PEGAR ESTO EN LOS SCRITPS DE LOS ENEMIGOS QUE HEREDAN DE ESTA CLASE)
        if (collision.gameObject.tag == "Meteoro" || collision.gameObject.tag == "Proyectil" || collision.gameObject.tag == "bullet")
        {
            rig.velocity = Vector3.zero;
            rig.angularVelocity = Vector3.zero;
        }
        //-------------------------------------------------------------------------------------------------
    }
    public virtual void CheckAnimations()
    {
        if (followRoute != null)
        {
            if (life > 0)
            {
                if (followRoute.GetAgent().speed > 0)
                {
                    animator.SetBool("Dead", false);
                    animator.SetBool("Idle", false);
                    animator.SetBool("Move", true);
                }
                else if (followRoute.GetAgent().speed <= 0 && !animator.GetBool("Dead") && construction == null)
                {
                    animator.SetBool("Dead", false);
                    animator.SetBool("Idle", true);
                    animator.SetBool("Move", false);
                }
            }
        }
        if(life <= 0 && !dead && nameEnemy != "TurtleShell")
        {
            followRoute.GetAgent().speed = 0;
            animator.SetBool("Dead",true);
            animator.SetBool("Idle", false);
            animator.SetBool("Move", false);
            dead = true;
            if (nameEnemy == "Slime_2(Small)")
            {
                DieEnemy();
            }
        }
    }
}
