using System.Collections.Generic;
using Assets.Scripts.Base;
using UnityEngine;
using System;

public class ShopManager : MonoBehaviour
{
    private SaveSystem saveSystem = new SaveSystem();
    private List<Stats> categories;

    public List<ShopItem> items { get; set; }
    public int coins { get; set; }

	// Use this for initialization
	void Start(){}
	
	// Update is called once per frame
	void Update(){}

    public void loadData()
    {
        loadItems();
        loadCoins();
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

    public void loadCoins()
    {
        saveSystem.Clear();
        saveSystem.Load(Files.STATS_FNAME);
        categories = saveSystem.GetObject<List<Stats>>();
        saveSystem.Clear();

        if (categories != null)
        {
            foreach (Stats statsLine in categories)
            {
                if (statsLine.categoryName == null)
                {
                    coins = statsLine.coins;
                }
            }
        }
        //else
        //{
            //for testing only, remove when coins are earned by playing the game
        //    coins = 100;
        //}
    }

    public void saveCoins()
    {
        Stats stat;

        foreach(Stats statsLine in categories)
        {
            if(statsLine.categoryName == null)
            {
                stat = statsLine;
            }
        }

        stat.coins = coins;

        saveSystem.Clear();
        saveSystem.Add(categories);
        saveSystem.Save();
        saveSystem.Clear();
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

        if (item.price <= coins && !item.isOwned)
        {
            decreaseCoins(item.price);
            item.isOwned = true;
            print(item.ToString());
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

    public void decreaseCoins(int coins)
    {
        if(this.coins - coins >= 0)
        {
            this.coins -= coins;
            print(this.coins.ToString());
        }
    }

    public void increaseCoins(int coins)
    {
        this.coins += coins;
    }
}
