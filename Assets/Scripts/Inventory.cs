using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] Image[] slots;


    int itemAmount = 0;

    void Start()
    {
    }

    void Update()
    {

    }
    public void AddItem(string _itemName, Sprite _itemSprite)
    {
        if (itemAmount >= 4) return;
        slots[itemAmount].sprite = _itemSprite;
        itemAmount++;
    }

    public void RemoveItem(string _itemName)
    {
        //Should check the name of the item
        slots[itemAmount].sprite = null;
        itemAmount--;
    }
    public bool CanTakeItem()
    {
        if (itemAmount >= 4)
            return false;
        else
            return true;
    }
    public bool CheckItemAmount()
    {
        if (itemAmount >= 4)
            return true;
        else return false;
    }

}
