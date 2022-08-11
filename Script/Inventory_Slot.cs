using System.Collections;
using System.Collections.Generic;
////////////using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
public class Inventory_Slot : MonoBehaviour
{
    public Image icon;
    public Text itemNameText;
    public Text itemCountText;
    public GameObject selectedItem;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Additem(Item _item)
    {
        itemNameText.text = _item.itemName;
        icon.sprite = _item.itemIcon;
        if (Item.ItemType.Use == _item.itemType)
        {
            if (_item.itemCount>0)
                itemCountText.text = _item.itemCount.ToString();

            else
                itemCountText.text = "";
        }
    }

    public void RemoveItem()
    {
        itemNameText.text = "";
        itemCountText.text = "";
        icon.sprite = null;
    }
}
