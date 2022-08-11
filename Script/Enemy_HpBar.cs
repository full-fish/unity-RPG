using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy_HpBar : MonoBehaviour
{
    public Slider slEnemyHealth;
    public GameObject objslEnemyHealth;
    public GameObject EnemyHealthBarPoisiton;
    // Start is called before the first frame update
    void Start()
    {
        slEnemyHealth = GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        slEnemyHealth.transform.position = EnemyHealthBarPoisiton.transform.position;
        slEnemyHealth.value = GetComponentInParent<Enemy_Snake>().enemyHealth / GetComponentInParent<Enemy_Snake>().enemyMaxHealth;//부모 스크립트 참조방법
        if (slEnemyHealth.value <= 0)
            transform.Find("Fill Area").gameObject.SetActive(false);
        else
            transform.Find("Fill Area").gameObject.SetActive(true);
    }
   public void Delete()
    {
        objslEnemyHealth.SetActive(false);
    }

}
