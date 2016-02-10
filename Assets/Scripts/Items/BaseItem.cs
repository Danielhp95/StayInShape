using UnityEngine;
using System.Collections;

public class BaseItem {

    protected string itemName { get; set; }
    protected string itemDescription { get; set; }
    protected int itemID { get; set; }

    public enum ItemType
    {
        POTION,
        MEDICINE,
        THROWABLE,
        WEAPON,
        EQUIPMENT
    }
}
