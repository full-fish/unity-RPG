using System.Collections;
using System.Collections.Generic;
//////////////////////using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    private Player theOrder;
    private AudioManager theAudio;
    public string key_Sound;
    public string enter_Sound;
    public string cancel_Sound;
    public string open_Sound;
    public string beep_Sound;

    private Inventory_Slot[] slots;// 인벤토리 슬롯들

    private List<Item> inventoryItemList; //소지한 템
    private List<Item> inventoryItemTabList;//텝에따라 보여지는 리스트

    public Text Description_Text; //부연설명
    public string[] tabDescription; //탭 부연설명

    public Transform tf;//slot의 부모객체   이걸 이용해서 자식개체 컨트롤

    public GameObject go; //끄고키고
    public GameObject[] selectedTabImages; //패널들

    private int selectedItem;
    private int selectedTab;

    private bool activated;//인ㅔㅂㄴ토리활성화시 트루
    private bool tabActivated;
    private bool itemActivated;
    private bool stopKeyInput; //키입력 제한 소비할때 질의나오는데 그 떄 방지
    private bool preventExec;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    // Start is called before the first frame update
    void Start()
    {
        theOrder = FindObjectOfType<Player>();
        theAudio = FindObjectOfType<AudioManager>();
        inventoryItemList = new List<Item>();
        inventoryItemTabList = new List<Item>();
        slots = tf.GetComponentsInChildren<Inventory_Slot>();
    }

    // Update is called once per frame
    void Update()
    {
     if(!stopKeyInput)
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                activated = !activated;

                if (activated)
                {
                    theOrder.NotMove();
                    theAudio.Play(open_Sound);
                    go.SetActive(true);
                    selectedTab = 0;
                    tabActivated = true;
                    itemActivated = false;
                    ShowTab();
               
                }
                else
                {
                    theAudio.Play(cancel_Sound);
                    StopAllCoroutines();
                    go.SetActive(false);
                    tabActivated = false;
                    itemActivated = false;
                    theOrder.Move();
                    
                }
            }

            if (activated)
            {
                if (tabActivated)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (selectedTab < selectedTabImages.Length - 1)
                            selectedTab++;
                        else
                            selectedTab = 0;
                        theAudio.Play(key_Sound);
                        SelectedTab();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (selectedTab > 0)
                            selectedTab--;
                        else
                            selectedTab = selectedTabImages.Length -1;
                        theAudio.Play(key_Sound);
                        SelectedTab();
                    }
                    else if (Input.GetKeyDown(KeyCode.Z))
                    {
                        theAudio.Play(enter_Sound);
                        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
                        color.a = 0.25f;
                        selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                        itemActivated = true;
                        tabActivated = false;
                        preventExec = true;
                        ShowItem();

                    }
                } //탭 활성화시 키입력 처리
                
                else if (itemActivated)
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        if (selectedItem < inventoryItemTabList.Count - 4)
                            selectedItem += 4;
                        else
                            selectedItem %= 4;
                        theAudio.Play(key_Sound);
                        SelectedItem();
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        if (selectedItem > 1)
                            selectedItem -= 4;
                        else
                            selectedItem = inventoryItemTabList.Count - 1- selectedItem;
                        theAudio.Play(key_Sound);
                        SelectedItem();
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (selectedItem < inventoryItemTabList.Count - 1)
                            selectedItem++;
                        else
                            selectedItem = 0;
                        theAudio.Play(key_Sound);
                        SelectedItem();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (selectedItem > 0)
                            selectedItem--;
                        else
                            selectedItem = inventoryItemTabList.Count -1;
                        theAudio.Play(key_Sound);
                        SelectedItem();
                    }
                    else if (Input.GetKeyDown(KeyCode.Z) && !preventExec)
                    {
                        if (selectedTab == 0)
                        {
                            theAudio.Play(enter_Sound);
                            stopKeyInput = true;
                            //물약을 마실 거냐같은 선택지 호출
                        }
                        else if (selectedTab == 1)
                        {
                            //장비장착
                        }
                        else//비프 출력
                        {
                            theAudio.Play(beep_Sound);
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.X))
                    {
                        theAudio.Play(cancel_Sound);
                        StopAllCoroutines();
                        itemActivated = false;
                        tabActivated = true;
                        ShowTab();
                    }
                }//아이템 활성화시 키입력 처리

                if (Input.GetKeyUp(KeyCode.Z))//중복 실행 방지
                    preventExec = false;
            }
            
        }
    }

    public void ShowTab()
    {
        RemoveSlot();
        SelectedTab();
    }//탭 활성화

    public void RemoveSlot()
    {
        for(int i=0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    }//인벤토리 슬롯 초기화
    public void SelectedTab()
    {
        StopAllCoroutines();
        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
        color.a = 0f;
        for(int i=0; i < selectedTabImages.Length; i++)
        {
            selectedTabImages[i].GetComponent<Image>().color = color;
        }
        Description_Text.text = tabDescription[selectedTab];
        StartCoroutine(SelectedTabEffectCoroitine());
    }//선택된 탭을 제외하고 다른 모든 태의 컬러 알파값 0으로 조정
    IEnumerator SelectedItemEffectCoroitine()//선택된 아이테 만짝임 효과
    {
        while (itemActivated)
        {
            Color color = slots[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                slots[selectedItem].selectedItem.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                slots[selectedItem].selectedItem.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            yield return new WaitForSeconds(0.3f);

        }
    }//선택된 탭 반짝임 효과

    IEnumerator SelectedTabEffectCoroitine()
    {
        while (itemActivated)
        {
            Color color = selectedTabImages[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            yield return new WaitForSeconds(0.3f);

        }
    }//선택된 탭 반짝임 효과

    public void ShowItem()//아이템 활성화 (inventoryTabList에 조건에 맞는 아이템들만 넣어주고, 이넨토리 슬롯에 출력)
    {
        inventoryItemList.Clear();
        RemoveSlot();
        selectedItem = 0;

        switch (selectedTab) // 탭에 따른 아이템 분류  그것을 인벤토리 탭 리스트에 추가
        {
            case 0:
                for( int i =0; i <inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Equip == inventoryItemList[i].itemType)
                        inventoryItemTabList.Add(inventoryItemList[i]);
                }
                break;
            case 1:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Use == inventoryItemList[i].itemType)
                        inventoryItemTabList.Add(inventoryItemList[i]);
                }
                break;
            case 2:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Quest == inventoryItemList[i].itemType)
                        inventoryItemTabList.Add(inventoryItemList[i]);
                }
                break;
        }

        for (int i =0; i < inventoryItemTabList.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].Additem(inventoryItemTabList[i]);
        }//인벤토리 탭 리스트의 내용을 인벤토리 슬롯에 추가

        SelectedItem();
    }
    public void SelectedItem()
    {
        StopAllCoroutines();
        if (inventoryItemTabList.Count > 0)
        {
            Color color = slots[0].selectedItem.GetComponent<Image>().color;
            color.a = 0f;
            for (int i = 0; i < inventoryItemTabList.Count; i++)
                slots[i].selectedItem.GetComponent<Image>().color = color;          
            Description_Text.text = inventoryItemTabList[selectedItem].itemDescription;
            StartCoroutine(SelectedItemEffectCoroitine());

        }
        else
            Description_Text.text = "해당 타입의 아이템을 소유하고 있지 않습니다.";
    }
}
