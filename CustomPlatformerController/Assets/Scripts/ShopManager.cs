using System.Collections.Generic;
using Assets.Scripts.Base;
using UnityEngine;
using System;

public class ShopManager : MonoBehaviour
{
    private SaveSystem saveSystem;
    public GameStats gameStats;

    private List<Stats> categories;

    public List<ShopItem> items { get; set; }

    void Awake()
    {
        saveSystem = new SaveSystem();
    }

    public void loadData()
    {
        loadItems();
    }

    public void saveData()
    {
        saveItems();
        saveCoins();
    }

    public void loadItems()
    {
        saveSystem.Clear();
        saveSystem.Load(Files.ITEMS_FNAME);
        items = saveSystem.GetObject<List<ShopItem>>();
        saveSystem.Clear();
    }

    public void saveItems()
    {
        saveSystem.Clear();
        saveSystem.Add(items);
        saveSystem.Save(Files.ITEMS_FNAME);
        saveSystem.Clear();
    }

    public void saveCoins()
    {
        gameStats.Save();
    }

    public ShopItem getShopItem(string name)
    {
        foreach(ShopItem item in items)
        {
            if(item.name == name)
            {
                return item;
            }
        }

        return null;
    }

    public void buyItem(string itemName)
    {
        ShopItem item = getShopItem(itemName);

        if (item.price <= gameStats.coins && !item.isOwned)
        {
            gameStats.DecreaseCoins(item.price);
            item.isOwned = true;
        }
    }

    public void equipItem(string itemName)
    {
        ShopItem item = getShopItem(itemName);

        if (item.isOwned)
        {
            item.isEquiped = true;
        }
    }

    public void unequipItem(string itemName)
    {
        ShopItem item = getShopItem(itemName);

        if (item.isEquiped)
        {
            item.isEquiped = false;
        }
    }
}
