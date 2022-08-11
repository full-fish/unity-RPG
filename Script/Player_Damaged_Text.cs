using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Damaged_Text : MonoBehaviour
{
    public Text playerDamagerText;
    public GameObject PlayerDamagedPosition;
    // Start is called before the first frame update
    void Start()
    {
        playerDamagerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDamagerText.transform.position = PlayerDamagedPosition.transform.position;
    }
}
