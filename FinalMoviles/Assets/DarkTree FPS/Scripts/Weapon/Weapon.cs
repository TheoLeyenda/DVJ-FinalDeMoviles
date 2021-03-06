﻿/// DarkTreeDevelopment (2019) DarkTree FPS v1.2
/// If you have any questions feel free to write me at email --- darktreedevelopment@gmail.com ---
/// Thanks for purchasing my asset!

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace DarkTreeFPS
{
    public class Weapon : MonoBehaviour
    {


        [Header("Weapon setting")]

        public bool UseItemAmmo;

        public WeaponSettingSO weaponSetting;

        [Tooltip("Set the name of weapon as in the weapon settings objects and pickup")]
        public string weaponName;

        [HideInInspector]
        public WeaponType weaponType;

        [Tooltip("Ammo item index")]
        public int ammoItemID;

        [Tooltip("Transform to instantiate particle system shot fx")]
        public Transform muzzleFlashTransform;
        [Tooltip("Transform to eject shell after shot")]
        public Transform shellTransform;
        [Tooltip("Transform to instantiate bullet on shot")]
        public Transform bulletTransform;

        [Tooltip("If you have animations for your weapon so better to use animator. Play animations if true and not if false")]
        public bool useAnimator = true;

        [Tooltip("How long reload animation is? Time in seconds to synch reloading animation with script")]
        public float reloadAnimationDuration = 3.0f;

        [Tooltip("Should weapon reload when ammo is 0")]
        public bool autoReload = true;

        [Header("Ammo")]
        [Tooltip("Ammo count in weapon magazine")]
        public int currentAmmo = 30;
        [Tooltip("Max weapon ammo capacity")]
        public int maxAmmo = 30;

        public GameObject grenadePrefab;

        public enum FireMode { automatic, single}
        [Header("Fire mode")]
        public FireMode fireMode;


        public int _totalAmmo;
        #region Utility variables

        //Here is a utility variables which get value from other scripts, set values to other scripts or used for calculations etc. 
        //I will not comment them because it may look complicated and dirty
        //If you'll get questions about them write me on email please

        [HideInInspector]
        public FPSController controller;


        private ParticleSystem MuzzleFlashParticlesFX;
        private AudioClip shotSFX, reloadingSFX, emptySFX;
        private GameObject shell;
        private int shellPoolSize;
        private float shellForce;
        [HideInInspector]
        public Text ammoText;
        private Text weaponNameText;

        //Stats
        [HideInInspector]
        public int damageMin, damageMax;
        private float fireRate;
        [HideInInspector]
        public float rigidbodyHitForce;
        private float spread;
        private Vector3 recoil;
        private bool useScope;
        private float scopeFOV;

        [HideInInspector]
        public Animator animator;
        private AudioSource audioSource;
        private Camera cam;

        private GameObject[] decals;
        private GameObject[] shells;

        private int shellIndex = 0;

        private int decalIndex_wood = 0;
        private int decalIndex_concrete = 0;
        private int decalIndex_dirt = 0;
        private int decalIndex_metal = 0;

        private float nextFireTime;

        [HideInInspector]
        public bool reloading = false;
        [HideInInspector]
        public bool canShot = true;
        [HideInInspector]
        public bool setAim = false;

        private float scopeTimer = 0f;
        private float scopeActivateTimer = 0.5f;

        private GameObject scope;

        private float normalFOV;
        private float normalSensX;
        private float normalSensY;

        private float scopeSensitivityX, scopeSensitivityY;

        private ParticleSystem temp_MuzzleFlashParticlesFX;

        private WeaponManager weaponManager;

        private float bulletInitialVelocity = 360;
        private float airResistanceForce = 1;
        private GameObject projectile;
        private GameObject[] projectiles;
        private int projectilePoolSize;
        private int projectilesIndex;

        private HitFXManager hitFXManager;

        [HideInInspector]
        public int calculatedDamage;

        private AudioSource ricochetSource;
        private AudioClip[] ricochetSounds;

        [HideInInspector]
        public RectTransform dynamicReticle;
        [HideInInspector]
        public GameObject staticReticle;
        [HideInInspector]
        public bool canUpdateCrosshair = true;

        //Melee variables (used only if weapon is melee type)
        private float meleeAttackDistance;
        private float meleeAttackRate;
        private int meleeDamagePoints;
        private float meleeRigidbodyHitForce;
        private float meleeHitTime;
        private AudioClip meleeHitFX;

        private float distanceToPlayer;

        //This factor used for adding more spread when character shooting in movement
        private float movementSpreadFactor;

        private Inventory inventory;
        [HideInInspector]
        public InputManager input;
        private Recoil recoilComponent;
        [HideInInspector]
        public Sway sway;

        [HideInInspector]
        public bool isThrowingGrenade = false;
        #endregion

        // Get weapon name on Awake() before WeaponManager will disable weapon components. Made for pickup functionality. WeaponManager enables weapon by name and weapon name must be initialized before it turns off
        private void Awake()
        {
            weaponName = weaponSetting.weaponName;
        }

        //Main setting function that grab values from Weapon scriptable object and set it to this class
        private void GetWeaponSettings()
        {
            weaponType = weaponSetting.weaponType;

            if (weaponType != WeaponType.Melee)
                MuzzleFlashParticlesFX = weaponSetting.MuzzleFlashParticlesFX;
            shotSFX = weaponSetting.shotSFX;
            reloadingSFX = weaponSetting.reloadingSFX;
            emptySFX = weaponSetting.emptySFX;

            shell = weaponSetting.shell;
            shellPoolSize = weaponSetting.shellsPoolSize;
            shellForce = weaponSetting.shellsPoolSize;

            damageMin = weaponSetting.damageMinimum;
            damageMax = weaponSetting.damageMaximum;
            fireRate = weaponSetting.fireRate;
            rigidbodyHitForce = weaponSetting.rigidBodyHitForce;
            recoil = weaponSetting.recoil;
            spread = weaponSetting.spread;
            useScope = weaponSetting.canUseScope;
            scopeFOV = weaponSetting.scopeFOV;
            scopeSensitivityX = weaponSetting.scopeSensitivityX;
            scopeSensitivityY = weaponSetting.scopeSensitivityY;

            bulletInitialVelocity = weaponSetting.bulletInitialVelocity;
            airResistanceForce = weaponSetting.airResistanceForce;
            projectile = weaponSetting.projectile;
            projectilePoolSize = weaponSetting.projectilePoolSize;

            if (weaponType == WeaponType.Melee)
            {
                meleeAttackDistance = weaponSetting.meleeAttackDistance;
                meleeAttackRate = weaponSetting.meleeAttackRate;
                meleeDamagePoints = weaponSetting.meleeDamagePoints;
                meleeRigidbodyHitForce = weaponSetting.meleeRigidbodyHitForce;
                meleeHitTime = weaponSetting.meleeHitTime;
                meleeHitFX = weaponSetting.meleeHitFX;
            }

    }
        
        private void Start()
        {
            GetWeaponSettings();
            if (weaponType != WeaponType.Melee && weaponType != WeaponType.Grenade)
                BalisticProjectilesPool();

            if (GetComponent<Animator>())
                animator = GetComponent<Animator>();
            else
                Debug.LogError("Please attach animator to your weapon object");

            if (GetComponent<AudioSource>())
                audioSource = GetComponentInParent<AudioSource>();
            else
                Debug.LogError("Please attach AudioSource to your weapon object");

            cam = Camera.main;

            controller = FindObjectOfType<FPSController>();

            normalSensX = controller.sensitivity.x;
            normalSensY = controller.sensitivity.y;
            
            normalFOV = cam.fieldOfView;

            weaponManager = FindObjectOfType<WeaponManager>();

            scope = weaponManager.scopeImage;
            ammoText = GameObject.Find("AmmoText").GetComponent<Text>();
            weaponNameText = GameObject.Find("WeaponText").GetComponent<Text>();

            recoilComponent = FindObjectOfType<Recoil>();
            sway = FindObjectOfType<Sway>();

            if (weaponType != WeaponType.Melee && weaponType != WeaponType.Grenade)
            {
                if (shell)
                    ShellsPool();
                else
                    Debug.LogError("No shell gameobject attached to weapon settings object");

                if (MuzzleFlashParticlesFX)
                    temp_MuzzleFlashParticlesFX = Instantiate(MuzzleFlashParticlesFX, muzzleFlashTransform.position, muzzleFlashTransform.rotation, muzzleFlashTransform);
                else
                    Debug.LogWarning("There is no shot particle system attached to weapon settings");

            }

            hitFXManager = FindObjectOfType<HitFXManager>();

            ricochetSource = hitFXManager.gameObject.GetComponent<AudioSource>();
            ricochetSounds = hitFXManager.ricochetSounds;

            inventory = FindObjectOfType<Inventory>();
            input = FindObjectOfType<InputManager>();

            /*if (weaponManager.UseNonPhysicalReticle)
            {
                if (GameObject.Find("StaticReticle") != null)
                    staticReticle = GameObject.Find("StaticReticle");
            }
            else
            {
                if (GameObject.Find("DynamicReticle") != null)
                    dynamicReticle = GameObject.Find("DynamicReticle").GetComponent<RectTransform>();
            }*/

            staticReticle = weaponManager.staticReticle;
            dynamicReticle = weaponManager.dynamicReticle;
            UseItemAmmo = false;
        }


        void Update()
        {
#if UNITY_STANDALONE
            scopeSensitivityX = controller.sensitivity.x;
            scopeSensitivityY = controller.sensitivity.y;
#endif
#if UNITY_ANDROID
            scopeSensitivityX = 0.2f;
            scopeSensitivityY = 0.2f;
#endif
            movementSpreadFactor = controller.GetVelocityMagnitude();
#if !UNITY_ANDROID
            if (Input.GetKey(input.Fire)  && !PlayerStats.isPlayerDead && weaponType != WeaponType.Pistol && !InventoryManager.showInventory && fireMode == FireMode.automatic)  //Statement to restrict auto-fire for pistol weapon type. Riffle and others are automatic
            {
                Fire();
            }
            else if (Input.GetKeyDown(input.Fire) && !PlayerStats.isPlayerDead && (weaponType == WeaponType.Pistol || fireMode == FireMode.single) && !InventoryManager.showInventory)
            {
                Fire();
            }
            
            if (weaponType != WeaponType.Melee && weaponType != WeaponType.Grenade)
            {
                //Reloading consists of two stages ReloadBegin and ReloadEnd  
                //ReloadBegin method play animation and soundFX and also restrict weapon shooting. ReloadingEnd removes restriction and add ammo to weapon
                //See more in methods below
                if (Input.GetKeyDown(input.Reload) || currentAmmo < 0)
                {
                    if (!reloading && !controller.isClimbing)
                        ReloadBegin();
                }

                /*if (Input.GetKey(input.Aim))
                {
                    ActiveAim();
                }
                else
                {
                    DisableAim();
                }*/
                if (Input.GetKeyDown(input.Aim))
                {
                    weaponManager.SetPointer();
                }
                SetAim();
                UpdateAmmoText();

                if (canUpdateCrosshair && !weaponManager.UseNonPhysicalReticle)
                    UpdateCrosshairPosition();
            }
#endif
            if (weaponType != WeaponType.Melee && weaponType != WeaponType.Grenade)
            {
                SetAim();
                UpdateAmmoText();
            }
            FireModeSwitch();
        }
        public void ActiveAim()
        {
            //Apuntar
            if (sway != null)
            {
                setAim = true;
                sway.xSwayAmount = sway.xSwayAmount * 0.3f;
                sway.ySwayAmount = sway.ySwayAmount * 0.3f;
                if (weaponManager.UseNonPhysicalReticle)
                {
                    if (staticReticle != null)
                    {
                        staticReticle.SetActive(false);
                    }
                    else
                    {
                        Debug.Log("staticReticle is Null");
                    }

                }
                else
                {
                    if (dynamicReticle != null)
                    {
                        dynamicReticle.gameObject.SetActive(false);
                    }
                    else
                    {
                        Debug.Log("dynamicReticle is Null");
                    }
                }
            }
        }
        public void DisableAim()
        {
            //Desapuntar
            if (sway != null)
            {
                setAim = false;
                sway.xSwayAmount = sway.startX;
                sway.ySwayAmount = sway.startY;
                if (weaponManager.UseNonPhysicalReticle)
                {
                    if (staticReticle != null)
                    {
                        staticReticle.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("staticReticle is Null");
                    }
                }
                else
                {
                    if (dynamicReticle != null)
                    {
                        dynamicReticle.gameObject.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("dynamicReticle is Null");
                    }
                }
            }
        }
        public void FireMobile()
        {
            Fire();
        }

        public void AimMobile()
        {
            setAim = !setAim;
        }

        public void Fire()
        {
            if (weaponManager.enableShoot)
            {
                if (weaponType != WeaponType.Melee && weaponType != WeaponType.Grenade)
                {
                    if (Time.time > nextFireTime && !reloading && canShot && !controller.isClimbing) //Allow fire statement
                    {
                        if (currentAmmo > 0)
                        {
                            currentAmmo -= 1;

                            PlayFX();

                            //Getting random damage from minimum and maximum damage.
                            calculatedDamage = Random.Range(damageMin, damageMax);

                            ProjectilesManager();

                            recoilComponent.AddRecoil(recoil);

                            //Calculating when next fire call allowed
                            nextFireTime = Time.time + fireRate;


                        }
                        else
                        {
                            if (!reloading && autoReload)
                                ReloadBegin();
                            else
                                audioSource.PlayOneShot(emptySFX);

                            nextFireTime = Time.time + fireRate;
                        }
                    }
                }
                else if (weaponType == WeaponType.Melee)
                {
                    if (Time.time > nextFireTime) //Allow fire statement
                    {
                        audioSource.Stop();
                        audioSource.PlayOneShot(shotSFX);
                        animator.Play("Attack");
                        Invoke("MeleeHit", meleeHitTime);
                        recoilComponent.AddRecoil(recoil);
                        nextFireTime = Time.time + meleeAttackRate;
                    }

                }
                else if (weaponType == WeaponType.Grenade && !isThrowingGrenade)
                {
                    animator.SetTrigger("Throw");
                    isThrowingGrenade = true;
                }
            }
        }

        public void MeleeHit()
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, meleeAttackDistance))
            {
                audioSource.PlayOneShot(meleeHitFX);

                //Decrease health of object by calculatedDamage
                if (hit.collider.GetComponent<ObjectHealth>())
                    hit.collider.GetComponent<ObjectHealth>().health -= meleeDamagePoints;

                /*
                if (hit.collider.GetComponent<NPC>())
                    hit.collider.GetComponent<NPC>().GetHit(meleeDamagePoints);

                if (hit.collider.GetComponent<ZombieNPC>())
                    hit.collider.GetComponent<ZombieNPC>().ApplyHit(meleeDamagePoints);
                */

                if (hit.rigidbody)
                {
                    //Add force to the rigidbody for bullet impact effect
                    hit.rigidbody.AddForceAtPosition(meleeRigidbodyHitForce * cam.transform.forward, hit.point);
                }

                if (hit.collider.tag == "Target")
                {
                    hit.rigidbody.isKinematic = false;
                    hit.rigidbody.AddForceAtPosition(meleeRigidbodyHitForce * cam.transform.forward, hit.point);
                }
            }
        }

        public void Throw()
        {
            var obj = Instantiate(weaponSetting.grenadePrefab);
            obj.transform.position = transform.position + transform.forward * 0.3f;
            obj.GetComponent<Rigidbody>().AddForce(transform.forward * weaponSetting.throwForce);
            isThrowingGrenade = false;
            inventory.RemoveItem("Grenade", true);
            weaponManager.UnhideWeaponAfterGrenadeDrop();
        }

        public void ApplyHit(RaycastHit hit)
        {
            //Play ricochet sfx
            RicochetSFX();
            //Set tag and transform of hit to HitParticlesFXManager
            HitParticlesFXManager(hit);

            //Decrease health of object by calculatedDamage
            if (hit.collider.GetComponent<ObjectHealth>())
                hit.collider.GetComponent<ObjectHealth>().health -= calculatedDamage;

            if (!hit.rigidbody)
            {
                //Set hit position to decal manager
                DecalManager(hit, false);
            }

            if (hit.rigidbody)
            {
                //Add force to the rigidbody for bullet impact effect
                hit.rigidbody.AddForceAtPosition(rigidbodyHitForce * damageMin * cam.transform.forward, hit.point);
            }

            if (hit.collider.tag == "Target")
            {
                hit.rigidbody.isKinematic = false;
                hit.rigidbody.AddForceAtPosition(rigidbodyHitForce * damageMin * cam.transform.forward, hit.point);
            }

            /*
            if (hit.collider.GetComponent<NPC>())
                hit.collider.GetComponent<NPC>().GetHit(Random.Range(damageMin, damageMax));

            if (hit.collider.GetComponent<ZombieNPC>())
                hit.collider.GetComponent<ZombieNPC>().ApplyHit(Random.Range(damageMin, damageMax));
                */
        }

        public void ReloadBegin()
        {
            if (CalculateTotalAmmo() > 0)
            {
                setAim = false;
                reloading = true;
                canShot = false;

                if (useAnimator)
                {
                    animator.SetBool("Aim", false);
                    animator.Play("Reload");
                }

                audioSource.PlayOneShot(reloadingSFX);

                Invoke("ReloadEnd", reloadAnimationDuration);
            }
            else
                return;
        }

        void ReloadEnd()
        {
            var ammoItems = GetAmmoItems();

            var index = 0;

            
            if (UseItemAmmo) // this
            {
                var neededAmmo = maxAmmo - currentAmmo;
                if (ammoItems[index].ammo >= neededAmmo)
                {
                    ammoItems[index].ammo -= neededAmmo;
                    currentAmmo += neededAmmo;

                    if (ammoItems[index].ammo == 0)
                        inventory.RemoveItem(ammoItems[index], true);
                }
                else if (ammoItems[index].ammo < neededAmmo)
                {
                    currentAmmo += ammoItems[index].ammo;
                    neededAmmo -= ammoItems[index].ammo;
                    ammoItems[index].ammo = 0;

                    if (ammoItems[index].ammo == 0)
                        inventory.RemoveItem(ammoItems[index], true);

                    ++index;

                    try
                    {
                        ammoItems[index].ammo -= neededAmmo;
                        currentAmmo += neededAmmo;

                        if (ammoItems[index].ammo == 0)
                            inventory.RemoveItem(ammoItems[index], true);
                    }
                    catch
                    {
                        //Do nothing. If ammo not enough, construction may drop exception. We catch exception there in this case and do nothing. Because construction works OK
                    }
                }
            }
            else if (!UseItemAmmo) // this
            {
                if (weaponManager.PrimaryGun == this)
                {
                    currentAmmo = weaponManager.ammoPrimaryGun;
                }
                else if (weaponManager.M4 == this)
                {
                    //HACER LA RESTA ENTRE LA MUNICION QUE TENGO Y LA QUE ESTOY CARGANDO
                    var neededAmmo = weaponManager.ammoM4 - currentAmmo;
                    if (weaponManager.gd.dataPlayer.M4Ammo < neededAmmo)
                    {
                        neededAmmo = weaponManager.gd.dataPlayer.M4Ammo;
                    }
                    currentAmmo = currentAmmo + neededAmmo;

                    if (currentAmmo <= 0)
                    {
                        currentAmmo = 0;
                    }
                    weaponManager.gd.dataPlayer.M4Ammo = weaponManager.gd.dataPlayer.M4Ammo - neededAmmo;
                    if (weaponManager.gd.dataPlayer.M4Ammo <= 0)
                    {
                        weaponManager.gd.dataPlayer.M4Ammo = 0;
                    }

                }
                else if (weaponManager.SCAR == this)
                {
                    //HACER LA RESTA ENTRE LA MUNICION QUE TENGO Y LA QUE ESTOY CARGANDO
                    var neededAmmo = weaponManager.ammoSCAR - currentAmmo;
                    if (weaponManager.gd.dataPlayer.scarAmmo < neededAmmo)
                    {
                        neededAmmo = weaponManager.gd.dataPlayer.scarAmmo;
                    }
                    currentAmmo = currentAmmo + neededAmmo;
                    if (currentAmmo <= 0)
                    {
                        currentAmmo = 0;
                    }
                    weaponManager.gd.dataPlayer.scarAmmo = weaponManager.gd.dataPlayer.scarAmmo - neededAmmo;
                    if (weaponManager.gd.dataPlayer.scarAmmo <= 0)
                    {
                        weaponManager.gd.dataPlayer.scarAmmo = 0;
                    }
                }
                else if (weaponManager.Sniper == this)
                {
                    //HACER LA RESTA ENTRE LA MUNICION QUE TENGO Y LA QUE ESTOY CARGANDO
                    var neededAmmo = weaponManager.ammoSniper - currentAmmo;
                    if (weaponManager.gd.dataPlayer.SniperAmmo < neededAmmo)
                    {
                        neededAmmo = weaponManager.gd.dataPlayer.SniperAmmo;
                    }
                    currentAmmo = currentAmmo + neededAmmo;
                    if (currentAmmo <= 0)
                    {
                        currentAmmo = 0;
                    }
                    weaponManager.gd.dataPlayer.SniperAmmo = weaponManager.gd.dataPlayer.SniperAmmo - neededAmmo;
                    if (weaponManager.gd.dataPlayer.SniperAmmo <= 0)
                    {
                        weaponManager.gd.dataPlayer.SniperAmmo = 0;
                    }
                }
            }
            /*
            for(int i = currentAmmo; i < maxAmmo; ++i) //For ammo < max ammo
            {
                //We take first avilable ammo item from inventory and get ammo from it

                if(ammoItems.Count > index && ammoItems[index] != null && ammoItems[index].ammo > 0) //If object exist and have ammo more than 0
                {
                    //Carefuly working with inventory ammo
                    ammoItems[index].ammo -= 1;
                    currentAmmo += 1;
                }
                else
                {
                    try
                    {
                        if (ammoItems[index] != null)
                            inventory.RemoveItem(ammoItems[index]);
                    }
                    catch
                    {
                        break;
                    }
                    
                    if (index < ammoItems.Count)
                    {
                        index++;
                    }
                    else
                    {
                        break;
                    }
                }
            }*/

                reloading = false;
                canShot = true;

                if (setAim && useAnimator)
                {
                    animator.SetBool("Aim", true);
                }
            
        }

