using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_HPBar : MonoBehaviour
{
    public Slider slHealth;
    public GameObject PlayerHealthBarPosition;
    // Start is called before the first frame update
    void Start()
    {
        slHealth = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slHealth.transform.position = PlayerHealthBarPosition.transform.position;
        slHealth.value = GameObject.Find("GameManager").GetComponent<GameManager>().player_Health/ GameObject.Find("GameManager").GetComponent<GameManager>().player_MaxHealth;
        if (slHealth.value <= 0)
            transform.Find("Fill Area").gameObject.SetActive(false);
        else
            transform.Find("Fill Area").gameObject.SetActive(true);
    }
}
