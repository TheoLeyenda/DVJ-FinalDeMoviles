/// DarkTreeDevelopment (2019) DarkTree FPS v1.2
/// If you have any questions feel free to write me at email --- darktreedevelopment@gmail.com ---
/// Thanks for purchasing my asset!

using UnityEngine;
using UnityEngine.UI;

namespace DarkTreeFPS
{
    public class UseObjects : MonoBehaviour
    {
        [Tooltip("The distance within which you can pick up item")]
        public float distance = 10f;
        private bool grab;
        private GameObject use;
        private GameObject useCursor;
        private Text useText;

        private InputManager input;
        private Inventory inventory;

        private Button useButton;
        public GameObject buttonPickUp;
        private void Start()
        {
            grab = false;
            useCursor = GameObject.Find("UseCursor");
            useText = useCursor.GetComponentInChildren<Text>();
            useCursor.SetActive(false);

            inventory = FindObjectOfType<Inventory>();
            input = FindObjectOfType<InputManager>();
            
        }

        void Update()
        {
            Pickup();
        }

        public void SetGrab(bool _grab)
        {
            grab = _grab;
        }

        public void Pickup()
        {
            RaycastHit hit;

            //Hit an object within pickup distance
            if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
            {
                if (hit.collider.tag == "Item")
                {
                    //Get an item which we want to pickup
                    use = hit.collider.gameObject;
                    useCursor.SetActive(true);
                    

                    if (use.GetComponent<Item>())
                    {
                        if(buttonPickUp != null)
                            buttonPickUp.SetActive(true);
                        useText.text = use.GetComponent<Item>().title;

                        if (Input.GetKeyDown(input.Use)  || grab)
                        {
                            inventory.GiveItem(use.GetComponent<Item>());
                            use = null;
                            grab = false;
                        }
                        
                            
                    }
                    //useText.text = use.weaponNameToAddAmmo + " Ammo x " + use.ammoQuantity;
                    else if (use.GetComponent<WeaponPickup>()) {

                        if (buttonPickUp != null)
                            buttonPickUp.SetActive(true);

                        useText.text = use.GetComponent<WeaponPickup>().weaponNameToEquip;
                        
                        if (Input.GetKeyDown(input.Use) || grab)
                        {
                            use.GetComponent<WeaponPickup>().Pickup();
                            grab = false;
                        }
                    }
                }
                else
                {
                    //Clear use object if there is no an object with "Item" tag
                    use = null;
                    useCursor.SetActive(false);
                    if (buttonPickUp != null)
                        buttonPickUp.SetActive(false);
                    useText.text = "";
                }
            }
            else
            {
                useCursor.SetActive(false);
                if(buttonPickUp != null)
                    buttonPickUp.SetActive(false);
                useText.text = "";
            }
        }
    }
    
}
