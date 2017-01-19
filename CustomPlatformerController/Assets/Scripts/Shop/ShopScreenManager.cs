using Assets.Scripts.Base;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreenManager : MenusController
{
    public ShopManager shopManager;
    private GameStats gameStats
    {
        get { return GameStats.Instance; }
    }

    public Dropdown dropdown;

    public Text itemName;
    public Text itemPrice;
    public Toggle isOwned;
    public Toggle isEquiped;

    public Button Btn;
    public Button equipBtn;
    public Text BtnTxt;

    public Text coins;

    private ShopItem selectedItem = null;

    void Awake()
    {
        fillItemDropdown();
        selectItem(0);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        coins.text = gameStats.coins.ToString();

        fillItemInfo();

        if (selectedItem != null)
        {
			if (!selectedItem.isOwned) BtnTxt.text = "Koop";
			else if (selectedItem.isEquiped) BtnTxt.text = "Deselecteer";
			else BtnTxt.text = "Selecteer";
        }
        else
        {
            BtnTxt.text = "";           
            Btn.enabled = false;
        }
    }

    //fills item dropdown with items
    public void fillItemDropdown()
    {
        List<string> itemNames = new List<string>();

        foreach (ShopItem item in shopManager.itemManager.items)
        {
            itemNames.Add(item.name);
        }

        dropdown.AddOptions(itemNames);
    }

    public void fillItemInfo()
    {
        if (selectedItem != null)
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
        selectedItem = shopManager.itemManager.items[index];
    }

	public void BuyOrEquip()
	{
		if (!selectedItem.isOwned) shopManager.buyItem(selectedItem.name);
		else shopManager.equipItem(selectedItem.name);
		SoundManager.Instance.PlayButtonClickSound();
	}

	public void buyItem()
    {
        shopManager.buyItem(selectedItem.name);
        SoundManager.Instance.PlayButtonClickSound();
    }

    public void equipItem()
    {
        shopManager.equipItem(selectedItem.name);
        SoundManager.Instance.PlayButtonClickSound();
    }

    public void backToMenu()
    {
        shopManager.saveData();
        LevelLoader.LoadMainMenu();
        SoundManager.Instance.PlayButtonClickSound();
    }
}