#region Decal, projectiles, shot FX, hitFX managers

        public void HitParticlesFXManager(RaycastHit hit)
        {
            if (hit.collider.tag == "Wood")
            {
                hitFXManager.objWoodHitFX.Stop();
                hitFXManager.objWoodHitFX.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                hitFXManager.objWoodHitFX.transform.LookAt(cam.transform.position);
                hitFXManager.objWoodHitFX.Play(true);
            }
            else if (hit.collider.tag == "Concrete")
            {
                hitFXManager.objConcreteHitFX.Stop();
                hitFXManager.objConcreteHitFX.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                hitFXManager.objConcreteHitFX.transform.LookAt(cam.transform.position);
                hitFXManager.objConcreteHitFX.Play(true);
            }
            else if (hit.collider.tag == "Dirt")
            {
                hitFXManager.objDirtHitFX.Stop();
                hitFXManager.objDirtHitFX.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                hitFXManager.objDirtHitFX.transform.LookAt(cam.transform.position);
                hitFXManager.objDirtHitFX.Play(true);
            }
            else if (hit.collider.tag == "Metal")
            {
                hitFXManager.objMetalHitFX.Stop();
                hitFXManager.objMetalHitFX.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                hitFXManager.objMetalHitFX.transform.LookAt(cam.transform.position);
                hitFXManager.objMetalHitFX.Play(true);
            }
            else if (hit.collider.tag == "Flesh")
            {
                hitFXManager.objBloodHitFX.Stop();
                hitFXManager.objBloodHitFX.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                hitFXManager.objBloodHitFX.transform.LookAt(cam.transform.position);
                hitFXManager.objBloodHitFX.Play(true);
            }
            else
            {
                hitFXManager.objConcreteHitFX.Stop();
                hitFXManager.objConcreteHitFX.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                hitFXManager.objConcreteHitFX.transform.LookAt(cam.transform.position);
                hitFXManager.objConcreteHitFX.Play(true);
            }

        }

        public void DecalManager(RaycastHit hit, bool applyParent)
        {
            if (hit.collider.CompareTag("Concrete"))
            {
                hitFXManager.concreteDecal_pool[decalIndex_concrete].SetActive(true);
                var decalPostion = hit.point + hit.normal * 0.025f;
                hitFXManager.concreteDecal_pool[decalIndex_concrete].transform.position = decalPostion;
                hitFXManager.concreteDecal_pool[decalIndex_concrete].transform.rotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);
                if (applyParent)
                    decals[decalIndex_concrete].transform.parent = hit.transform;

                decalIndex_concrete++;

                if (decalIndex_concrete == hitFXManager.decalsPoolSizeForEachType)
                {
                    decalIndex_concrete = 0;
                }
            }
            else if (hit.collider.CompareTag("Wood"))
            {
                hitFXManager.woodDecal_pool[decalIndex_wood].SetActive(true);
                var decalPostion = hit.point + hit.normal * 0.025f;
                hitFXManager.woodDecal_pool[decalIndex_wood].transform.position = decalPostion;
                hitFXManager.woodDecal_pool[decalIndex_wood].transform.rotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);
                if (applyParent)
                    decals[decalIndex_wood].transform.parent = hit.transform;

                decalIndex_wood++;

                if (decalIndex_wood == hitFXManager.decalsPoolSizeForEachType)
                {
                    decalIndex_wood = 0;
                }
            }
            else if (hit.collider.CompareTag("Dirt"))
            {
                hitFXManager.dirtDecal_pool[decalIndex_dirt].SetActive(true); var decalPostion = hit.point + hit.normal * 0.025f;
                hitFXManager.dirtDecal_pool[decalIndex_dirt].transform.position = decalPostion;
                hitFXManager.dirtDecal_pool[decalIndex_dirt].transform.rotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);
                if (applyParent)
                    decals[decalIndex_dirt].transform.parent = hit.transform;

                decalIndex_dirt++;

                if (decalIndex_dirt == hitFXManager.decalsPoolSizeForEachType)
                {
                    decalIndex_dirt = 0;
                }
            }
            else if (hit.collider.CompareTag("Metal"))
            {
                hitFXManager.metalDecal_pool[decalIndex_metal].SetActive(true);
                var decalPostion = hit.point + hit.normal * 0.025f;
                hitFXManager.metalDecal_pool[decalIndex_metal].transform.position = decalPostion;
                hitFXManager.metalDecal_pool[decalIndex_metal].transform.rotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);
                if (applyParent)
                    decals[decalIndex_metal].transform.parent = hit.transform;

                decalIndex_metal++;

                if (decalIndex_metal == hitFXManager.decalsPoolSizeForEachType)
                {
                    decalIndex_metal = 0;
                }
            }
            else
            {
                hitFXManager.concreteDecal_pool[decalIndex_concrete].SetActive(true);
                var decalPostion = hit.point + hit.normal * 0.025f;
                hitFXManager.concreteDecal_pool[decalIndex_concrete].transform.position = decalPostion;
                hitFXManager.concreteDecal_pool[decalIndex_concrete].transform.rotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);
                if (applyParent)
                    decals[decalIndex_concrete].transform.parent = hit.transform;

                decalIndex_concrete++;

                if (decalIndex_concrete == hitFXManager.decalsPoolSizeForEachType)
                {
                    decalIndex_concrete = 0;
                }
            }
        }

        public void ProjectilesManager()
        {
            var spreadWithMovementFactor = spread + movementSpreadFactor;

            ///Make lower spread factor if player is aiming
            if (setAim)
                spreadWithMovementFactor /= 3;

            Vector3 spreadVector = new Vector3(Random.Range(-spreadWithMovementFactor, spreadWithMovementFactor), Random.Range(-spreadWithMovementFactor, spreadWithMovementFactor), Random.Range(-spreadWithMovementFactor, spreadWithMovementFactor));

            if (distanceToPlayer > 1 && !weaponManager.UseNonPhysicalReticle && !setAim)
            {
                projectiles[projectilesIndex].transform.position = bulletTransform.position;
                projectiles[projectilesIndex].transform.rotation = bulletTransform.rotation;
                //projectiles[projectilesIndex].transform.Rotate(spreadVector);
            }
            else
            {
                projectiles[projectilesIndex].transform.position = Camera.main.transform.position;
                projectiles[projectilesIndex].transform.rotation = Camera.main.transform.rotation;
                //projectiles[projectilesIndex].transform.Rotate(spreadVector);
            }

            if (weaponType == WeaponType.SniperRiffle && setAim)
            {
                projectiles[projectilesIndex].transform.position = Camera.main.transform.position;
                projectiles[projectilesIndex].transform.rotation = Camera.main.transform.rotation;
            }

            projectiles[projectilesIndex].SetActive(true);

            projectilesIndex++;

            if (projectilesIndex == projectiles.Length)
            {
                projectilesIndex = 0;
            }
        }

        private void PlayFX()
        {
            if (useAnimator)
                animator.Play("Shot");

            temp_MuzzleFlashParticlesFX.time = 0;
            temp_MuzzleFlashParticlesFX.Play();

            audioSource.Stop();
            audioSource.PlayOneShot(shotSFX);

            if (shells != null)
            {
                if (weaponType != WeaponType.SniperRiffle)
                {
                    shells[shellIndex].GetComponent<Rigidbody>().velocity = Vector3.zero;
                    shells[shellIndex].SetActive(true);
                    shells[shellIndex].transform.localPosition = shellTransform.transform.position;
                    shells[shellIndex].transform.localRotation = shellTransform.transform.rotation;
                    shells[shellIndex].GetComponent<Rigidbody>().AddForce(shellTransform.transform.right * shellForce, ForceMode.Force);
                    shellIndex++;
                }

                if (shellIndex == shells.Length)
                {
                    shellIndex = 0;
                }
            }
        }

        public void RicochetSFX()
        {
            ricochetSource.Stop();
            ricochetSource.PlayOneShot(ricochetSounds[Random.Range(0, ricochetSounds.Length)]);
        }

