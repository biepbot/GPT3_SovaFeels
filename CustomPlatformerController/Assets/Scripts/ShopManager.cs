using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public List<ShopItem> items { get; set; }
    public int coins { get; set; }

	// Use this for initialization
	void Start(){}
	
	// Update is called once per frame
	void Update(){}

    public void loadData()
    {
        items = loadItems();
        coins = loadCoins();
    }

    public List<ShopItem> loadItems()
    {
        //temp implementation, replace with load from file later
        List<ShopItem> items = new List<ShopItem>();

        items.Add(new ShopItem("red shirt", 10, false, false));
        items.Add(new ShopItem("blue pants", 25, false, false));

        return items;
    }

    public void saveItems()
    {
        //temp implementation
    }

    public int loadCoins()
    {
        //temp implementation, replace later
        return 100;
    }

    public ShopItem getShopItem(string name)
    {
        ShopItem shopItem = null;

        foreach(ShopItem item in items)
        {
            if(item.name == name)
            {
                shopItem = item;
            }
        }

        return shopItem;
    }

    public void buyItem(string itemName)
    {
        ShopItem item = getShopItem(itemName);

        if (item == null)
        {
            return;
        }


        if (item.price <= coins && !item.isOwned)
        {
            decreaseCoins(item.price);
            item.isOwned = true;
        }
    }

    public void equipItem(string itemName)
    {
        ShopItem item = getShopItem(itemName);

        if(item == null)
        {
            return;
        }

        if (item.isOwned)
        {
            item.isEquiped = true;
        }
    }

    public void unequipItem(string itemName)
    {
        ShopItem item = getShopItem(itemName);

        if (item == null)
        {
            return;
        }

        if (item.isEquiped)
        {
            item.isEquiped = false;
        }
    }

    public void decreaseCoins(int coins)
    {
        if(this.coins >= coins)
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
