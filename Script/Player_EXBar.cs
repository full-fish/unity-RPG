using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_EXBar : MonoBehaviour
{
    public Slider slEX;
    public GameObject PlayerEXBarPosition;
    // Start is called before the first frame update
    void Start()
    {
        slEX = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slEX.transform.position = PlayerEXBarPosition.transform.position;
        slEX.value = GameObject.Find("GameManager").GetComponent<GameManager>().current_Player_EX / GameObject.Find("GameManager").GetComponent<GameManager>().levelUp_Player_EX;
        if (slEX.value <= 0)
            transform.Find("Fill Area").gameObject.SetActive(false);
        else
            transform.Find("Fill Area").gameObject.SetActive(true);
    }
}
