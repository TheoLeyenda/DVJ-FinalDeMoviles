/// DarkTreeDevelopment (2019) DarkTree FPS v1.2
/// If you have any questions feel free to write me at email --- darktreedevelopment@gmail.com ---
/// Thanks for purchasing my asset!

using UnityEngine;
using System.Collections.Generic;

namespace DarkTreeFPS
{
    public class WeaponManager : MonoBehaviour
    {
        //A public list which get all aviliable weapons on Start() and operate with them
        public List<Weapon> weapons;

        public bool UseNonPhysicalReticle = true;

        public bool haveMeleeWeaponByDefault = true;
        [HideInInspector]
        public Weapon melleeDefaultWeapon;
        [HideInInspector]
        public Weapon grenade;
        [HideInInspector]
        public Weapon PrimaryGun;
        [HideInInspector]
        public Weapon M4;
        [HideInInspector]
        public Weapon SCAR;
        [HideInInspector]
        public Weapon Sniper;

        [Header("Weapons PC")]
        public Weapon melleeDefaultWeaponPC;
        public Weapon grenadePC;
        public Weapon PrimaryGunPC;
        public Weapon M4PC;
        public Weapon SCARPC;
        public Weapon SniperPC;

        [Header("Weapons Android")]
        public Weapon melleeDefaultWeaponAndroid;
        public Weapon grenadeAndroid;
        public Weapon PrimaryGunAndroid;
        public Weapon M4Android;
        public Weapon SCARAndroid;
        public Weapon SniperAndroid;

        public int ammoPrimaryGun;
        public int ammoM4;
        public int ammoSCAR;
        public int ammoSniper;

        public bool enableGrenade;
        public List<Slot> slots;
        [Range(1, 9)]

        private int slotsSize = 4;
        private bool isPointer = false;
        public int switchSlotIndex = 0;
        public int currentWeaponIndex;
        public Slot activeSlot;
        private bool once = false;
        //public Weapon primarySlot;
        //public Weapon secondarySlot;

        [Tooltip("Scope image used for riffle aiming state")]
        public GameObject scopeImage;
        [Tooltip("Crosshair image")]
        public GameObject reticleDynamic;
        public GameObject reticleStatic;

        [Tooltip("Animator that contain pickup animation")]
        public Animator weaponHolderAnimator;

        [HideInInspector]
        public GameObject tempGameobject;

        //Transform where weapons will dropped on Drop()
        private Transform playerTransform;

        private Transform swayTransform;

        private Inventory inventory;

        [HideInInspector]
        public GameData gd;

        private GameManager gm;

        private int indexSlot = 0;

        public bool enableShoot = false;
        [HideInInspector]
        //public Weapon currentWeapon;