#endregion

#region Pool methods

        public void ShellsPool()
        {
            shells = new GameObject[shellPoolSize];
            var shellsParentObject = new GameObject(weaponName + "_shellsPool");

            for (int i = 0; i < shellPoolSize; i++)
            {
                shells[i] = Instantiate(shell);
                shells[i].SetActive(false);
                shells[i].transform.parent = shellsParentObject.transform;
            }
        }

        public void BalisticProjectilesPool()
        {
            projectiles = new GameObject[projectilePoolSize];

            var projectileSettingObject = Instantiate(projectile);
            projectileSettingObject.SetActive(false);
            projectileSettingObject.GetComponentInChildren<BalisticProjectile>().weapon = this;
            projectileSettingObject.GetComponentInChildren<BalisticProjectile>().initialVelocity = bulletInitialVelocity;
            projectileSettingObject.GetComponentInChildren<BalisticProjectile>().airResistance = airResistanceForce;

            var projectilesParentObject = new GameObject(weaponName + "_projectilesPool" + " " + weaponName);

            for (int i = 0; i < projectilePoolSize; i++)
            {
                projectiles[i] = Instantiate(projectileSettingObject);
                projectiles[i].SetActive(false);
                projectiles[i].transform.parent = projectilesParentObject.transform;
            }
        }

