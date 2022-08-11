using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public int itemID;
    public string itemName;
    public string itemDescription;//아이템 설명
    public int itemIconEmage;
    public int itemCount;
    public Sprite itemIcon;
    public Sprite[] itemIcon2;
  
    public ItemType itemType;

    public enum ItemType//열거
    {
        Use,
        Equip,
        Quest,
        ETC
    }
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(itemIcon2[2].name);
       
    }

    public Item(int _itemIconEmage, int _itmeID, string _itemName, string _itemDescription, ItemType _itemType, int _itemCount   = 1)
    {
        itemID = _itmeID;//스트링값으로 ㅏㄷ아와야할듯
        itemName = _itemName;
        itemDescription = _itemDescription;
        itemCount = _itemCount;
        itemType = _itemType;
         itemIconEmage = _itemIconEmage;

         //itemIcon = Resources.Load("ItemIcon/무기", typeof(Sprite)) as Sprite;
        // Debug.Log(itemIcon2[1].name);
        itemIcon2 = Resources.LoadAll<Sprite>("ItemIcon/Sword");
        itemIcon = itemIcon2[_itemIconEmage];
        //Debug.Log("1111"+itemIcon2[_itemIconEmage].name);
        // itemIcon = itemIcon2[1];

        //itemIcon2 = itemIcon[_itemIconEmage] as Sprite;

        //itemIcon = Resources.LoadAll("ItmeIcon/Sword" + _itemIconEmage, typeof(Sprite)) as Sprite;

    }
}