        public void ON()
        {
            if (!once)
            {
                gm = GameObject.Find("GamePrefab").GetComponent<GameManager>();
                if (gm.InTutorial)
                {
                    enableShoot = false;
                }
                else
                {
                    enableShoot = true;
                }
                Sway swayObject = FindObjectOfType<Sway>();
                if (swayObject != null)
                {
                    swayTransform = FindObjectOfType<Sway>().GetComponent<Transform>();
                }

                if (swayTransform != null)
                {
                    for (int i = 0; i < slotsSize; i++)
                    {
                        Slot slot_temp = gameObject.AddComponent<Slot>();

                        slots.Add(slot_temp);
                    }

                    if (haveMeleeWeaponByDefault)
                    {
                        slots[0].storedWeapon = melleeDefaultWeapon;
                        activeSlot = slots[0];
                    }
                    else
                    {
                        slots[indexSlot].storedWeapon = PrimaryGun;
                        activeSlot = slots[indexSlot];
                        slots[indexSlot].storedWeapon.currentAmmo = 0;
                        //AGREGO LAS ARMAS COMPRADAS EN LA TIENDA
                        if (gd.dataPlayer.unlockedM4)
                        {
                            indexSlot++;
                            if (indexSlot < slots.Count)
                            {
                                M4.currentAmmo = 30;
                                M4._totalAmmo = gd.dataPlayer.M4Ammo;
                                slots[indexSlot].storedWeapon = M4;
                            }
                        }
                        if (gd.dataPlayer.unlockedScar)
                        {
                            indexSlot++;
                            if (indexSlot < slots.Count)
                            {
                                SCAR.currentAmmo = 30;
                                SCAR._totalAmmo = gd.dataPlayer.scarAmmo;
                                slots[indexSlot].storedWeapon = SCAR;
                            }
                        }
                        if (gd.dataPlayer.unlockedSniper)
                        {
                            indexSlot++;
                            if (indexSlot < slots.Count)
                            {
                                Sniper.currentAmmo = 10;
                                Sniper._totalAmmo = gd.dataPlayer.SniperAmmo;
                                slots[indexSlot].storedWeapon = Sniper;

                            }
                        }
                    }
                    if (enableGrenade)
                    {
                        if (grenade != null)
                        {
                            indexSlot++;
                            if (indexSlot < slots.Count)
                            {
                                slots[indexSlot].storedWeapon = grenade;
                            }
                        }
                    }

                    scopeImage.SetActive(false);

                    if (UseNonPhysicalReticle)
                    {
                        reticleStatic.SetActive(true);
                        reticleDynamic.SetActive(false);
                    }
                    else
                    {
                        reticleStatic.SetActive(false);
                        reticleDynamic.SetActive(true);
                    }

                    playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

                    foreach (Weapon weapon in swayTransform.GetComponentsInChildren<Weapon>(true))
                    {
                        weapons.Add(weapon);
                    }

                    inventory = FindObjectOfType<Inventory>();
                    switchSlotIndex = 0;
                    SlotChange();
                }
                once = true;
            }
        }
        private void OnEnable()
        {
            ON();
        }
        private void Awake()
        {
            //ON();

#if UNITY_ANDROID
            melleeDefaultWeapon = melleeDefaultWeaponAndroid;
            grenade = grenadeAndroid;
            PrimaryGun = PrimaryGunAndroid;
            M4 = M4Android;
            SCAR = SCARAndroid;
            Sniper = SniperAndroid;
#endif
#if UNITY_STANDALONE
            melleeDefaultWeapon = melleeDefaultWeaponPC;
            grenade = grenadePC;
            PrimaryGun = PrimaryGunPC;
            M4 = M4PC;
            SCAR = SCARPC;
            Sniper = SniperPC;
#endif
            gd = GameData.instaceGameData;
            /*if(gd == null)
            {
                Debug.Log("gd is null");
            }*/
        }
        public void CheckEnableShoot()
        {
            if (!enableShoot)
            {
                PrimaryGun.currentAmmo = 0;
            }
            else
            {
                PrimaryGun._totalAmmo = 20;
                PrimaryGun._totalAmmo = 120;
            }
        }
        private void Update()
        {
            //Check if we are trying to switch weapons we have
            SlotInput();
            if (Input.GetKeyDown(KeyCode.G))
            {
                DropWeapon();
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                DropAllWeapons();
            }
            CheckAimCurrentWeapon();
            CheckEnableShoot();
        }
        public void FireCurrentWeapon()
        {
            //DISPARO
            if (activeSlot.storedWeapon.weaponName != "Knife")
            {
                if (Input.GetKey(activeSlot.storedWeapon.input.Fire) && !PlayerStats.isPlayerDead && activeSlot.storedWeapon.weaponType != WeaponType.Pistol && !InventoryManager.showInventory && activeSlot.storedWeapon.fireMode == Weapon.FireMode.automatic)  //Statement to restrict auto-fire for pistol weapon type. Riffle and others are automatic
                {
                    Debug.Log("ENTRE");
                    activeSlot.storedWeapon.Fire();
                }
                else if (Input.GetKeyDown(activeSlot.storedWeapon.input.Fire) && !PlayerStats.isPlayerDead && (activeSlot.storedWeapon.weaponType == WeaponType.Pistol || activeSlot.storedWeapon.fireMode == Weapon.FireMode.single) && !InventoryManager.showInventory)
                {
                    activeSlot.storedWeapon.Fire();
                }
            }
        }
        public void CheckAimCurrentWeapon()
        {
            if (isPointer)
            {
                if (activeSlot.storedWeapon.weaponName != "Knife")
                    activeSlot.storedWeapon.ActiveAim();
            }
            else
            {
                if (activeSlot.storedWeapon.weaponName != "Knife")
                    activeSlot.storedWeapon.DisableAim();
            }
        }
        public void SetPointer()
        { 
            if (activeSlot.storedWeapon.weaponName != "Knife")
                isPointer = !isPointer;
        }
        public void ReloadCurrentWeapon()
        {
            if (activeSlot.storedWeapon.weaponName != "Knife")
            {
                //RECARGA
                if (activeSlot.storedWeapon.weaponType != WeaponType.Melee && activeSlot.storedWeapon.weaponType != WeaponType.Grenade)
                {
                    //Reloading consists of two stages ReloadBegin and ReloadEnd  
                    //ReloadBegin method play animation and soundFX and also restrict weapon shooting. ReloadingEnd removes restriction and add ammo to weapon
                    //See more in methods below
                    //if (Input.GetKeyDown(activeSlot.storedWeapon.input.Reload) || activeSlot.storedWeapon.currentAmmo < 0)
                    //{
                        if (!activeSlot.storedWeapon.reloading && !activeSlot.storedWeapon.controller.isClimbing)
                            activeSlot.storedWeapon.ReloadBegin();
                    //}

                    if (Input.GetKey(activeSlot.storedWeapon.input.Aim))
                    {
                       
                        activeSlot.storedWeapon.setAim = true;
                        activeSlot.storedWeapon.sway.xSwayAmount = activeSlot.storedWeapon.sway.xSwayAmount * 0.3f;
                        activeSlot.storedWeapon.sway.ySwayAmount = activeSlot.storedWeapon.sway.ySwayAmount * 0.3f;
                        if (UseNonPhysicalReticle)
                            activeSlot.storedWeapon.staticReticle.SetActive(false);
                        else
                            activeSlot.storedWeapon.dynamicReticle.gameObject.SetActive(false);
                    }
                    else
                    {
                        activeSlot.storedWeapon.setAim = false;
                        activeSlot.storedWeapon.sway.xSwayAmount = activeSlot.storedWeapon.sway.startX;
                        activeSlot.storedWeapon.sway.ySwayAmount = activeSlot.storedWeapon.sway.startY;
                        if (UseNonPhysicalReticle)
                            activeSlot.storedWeapon.staticReticle.SetActive(true);
                        else
                            activeSlot.storedWeapon.dynamicReticle.gameObject.SetActive(true);
                    }


                    activeSlot.storedWeapon.SetAim();
                    activeSlot.storedWeapon.UpdateAmmoText();

                    if (activeSlot.storedWeapon.canUpdateCrosshair && !UseNonPhysicalReticle)
                        activeSlot.storedWeapon.UpdateCrosshairPosition();
                }
            }
        }
        public Slot FindFreeSlot()
        {
            foreach (Slot slot in slots)
            {
                if (slot.IsFree())
                    return slot;
            }

            return null;
        }
        
