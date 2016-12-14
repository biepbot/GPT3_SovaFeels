using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreenManager : MonoBehaviour
{
    public ShopManager shopManager;

    public Dropdown dropdown;

    public Text itemName;
    public Text itemPrice;
    public Toggle isOwned;
    public Toggle isEquiped;

    public Button buyBtn;
    public Button equipBtn;
    public Text equipBtnTxt;

    public Text coins;

    private ShopItem selectedItem;

	// Use this for initialization
	void Start ()
    {
        shopManager.loadData();
        fillItemDropdown();
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {
        coins.text = shopManager.coins.ToString();

        fillItemInfo();

        if (selectedItem != null)
        {
            buyBtn.enabled = !selectedItem.isOwned;
            equipBtn.enabled = selectedItem.isOwned;

            if(selectedItem.isEquiped)
            {
                equipBtnTxt.text = "unequip";
            }
            else
            {
                equipBtnTxt.text = "equip";
            }
        }
        else
        {
            equipBtnTxt.text = "equip";

            buyBtn.enabled = false;
            equipBtn.enabled = false;
        }
    }

    //fills item dropdown with items
    public void fillItemDropdown()
    {
        dropdown.AddOptions(new List<string>() { "item" });

        List<string> itemNames = new List<string>();

        foreach(ShopItem item in shopManager.items)
        {
            itemNames.Add(item.name);
        }

        dropdown.AddOptions(itemNames);
    }

    public void fillItemInfo()
    {
        if(selectedItem != null)
        {
            itemName.text = selectedItem.name;
            itemPrice.text = selectedItem.price.ToString();

            isOwned.isOn = selectedItem.isOwned;
            isEquiped.isOn = selectedItem.isEquiped;
        }
        else
        {
            itemName.text = "";
            itemPrice.text = "";

            isOwned.isOn = false;
            isEquiped.isOn = false;
        }
    }

    public void selectItem(int index)
    {
        if(index > 0)
        {
            selectedItem = shopManager.items[index - 1];
        }
        else
        {
            selectedItem = null;
        }
    }

    public void buyItem()
    {
        shopManager.buyItem(selectedItem.name);
    }

    public void equipItem()
    {
        if(!selectedItem.isEquiped)
        {
            shopManager.equipItem(selectedItem.name);
        }
        else
        {
            shopManager.unequipItem(selectedItem.name);
        }
    }
}
