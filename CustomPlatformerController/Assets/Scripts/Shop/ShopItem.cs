using System;

[Serializable]
public class ShopItem
{
    public string name { get; set; }
    public int price { get; set; }

    public bool isEquiped { get; set; }
    public bool isOwned { get; set; }

    public ShopItem(string name, int price, bool isEquiped, bool isOwned)
    {
        this.name = name;
        this.price = price;
        this.isEquiped = isEquiped;
        this.isOwned = isOwned;
    }

    public override string ToString()
    {
        return name;
    }
}
