using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        itemList.Add(new Item(0, 20001, "기본 검", "공격력이 1입니다.", Item.ItemType.Equip));
        itemList.Add(new Item(1, 20001, "어... 조금 좋은 검", "공격력이 2입니다.", Item.ItemType.Equip));
        itemList.Add(new Item(2, 20002, "뾰족 한 검", "공격력이 3입니다.", Item.ItemType.Equip));
        itemList.Add(new Item(3, 20003, "...", "공격력이 5입니다.", Item.ItemType.Equip));
        itemList.Add(new Item(4, 20002, "......", "체력을 50 채워줍니다", Item.ItemType.Equip));
        //itemList.Add(new Item("sword {2}", 10002, "빨간 포션", "체력을 50 채워줍니다", Item.ItemType.Use));
        //itemList.Add(new Item("sword 2", 10002, "빨간 포션", "체력을 50 채워줍니다", Item.ItemType.Use));
        // itemList.Add(new Item("sword2", 10002, "빨간 포션", "체력을 50 채워줍니다", Item.ItemType.Use));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
