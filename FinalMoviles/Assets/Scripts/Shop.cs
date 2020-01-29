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
        public int sellPrice;
        public Sprite spriteInShop;
        public Sprite spriteOutStok;
        public Image image;
        public Button button;
        public bool outStock;
        public int countStock;

        public void CheckInShop()
        {
            if (countStock <= 0)
            {
                outStock = true;
            }
            else if (countStock > 0)
            {
                outStock = false;
            }
            if (outStock)
            {
                button.interactable = false;
                image.sprite = spriteOutStok;
            }
            else if (!outStock)
            {
                button.interactable = true;
                image.sprite = spriteInShop;
            }
        }
    }
    public List<ItemShoop> itemsShop;
    private GameData gd;
    private void Start()
    {
        gd = GameData.instaceGameData;
        for (int i = 0; i < itemsShop.Count; i++)
        {
            itemsShop[i].CheckInShop();
        }
    }
    public void BuyItem(ItemsShoop _itemsShoop)
    {
        int index = (int)_itemsShoop;
        switch (_itemsShoop)
        {
            case ItemsShoop.AmmoM4:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock && gd.dataPlayer.unlockedM4)
                {
                    if (gd.dataPlayer.M4Ammo < gd.dataPlayer.maxAmmoM4)
                    {
                        gd.generalScore -= itemsShop[index].price;
                        gd.dataPlayer.M4Ammo += itemsShop[index].count;
                    }
                    else if (gd.dataPlayer.M4Ammo >= gd.dataPlayer.maxAmmoM4)
                    {
                        gd.dataPlayer.M4Ammo = gd.dataPlayer.maxAmmoM4;
                    }
                }
                break;
            case ItemsShoop.AmmoSCAR:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock && gd.dataPlayer.unlockedScar)
                {
                    if (gd.dataPlayer.scarAmmo < gd.dataPlayer.maxAmmoScar)
                    {
                        gd.generalScore -= itemsShop[index].price;
                        gd.dataPlayer.scarAmmo += itemsShop[index].count;
                    }
                    else if (gd.dataPlayer.scarAmmo >= gd.dataPlayer.maxAmmoScar)
                    {
                        gd.dataPlayer.scarAmmo = gd.dataPlayer.maxAmmoScar;
                    }
                }
                break;
            case ItemsShoop.AmmoSniper:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock && gd.dataPlayer.unlockedSniper)
                {
                    if (gd.dataPlayer.SniperAmmo < gd.dataPlayer.maxAmmoSniper)
                    {
                        gd.generalScore -= itemsShop[index].price;
                        gd.dataPlayer.SniperAmmo += itemsShop[index].count;
                    }
                    else if (gd.dataPlayer.SniperAmmo >= gd.dataPlayer.maxAmmoSniper)
                    {
                        gd.dataPlayer.SniperAmmo = gd.dataPlayer.maxAmmoSniper;
                    }
                }
                break;
            case ItemsShoop.IcePowerUp:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock)
                {
                    if (gd.dataPlayer.countIcePowerUp < gd.dataPlayer.maxCountIcePowerUp)
                    {
                        gd.generalScore -= itemsShop[index].price;
                        gd.dataPlayer.countIcePowerUp += itemsShop[index].count;
                    }
                    else if (gd.dataPlayer.countIcePowerUp >= gd.dataPlayer.maxCountIcePowerUp)
                    {
                        gd.dataPlayer.countIcePowerUp = gd.dataPlayer.maxCountIcePowerUp;
                    }
                    
                }
                break;
            case ItemsShoop.LifeUpPowerUp:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock)
                {
                    if (gd.dataPlayer.countLifeUpPowerUp < gd.dataPlayer.maxCountLifeUpPowerUp)
                    {
                        gd.generalScore -= itemsShop[index].price;
                        gd.dataPlayer.countLifeUpPowerUp += itemsShop[index].count;
                    }
                    else if (gd.dataPlayer.countLifeUpPowerUp >= gd.dataPlayer.maxCountLifeUpPowerUp)
                    {
                        gd.dataPlayer.countLifeUpPowerUp = gd.dataPlayer.maxCountLifeUpPowerUp;
                    }
                }
                break;
            case ItemsShoop.M4:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock)
                {
                    gd.generalScore -= itemsShop[index].price;
                    gd.dataPlayer.unlockedM4 = true;
                    itemsShop[index].outStock = true;
                }
                break;
            case ItemsShoop.MedikitPowerUp:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock)
                {
                    if (gd.dataPlayer.countMedikitPowerUp < gd.dataPlayer.maxCountMedikitPowerUp)
                    {
                        gd.generalScore -= itemsShop[index].price;
                        gd.dataPlayer.countMedikitPowerUp += itemsShop[index].count;
                    }
                    else if (gd.dataPlayer.countMedikitPowerUp >= gd.dataPlayer.maxCountMedikitPowerUp)
                    {
                        gd.dataPlayer.countMedikitPowerUp = gd.dataPlayer.maxCountMedikitPowerUp;
                    }
                    
                }
                break;
            case ItemsShoop.MeteoroPowerUp:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock)
                {
                    if (gd.dataPlayer.countMeteoroPowerUp < gd.dataPlayer.maxCountMeteoroPowerUp)
                    {
                        gd.generalScore -= itemsShop[index].price;
                        gd.dataPlayer.countMeteoroPowerUp += itemsShop[index].count;
                    }
                    else if (gd.dataPlayer.countMeteoroPowerUp >= gd.dataPlayer.maxCountMeteoroPowerUp)
                    {
                        gd.dataPlayer.countMeteoroPowerUp = gd.dataPlayer.maxCountMeteoroPowerUp;
                    }
                }
                break;
            case ItemsShoop.NukePowerUp:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock)
                {
                    if (gd.dataPlayer.countNukePowerUp < gd.dataPlayer.maxCountNukePowerUp)
                    {
                        gd.generalScore -= itemsShop[index].price;
                        gd.dataPlayer.countNukePowerUp += itemsShop[index].count;
                    }
                    else if (gd.dataPlayer.countNukePowerUp >= gd.dataPlayer.maxCountNukePowerUp)
                    {
                        gd.dataPlayer.countNukePowerUp = gd.dataPlayer.maxCountNukePowerUp;
                    }
                }
                break;
            case ItemsShoop.RepairConstructionPowerUp:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock)
                {
                    if (gd.dataPlayer.countRepairConstructionPowerUp < gd.dataPlayer.maxCountRepairConstructionPowerUp)
                    {
                        gd.generalScore -= itemsShop[index].price;
                        gd.dataPlayer.countRepairConstructionPowerUp += itemsShop[index].count;
                    }
                    else if (gd.dataPlayer.countRepairConstructionPowerUp >= gd.dataPlayer.maxCountRepairConstructionPowerUp)
                    {
                        gd.dataPlayer.countRepairConstructionPowerUp = gd.dataPlayer.maxCountRepairConstructionPowerUp;
                    }
                }
                break;
            case ItemsShoop.SCAR:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock)
                {
                    gd.generalScore -= itemsShop[index].price;
                    gd.dataPlayer.unlockedScar = true;
                    itemsShop[index].outStock = true;
                }
                break;
            case ItemsShoop.Sniper:
                if (gd.generalScore >= itemsShop[index].price && !itemsShop[index].outStock)
                {
                    gd.generalScore -= itemsShop[index].price;
                    gd.dataPlayer.unlockedSniper = true;
                    itemsShop[index].outStock = true;
                }
                break;
        }
        itemsShop[index].countStock--;
        itemsShop[index].CheckInShop();
    }
    public void SellItem(ItemsShoop _itemsShoop)
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
    }
}
