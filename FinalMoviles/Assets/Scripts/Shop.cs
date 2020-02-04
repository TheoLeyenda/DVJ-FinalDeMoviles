using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    public enum ItemsShoop
    {
        M4,
        AmmoM4,
        SCAR,
        AmmoSCAR,
        Sniper,
        AmmoSniper,
        NukePowerUp,
        LifeUpPowerUp,
        RepairConstructionPowerUp,
        MedikitPowerUp,
        IcePowerUp,
        MeteoroPowerUp,
        Count,
    }
    [System.Serializable]
    public class ItemShoop
    {
        public string name;
        public int price;
        public int count;
        //public Sprite spriteInShop;
        //public Sprite spriteOutStok;
        //public Image image;
        public Button button;
        public bool outStock;
        //public int countStock;
        public Text textCount;
        public Text textPrice;

        public void CheckInShop()
        {
            if (outStock)
            {
                button.interactable = false;
                /*if (spriteOutStok != null)
                {
                    image.sprite = spriteOutStok;
                }*/
            }
            else if (!outStock)
            {
                button.interactable = true;
                /*if (spriteInShop != null)
                {
                    image.sprite = spriteInShop;
                }*/
            }
        }
    }
    public List<ItemShoop> itemsShop;
    private GameData gd;
    public void CheckItems()
    {
        for (int i = 0; i < itemsShop.Count; i++)
        {
            if (gd.dataPlayer.unlockedM4 && itemsShop[i].name == "M4"
                || gd.dataPlayer.unlockedScar && itemsShop[i].name == "SCAR"
                || gd.dataPlayer.unlockedSniper && itemsShop[i].name == "Sniper"
                || gd.dataPlayer.scarAmmo >= gd.dataPlayer.maxAmmoScar && itemsShop[i].name == "AmmoSCAR"
                || gd.dataPlayer.M4Ammo >= gd.dataPlayer.maxAmmoM4 && itemsShop[i].name == "AmmoM4"
                || gd.dataPlayer.SniperAmmo >= gd.dataPlayer.maxAmmoSniper && itemsShop[i].name == "AmmoSniper"
                || gd.dataPlayer.countIcePowerUp >= gd.dataPlayer.maxCountIcePowerUp && itemsShop[i].name == "IcePowerUp"
                || gd.dataPlayer.countLifeUpPowerUp >= gd.dataPlayer.maxCountLifeUpPowerUp && itemsShop[i].name == "LifeUpPowerUp"
                || gd.dataPlayer.countMedikitPowerUp >= gd.dataPlayer.maxCountMedikitPowerUp && itemsShop[i].name == "MedikitPowerUp"
                || gd.dataPlayer.countMeteoroPowerUp >= gd.dataPlayer.maxCountMeteoroPowerUp && itemsShop[i].name == "MeteoroPowerUp"
                || gd.dataPlayer.countNukePowerUp >= gd.dataPlayer.maxCountNukePowerUp && itemsShop[i].name == "NukePowerUp"
                || gd.dataPlayer.countRepairConstructionPowerUp >= gd.dataPlayer.maxCountRepairConstructionPowerUp && itemsShop[i].name == "RepairConstructionPowerUp")
            {
                itemsShop[i].outStock = true;
            }
            else
            {
                itemsShop[i].outStock = false;
            }
            itemsShop[i].CheckInShop();
        }
    }
    private void Start()
    {
        gd = GameData.instaceGameData;
        CheckItems();
        ShowItem();
    }
    public void ShowItem()
    {
        itemsShop[(int)ItemsShoop.AmmoM4].textCount.text = "" + gd.dataPlayer.M4Ammo;
        itemsShop[(int)ItemsShoop.AmmoSCAR].textCount.text = "" + gd.dataPlayer.scarAmmo;
        itemsShop[(int)ItemsShoop.AmmoSniper].textCount.text = "" + gd.dataPlayer.SniperAmmo;
        itemsShop[(int)ItemsShoop.NukePowerUp].textCount.text = "" + gd.dataPlayer.countNukePowerUp;
        itemsShop[(int)ItemsShoop.LifeUpPowerUp].textCount.text = "" + gd.dataPlayer.countLifeUpPowerUp;
        itemsShop[(int)ItemsShoop.RepairConstructionPowerUp].textCount.text = "" + gd.dataPlayer.countRepairConstructionPowerUp;
        itemsShop[(int)ItemsShoop.MedikitPowerUp].textCount.text = "" + gd.dataPlayer.countMedikitPowerUp;
        itemsShop[(int)ItemsShoop.IcePowerUp].textCount.text = "" + gd.dataPlayer.countIcePowerUp;
        itemsShop[(int)ItemsShoop.MeteoroPowerUp].textCount.text = "" + gd.dataPlayer.countMeteoroPowerUp;

        itemsShop[(int)ItemsShoop.M4].textPrice.text = itemsShop[(int)ItemsShoop.M4].price + "$";
        itemsShop[(int)ItemsShoop.AmmoM4].textPrice.text = itemsShop[(int)ItemsShoop.AmmoM4].price + "$";
        itemsShop[(int)ItemsShoop.SCAR].textPrice.text = itemsShop[(int)ItemsShoop.SCAR].price + "$";
        itemsShop[(int)ItemsShoop.AmmoSCAR].textPrice.text = itemsShop[(int)ItemsShoop.AmmoSCAR].price + "$";
        itemsShop[(int)ItemsShoop.Sniper].textPrice.text = itemsShop[(int)ItemsShoop.Sniper].price + "$";
        itemsShop[(int)ItemsShoop.AmmoSniper].textPrice.text = itemsShop[(int)ItemsShoop.AmmoSniper].price + "$";
        itemsShop[(int)ItemsShoop.NukePowerUp].textPrice.text = itemsShop[(int)ItemsShoop.NukePowerUp].price + "$";
        itemsShop[(int)ItemsShoop.LifeUpPowerUp].textPrice.text = itemsShop[(int)ItemsShoop.LifeUpPowerUp].price + "$";
        itemsShop[(int)ItemsShoop.RepairConstructionPowerUp].textPrice.text = itemsShop[(int)ItemsShoop.RepairConstructionPowerUp].price + "$";
        itemsShop[(int)ItemsShoop.MedikitPowerUp].textPrice.text = itemsShop[(int)ItemsShoop.MedikitPowerUp].price + "$";
        itemsShop[(int)ItemsShoop.IcePowerUp].textPrice.text = itemsShop[(int)ItemsShoop.IcePowerUp].price + "$";
        itemsShop[(int)ItemsShoop.MeteoroPowerUp].textPrice.text = itemsShop[(int)ItemsShoop.MeteoroPowerUp].price + "$";
    }
    public void BuyItem(int indexItemShop)
    {
        int index = indexItemShop;
        switch (indexItemShop)
        {
            case (int)ItemsShoop.AmmoM4:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock && gd.dataPlayer.unlockedM4)
                {
                    if (gd.dataPlayer.M4Ammo < gd.dataPlayer.maxAmmoM4)
                    {
                        gd.generalScore -= itemsShop[index].price;
                        gd.dataPlayer.M4Ammo += itemsShop[index].count;
                        itemsShop[index].textCount.text = ""+gd.dataPlayer.M4Ammo;
                    }
                    else if (gd.dataPlayer.M4Ammo >= gd.dataPlayer.maxAmmoM4)
                    {
                        gd.dataPlayer.M4Ammo = gd.dataPlayer.maxAmmoM4;
                        itemsShop[index].textCount.text = "" + gd.dataPlayer.maxAmmoM4;
                    }
                }
                break;
            case (int)ItemsShoop.AmmoSCAR:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock && gd.dataPlayer.unlockedScar)
                {
                    if (gd.dataPlayer.scarAmmo < gd.dataPlayer.maxAmmoScar)
                    {
                        gd.generalScore -= itemsShop[index].price;
                        gd.dataPlayer.scarAmmo += itemsShop[index].count;
                        itemsShop[index].textCount.text = "" + gd.dataPlayer.scarAmmo;
                    }
                    else if (gd.dataPlayer.scarAmmo >= gd.dataPlayer.maxAmmoScar)
                    {
                        gd.dataPlayer.scarAmmo = gd.dataPlayer.maxAmmoScar;
                        itemsShop[index].textCount.text = "" + gd.dataPlayer.maxAmmoScar;
                    }
                }
                break;
            case (int)ItemsShoop.AmmoSniper:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock && gd.dataPlayer.unlockedSniper)
                {
                    if (gd.dataPlayer.SniperAmmo < gd.dataPlayer.maxAmmoSniper)
                    {
                        gd.generalScore -= itemsShop[index].price;
                        gd.dataPlayer.SniperAmmo += itemsShop[index].count;
                        itemsShop[index].textCount.text = "" + gd.dataPlayer.SniperAmmo;
                    }
                    else if (gd.dataPlayer.SniperAmmo >= gd.dataPlayer.maxAmmoSniper)
                    {
                        gd.dataPlayer.SniperAmmo = gd.dataPlayer.maxAmmoSniper;
                        itemsShop[index].textCount.text = "" + gd.dataPlayer.maxAmmoSniper;
                    }
                }
                break;
            case (int)ItemsShoop.IcePowerUp:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock)
                {
                    if (gd.dataPlayer.countIcePowerUp < gd.dataPlayer.maxCountIcePowerUp)
                    {
                        gd.generalScore -= itemsShop[index].price;
                        gd.dataPlayer.countIcePowerUp += itemsShop[index].count;
                        itemsShop[index].textCount.text = "" + gd.dataPlayer.countIcePowerUp;

                    }
                    else if (gd.dataPlayer.countIcePowerUp >= gd.dataPlayer.maxCountIcePowerUp)
                    {
                        gd.dataPlayer.countIcePowerUp = gd.dataPlayer.maxCountIcePowerUp;
                        itemsShop[index].textCount.text = "" + gd.dataPlayer.maxCountIcePowerUp;
                    }

                }
                break;
            case (int)ItemsShoop.LifeUpPowerUp:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock)
                {
                    if (gd.dataPlayer.countLifeUpPowerUp < gd.dataPlayer.maxCountLifeUpPowerUp)
                    {
                        gd.generalScore -= itemsShop[index].price;
                        gd.dataPlayer.countLifeUpPowerUp += itemsShop[index].count;
                        itemsShop[index].textCount.text = "" + gd.dataPlayer.countLifeUpPowerUp;
                    }
                    else if (gd.dataPlayer.countLifeUpPowerUp >= gd.dataPlayer.maxCountLifeUpPowerUp)
                    {
                        gd.dataPlayer.countLifeUpPowerUp = gd.dataPlayer.maxCountLifeUpPowerUp;
                        itemsShop[index].textCount.text = "" + gd.dataPlayer.maxCountLifeUpPowerUp;
                    }
                }
                break;
            case (int)ItemsShoop.M4:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock)
                {
                    gd.generalScore -= itemsShop[index].price;
                    gd.dataPlayer.unlockedM4 = true;
                    itemsShop[index].outStock = true;
                }
                break;
            case (int)ItemsShoop.MedikitPowerUp:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock)
                {
                    if (gd.dataPlayer.countMedikitPowerUp < gd.dataPlayer.maxCountMedikitPowerUp)
                    {
                        gd.generalScore -= itemsShop[index].price;
                        gd.dataPlayer.countMedikitPowerUp += itemsShop[index].count;
                        itemsShop[index].textCount.text = "" + gd.dataPlayer.countMedikitPowerUp;
                    }
                    else if (gd.dataPlayer.countMedikitPowerUp >= gd.dataPlayer.maxCountMedikitPowerUp)
                    {
                        gd.dataPlayer.countMedikitPowerUp = gd.dataPlayer.maxCountMedikitPowerUp;
                        itemsShop[index].textCount.text = "" + gd.dataPlayer.maxCountMedikitPowerUp;
                    }
                }
                break;
            case (int)ItemsShoop.MeteoroPowerUp:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock)
                {
                    if (gd.dataPlayer.countMeteoroPowerUp < gd.dataPlayer.maxCountMeteoroPowerUp)
                    {
                        gd.generalScore -= itemsShop[index].price;
                        gd.dataPlayer.countMeteoroPowerUp += itemsShop[index].count;
                        itemsShop[index].textCount.text = "" + gd.dataPlayer.countMeteoroPowerUp;
                    }
                    else if (gd.dataPlayer.countMeteoroPowerUp >= gd.dataPlayer.maxCountMeteoroPowerUp)
                    {
                        gd.dataPlayer.countMeteoroPowerUp = gd.dataPlayer.maxCountMeteoroPowerUp;
                        itemsShop[index].textCount.text = "" + gd.dataPlayer.maxCountMeteoroPowerUp;
                    }
                }
                break;
            case (int)ItemsShoop.NukePowerUp:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock)
                {
                    if (gd.dataPlayer.countNukePowerUp < gd.dataPlayer.maxCountNukePowerUp)
                    {
                        gd.generalScore -= itemsShop[index].price;
                        gd.dataPlayer.countNukePowerUp += itemsShop[index].count;
                        itemsShop[index].textCount.text = "" + gd.dataPlayer.countNukePowerUp;
                    }
                    else if (gd.dataPlayer.countNukePowerUp >= gd.dataPlayer.maxCountNukePowerUp)
                    {
                        gd.dataPlayer.countNukePowerUp = gd.dataPlayer.maxCountNukePowerUp;
                        itemsShop[index].textCount.text = "" + gd.dataPlayer.maxCountNukePowerUp;
                    }
                }
                break;
            case (int)ItemsShoop.RepairConstructionPowerUp:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock)
                {
                    if (gd.dataPlayer.countRepairConstructionPowerUp < gd.dataPlayer.maxCountRepairConstructionPowerUp)
                    {
                        gd.generalScore -= itemsShop[index].price;
                        gd.dataPlayer.countRepairConstructionPowerUp += itemsShop[index].count;
                        itemsShop[index].textCount.text = "" + gd.dataPlayer.countRepairConstructionPowerUp;
                    }
                    else if (gd.dataPlayer.countRepairConstructionPowerUp >= gd.dataPlayer.maxCountRepairConstructionPowerUp)
                    {
                        gd.dataPlayer.countRepairConstructionPowerUp = gd.dataPlayer.maxCountRepairConstructionPowerUp;
                        itemsShop[index].textCount.text = "" + gd.dataPlayer.maxCountRepairConstructionPowerUp;
                    }
                }
                break;
            case (int)ItemsShoop.SCAR:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock)
                {
                    gd.generalScore -= itemsShop[index].price;
                    gd.dataPlayer.unlockedScar = true;
                    itemsShop[index].outStock = true;
                }
                break;
            case (int)ItemsShoop.Sniper:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock)
                {
                    gd.generalScore -= itemsShop[index].price;
                    gd.dataPlayer.unlockedSniper = true;
                    itemsShop[index].outStock = true;
                }
                break;
        }
        CheckItems();
        itemsShop[index].CheckInShop();
    }

    /*public void SellItem(ItemsShoop _itemsShoop)
    {
        int index = (int)_itemsShoop;
        switch (_itemsShoop)
        {
            case ItemsShoop.AmmoM4:
                if (gd.dataPlayer.M4Ammo > 0)
                {
                    gd.dataPlayer.M4Ammo -= itemsShop[index].count;
                    gd.generalScore += itemsShop[index].sellPrice;
                    if (gd.dataPlayer.M4Ammo < 0)
                    {
                        gd.dataPlayer.M4Ammo = 0;
                    }
                }
                break;
            case ItemsShoop.AmmoSCAR:
                if (gd.dataPlayer.scarAmmo > 0)
                {
                    gd.dataPlayer.scarAmmo -= itemsShop[index].count;
                    gd.generalScore += itemsShop[index].sellPrice;
                    if (gd.dataPlayer.scarAmmo < 0)
                    {
                        gd.dataPlayer.scarAmmo = 0;
                    }
                }
                break;
            case ItemsShoop.AmmoSniper:
                if (gd.dataPlayer.SniperAmmo > 0)
                {
                    gd.dataPlayer.SniperAmmo -= itemsShop[index].count;
                    gd.generalScore += itemsShop[index].sellPrice;
                    if (gd.dataPlayer.SniperAmmo < 0)
                    {
                        gd.dataPlayer.SniperAmmo = 0;
                    }
                }
                break;
            case ItemsShoop.IcePowerUp:
                if (gd.dataPlayer.countIcePowerUp > 0)
                {
                    gd.dataPlayer.countIcePowerUp -= itemsShop[index].count;
                    gd.generalScore += itemsShop[index].sellPrice;
                    if (gd.dataPlayer.countIcePowerUp < 0)
                    {
                        gd.dataPlayer.countIcePowerUp = 0;
                    }
                }
                break;
            case ItemsShoop.LifeUpPowerUp:
                if (gd.dataPlayer.countLifeUpPowerUp > 0)
                {
                    gd.dataPlayer.countLifeUpPowerUp -= itemsShop[index].count;
                    gd.generalScore += itemsShop[index].sellPrice;
                    if (gd.dataPlayer.countLifeUpPowerUp < 0)
                    {
                        gd.dataPlayer.countLifeUpPowerUp = 0;
                    }
                }
                break;
            case ItemsShoop.MedikitPowerUp:
                if (gd.dataPlayer.countMedikitPowerUp > 0)
                {
                    gd.dataPlayer.countMedikitPowerUp -= itemsShop[index].count;
                    gd.generalScore += itemsShop[index].sellPrice;
                    if (gd.dataPlayer.countMedikitPowerUp < 0)
                    {
                        gd.dataPlayer.countMedikitPowerUp = 0;
                    }
                }
                break;
            case ItemsShoop.MeteoroPowerUp:
                if (gd.dataPlayer.countMeteoroPowerUp > 0)
                {
                    gd.dataPlayer.countMeteoroPowerUp -= itemsShop[index].count;
                    gd.generalScore += itemsShop[index].sellPrice;
                    if (gd.dataPlayer.countMeteoroPowerUp < 0)
                    {
                        gd.dataPlayer.countMeteoroPowerUp = 0;
                    }
                }
                break;
            case ItemsShoop.NukePowerUp:
                if (gd.dataPlayer.countNukePowerUp > 0)
                {
                    gd.dataPlayer.countNukePowerUp -= itemsShop[index].count;
                    gd.generalScore += itemsShop[index].sellPrice;
                    if (gd.dataPlayer.countNukePowerUp < 0)
                    {
                        gd.dataPlayer.countNukePowerUp = 0;
                    }
                }
                break;
            case ItemsShoop.RepairConstructionPowerUp:
                if (gd.dataPlayer.countRepairConstructionPowerUp > 0)
                {
                    gd.dataPlayer.countRepairConstructionPowerUp -= itemsShop[index].count;
                    gd.generalScore += itemsShop[index].sellPrice;
                    if (gd.dataPlayer.countRepairConstructionPowerUp < 0)
                    {
                        gd.dataPlayer.countRepairConstructionPowerUp = 0;
                    }
                }
                break;
        }
        itemsShop[index].countStock++;
        itemsShop[index].CheckInShop();
    }*/
}