        private void SlotInput()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                switchSlotIndex++;
                if (switchSlotIndex < slotsSize)
                {
                    if (slots[switchSlotIndex] != null)
                    {
                        SlotChange();
                    }
                    if (slots[switchSlotIndex].storedWeapon == null)
                    {
                        switchSlotIndex = 0;
                        SlotChange();
                    }
                }
                else
                {
                    switchSlotIndex = 0;
                    SlotChange();
                }
                
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                switchSlotIndex--;
                if (switchSlotIndex >= 0)
                {
                    SlotChange();
                }
                else if (switchSlotIndex < 0)
                {
                    switchSlotIndex = slotsSize;
                    SlotChange();
                }
                
            }

            if (Input.GetButtonDown("Slot0")) { switchSlotIndex = 0; SlotChange(); }
            else if (Input.GetButtonDown("Slot1")) { switchSlotIndex = 1; SlotChange(); }
            else if (Input.GetButtonDown("Slot2")) { switchSlotIndex = 2; SlotChange(); }
            else if (Input.GetButtonDown("Slot3")) { switchSlotIndex = 3; SlotChange(); }
            else if (Input.GetButtonDown("Slot4")) { switchSlotIndex = 4; SlotChange(); }
            else if (Input.GetButtonDown("Slot5")) { switchSlotIndex = 5; SlotChange(); }
            else if (Input.GetButtonDown("Slot6")) { switchSlotIndex = 6; SlotChange(); }
            else if (Input.GetButtonDown("Slot7")) { switchSlotIndex = 7; SlotChange(); }
            else if (Input.GetButtonDown("Slot8")) { switchSlotIndex = 8; SlotChange(); }
        }

        public void ShowGrenade()
        {
            //print("Show grenade");
            HideAll();
            //activeSlot = slots[3];
            grenade.gameObject.SetActive(true);
        }

        private void SlotChange()
        {
            //print("Slot change");
            if (grenade.gameObject.activeInHierarchy)
                grenade.gameObject.SetActive(false);

            //print("Slot change 2");
            if (activeSlot != null && activeSlot.storedWeapon != null)
            {///////  012            /// 012     
                if (slots.Count > switchSlotIndex)
                {
                    if (slots[switchSlotIndex].storedWeapon != null)
                    {
                        activeSlot.storedWeapon.gameObject.SetActive(false);

                        activeSlot = null;
                        activeSlot = slots[switchSlotIndex];
                        activeSlot.storedWeapon.gameObject.SetActive(true);

                        //print("I equip slot number" + switchSlotIndex);

                        weaponHolderAnimator.Play("Unhide");
                    }
                }
                else
                    return;
            }
            else
                return;
        }

        //EquipWeapon is called from Item class on pickup. Item class passes arguments to EquipWeapon
        public bool IsWeaponAlreadyPicked(string weaponName)
        {
            foreach (Slot slot in slots)
            {
                if (slot.storedWeapon != null && slot.storedWeapon.weaponName == weaponName)
                    return true;
            }
            return false;
        }

        public Weapon FindWeaponByName(string name)
        {
            foreach (Slot slot in slots)
            {
                if (slot.storedWeapon.weaponName == name)
                    return slot.storedWeapon;
            }

            return null;
        }

        public void EquipWeapon(string weaponName, GameObject temp)
        {
            //print(temp.name);

            grenade.gameObject.SetActive(false);

            if (!IsWeaponAlreadyPicked(weaponName))
            {
                if (FindFreeSlot() != null)
                {
                    if (activeSlot != null)
                        activeSlot.storedWeapon.gameObject.SetActive(false);

                    activeSlot = FindFreeSlot();

                    foreach (Weapon weapon in weapons)
                    {
                        if (weapon.weaponName == weaponName)
                        {
                            activeSlot.storedWeapon = weapon;
                            //print(activeSlot.storedWeapon.currentAmmo);
                            //print(temp.GetComponent<WeaponPickup>().ammoInWeaponCount);
                            activeSlot.storedWeapon.currentAmmo = temp.GetComponent<WeaponPickup>().ammoInWeaponCount;
                            activeSlot.storedDropObject = temp;
                            activeSlot.storedDropObject.SetActive(false);
                            temp = null;
                            activeSlot.storedWeapon.gameObject.SetActive(true);

                            switchSlotIndex = switchSlotIndex + 1;

                            weaponHolderAnimator.Play("Unhide");

                            break;
                        }
                    }

                }
            }
            else
            {
                Weapon weapon_temp = FindWeaponByName(weaponName);
                
                //temp.SetActive(false);
                //temp = null;
                //print("Weapon already exist");
            }
        }

        private Slot FindEquipedSlot()
        {
            foreach (Slot slot in slots)
            {
                if (!slot.IsFree())
                    return slot;
            }

            return null;
        }

        public void HideWeaponOnDeath()
        {
            weaponHolderAnimator.SetLayerWeight(1, 0);
            weaponHolderAnimator.SetBool("HideWeapon", true);
        }

        public void UnhideWeaponOnRespawn()
        {
            if (UseNonPhysicalReticle)
            {
                reticleStatic.SetActive(true);
                reticleDynamic.SetActive(false);
            }
            else
            {
                reticleStatic.SetActive(false);
                reticleDynamic.SetActive(true);
            }

            weaponHolderAnimator.SetLayerWeight(1, 1);
            weaponHolderAnimator.SetBool("HideWeapon", false);
        }
        
        public void HideAll()
        {
            print("Hide weapon works");

            if (activeSlot != null)
            {
                    activeSlot.storedWeapon.gameObject.SetActive(false);
                    
                    weaponHolderAnimator.Play("HideWeapon");
            }
        }

        public void UnhideWeaponAfterGrenadeDrop()
        {
            grenade.gameObject.SetActive(false);

            if(activeSlot != null)
            {
                weaponHolderAnimator.Play("Unhide");
                activeSlot.storedWeapon.gameObject.SetActive(true);
            }
        }

        public void DropAllWeapons()
        {
            weaponHolderAnimator.SetLayerWeight(1, 0);
            weaponHolderAnimator.SetBool("HideWeapon", true);

            foreach (Slot slot in slots)
            {
                if (!slot.IsFree())
                {
                    if (slot.storedWeapon.weaponType != WeaponType.Melee && haveMeleeWeaponByDefault)
                    {
                        if (slot.storedWeapon == activeSlot.storedWeapon)
                        {
                            DropWeapon();
                        }
                        else
                        {
                            slot.storedWeapon.gameObject.SetActive(false);
                            if (slot.storedDropObject)
                            {
                                slot.storedDropObject.SetActive(true);
                                slot.storedDropObject.transform.position = playerTransform.transform.position + playerTransform.forward * 0.5f;
                                slot.storedDropObject = null;
                                slot.storedWeapon = null;
                            }
                        }
                    }
                }
            }

            if (haveMeleeWeaponByDefault)
            {
                activeSlot = slots[0];
                activeSlot.storedWeapon.gameObject.SetActive(true);
            }
        }

        public void ChangeWeaponMobile(bool equipMelee)
        {
            print("I work!");

            if (!equipMelee)
            {
                print("I equip no melee");

                if (slots[1].storedWeapon != null && slots[2].storedWeapon != null)
                {
                    if (activeSlot.storedWeapon == slots[2].storedWeapon)
                    {
                        HideAll();
                        switchSlotIndex = 1;
                        SlotChange();
                    }
                    else if (activeSlot.storedWeapon == slots[1].storedWeapon)
                    {
                        HideAll();
                        switchSlotIndex = 2;
                        SlotChange();
                    }
                    else if (activeSlot.storedWeapon == slots[0].storedWeapon)

                    {
                        if (slots[1].storedWeapon != null)
                        {
                            HideAll();
                            switchSlotIndex = 1;
                            SlotChange();
                        }
                        else if (slots[2].storedWeapon != null)
                        {
                            HideAll();
                            switchSlotIndex = 2;
                            SlotChange();
                        }
                    }
                }
                else if (activeSlot.storedWeapon == slots[0].storedWeapon)
                {
                    if (slots[1].storedWeapon != null)
                    {
                        HideAll();
                        switchSlotIndex = 1;
                        SlotChange();
                    }
                    else if(slots[2].storedWeapon != null)
                    {
                        HideAll();
                        switchSlotIndex = 2;
                        SlotChange();
                    }
                }
                
            }
            else if(equipMelee && haveMeleeWeaponByDefault)
            {
                //print("I equip melee");
                
                HideAll();
                switchSlotIndex = 0;
                SlotChange();
            }
        }

        public void DropWeapon()
        {
            if (activeSlot != null)
            {
                if (activeSlot != slots[0] && haveMeleeWeaponByDefault)
                {
                    activeSlot.storedDropObject.GetComponent<WeaponPickup>().ammoInWeaponCount = activeSlot.storedWeapon.currentAmmo;
                    activeSlot.storedWeapon.gameObject.SetActive(false);
                    activeSlot.storedDropObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
                    activeSlot.storedDropObject.SetActive(true);
                    activeSlot.storedDropObject = null;
                    activeSlot.storedWeapon = null;
                    activeSlot = FindEquipedSlot();

                    if (activeSlot != null)
                        activeSlot.storedWeapon.gameObject.SetActive(true);

                    weaponHolderAnimator.Play("Unhide");
                }
            }
        }
        
        public void DropWeaponFromSlot(int slot)
        {
            if(activeSlot.storedWeapon == slots[slot].storedWeapon)
            {
                DropWeapon();
            }
            else
            {
                slots[slot].storedDropObject.GetComponent<WeaponPickup>().ammoInWeaponCount = slots[slot].storedWeapon.currentAmmo;
                slots[slot].storedDropObject.transform.position = playerTransform.position + playerTransform.forward * 0.5f;
                slots[slot].storedDropObject.SetActive(true);
                slots[slot].storedDropObject = null;
                slots[slot].storedWeapon = null;
            }
        }
    }
}
