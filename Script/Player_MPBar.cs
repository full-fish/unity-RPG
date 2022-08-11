using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_MPBar : MonoBehaviour
{
    public Slider slMP;
    public GameObject PlayerMPBarPosition;
    // Start is called before the first frame update
    void Start()
    {
        slMP = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slMP.transform.position = PlayerMPBarPosition.transform.position;
        slMP.value = GameObject.Find("GameManager").GetComponent<GameManager>().player_MP / GameObject.Find("GameManager").GetComponent<GameManager>().player_MaxMP;
        if (slMP.value <= 0)
            transform.Find("Fill Area").gameObject.SetActive(false);
        else
            transform.Find("Fill Area").gameObject.SetActive(true);
    }
}
