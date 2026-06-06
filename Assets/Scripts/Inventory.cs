using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] Image[] slots;


    int itemAmount = 0;

    bool isNeedleTaken = false;

    void Start()
    {
    }

    void Update()
    {

    }
    public void AddItem(string _itemName, Sprite _itemSprite)
    {
        if (itemAmount >= 4) return;
        slots[itemAmount].enabled = true;
        slots[itemAmount].sprite = _itemSprite;

        if(_itemName=="needle")
        {
            isNeedleTaken = true;
        }
        if (_itemName == "choco")
        {
            slots[itemAmount].GetComponent<RectTransform>().sizeDelta = new Vector2(160, 80);
        }
        else if (_itemName == "flour")
        {
            slots[itemAmount].GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);

        }
        else if (_itemName == "butter")
        {
            slots[itemAmount].GetComponent<RectTransform>().sizeDelta = new Vector2(120, 80);
        }
        else

        {
            slots[itemAmount].GetComponent<RectTransform>().sizeDelta = new Vector2(100, 80);
        }
        itemAmount++;
    }

    public void RemoveItem(string _itemName)
    {
        //Should check the name of the item
        slots[itemAmount].enabled = false;

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
    public bool CheckIfPlayerHasNeedle()
    {
        return isNeedleTaken;
    }

}