#endregion

#region Utility methods

        public void SetAim()
        {
            if (!reloading && useAnimator)
            {
                animator.SetBool("Aim", setAim);

            }
            else
            {
                setAim = false;
            }

            scopeActivateTimer -= Time.deltaTime;

            if (useScope && setAim)
            {
                if (scopeActivateTimer <= 0)
                {
                    if (!scope.activeSelf)
                    {
                        normalSensX = controller.sensitivity.x;
                        normalSensY = controller.sensitivity.y;
                    }
                    scope.SetActive(true);
                    cam.fieldOfView = scopeFOV;
                    controller.sensitivity.x = scopeSensitivityX;
                    controller.sensitivity.y = scopeSensitivityY;
                }
            }
            else
            {
                cam.fieldOfView = normalFOV;
                if (scope.activeSelf)
                {
                    controller.sensitivity.x = normalSensX;
                    controller.sensitivity.y = normalSensY;
                }
                scope.SetActive(false);
                scopeActivateTimer = scopeTimer;
            }
        }

        public void UpdateCrosshairPosition()
        {
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000))
            {
                distanceToPlayer = Vector3.Distance(cam.transform.position, hit.point);

                if (distanceToPlayer > 1)
                {
                    RaycastHit muzzleHit;
                    if (Physics.Raycast(bulletTransform.position, bulletTransform.forward, out muzzleHit, 1000))
                        if (dynamicReticle != null)
                        {
                            dynamicReticle.transform.position = cam.WorldToScreenPoint(muzzleHit.point);
                        }
                }
                else
                {
                    var screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
                    if (dynamicReticle != null)
                    {
                        dynamicReticle.transform.position = screenCenter;
                    }
                }
            }
            else
            {
                var screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
                if (dynamicReticle != null)
                {
                    dynamicReticle.transform.position = screenCenter;
                }
            }
        }
        
        private void FireModeSwitch()
        {
            if (Input.GetKeyDown(input.FiremodeAuto))
            {
                fireMode = FireMode.automatic;
            }
            if(Input.GetKeyDown(input.FiremodeSingle))
            {
                fireMode = FireMode.single;
            }
        }

        public void UpdateAmmoText()
        {
            if (!ammoText.enabled)
                ammoText.enabled = true;

            char mode = new char();

            if(fireMode == FireMode.single)
            {
                mode = '1';
            }
            else
            {
                mode = 'A';
            }
            
            weaponNameText.text = weaponName;
            ammoText.text = string.Format("{0}|{1}\n Mode:{2}", currentAmmo, CalculateTotalAmmo(), mode);
            
        }
        //
        public int CalculateTotalAmmo()
        {
            int totalAmmo = new int();

            /*foreach (var item in inventory.characterItems)
            {
                if (item.id == ammoItemID)
                {
                    totalAmmo += item.ammo;
                }
            }*/
            if (this == weaponManager.M4)
            {
                totalAmmo = weaponManager.gd.dataPlayer.M4Ammo;
            }
            else if (this == weaponManager.SCAR)
            {
                totalAmmo = weaponManager.gd.dataPlayer.scarAmmo;
            }
            else if (this == weaponManager.Sniper)
            {
                totalAmmo = weaponManager.gd.dataPlayer.SniperAmmo;
            }
            if (weaponManager.PrimaryGun == this) // this
            {
                totalAmmo = _totalAmmo;
            }
            return totalAmmo;
        }

        public List<Item> GetAmmoItems()
        {
            var items = new List<Item>();

            foreach (var item in inventory.characterItems)
            {
                if (item.id == ammoItemID)
                    items.Add(item);
            }

            return items;
        }

#endregion

        private void OnDisable()
        {
            if (animator != null)
            {
                if (animator.gameObject.activeSelf)
                {
                    CancelInvoke();
                    animator.StopPlayback();
                    animator.Rebind();
                    canShot = true;
                    reloading = false;
                    if (weaponNameText)
                        weaponNameText.text = "";
                    if (ammoText)
                        ammoText.text = "";
                }
            }
        }
    }
}
