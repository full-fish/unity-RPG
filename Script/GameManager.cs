//GameObject.Find("GameManager").GetComponent<GameManager>().;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public int player_Level;
    public int status_Point;
    public float player_Health;
    public float player_MaxHealth;
    public int player_Attack;
    public float player_MP;
    public float player_MaxMP;
    public float current_Player_EX;
    public float levelUp_Player_EX;
    public int staus_Attack;
    public float staus_Defend;
    public int player_Attack_Total;
    public GameObject UIRestartButton;
    public GameObject UIHealthUPButton;
    public GameObject UIMPUPButton;
    public GameObject UIDamageUPButton;
    public GameObject UIDefendUPButton;
    public GameObject UIStats;
    public GameObject PlayerDamagedPosition;
   // public GameObject EnemyDamagedPosition;
    public GameObject PlayerDamagedTextobj;
   // public GameObject EnemyDamagedTextobj;
    public GameObject eneymySnakeObjec;

    public Transform[] spawnPoints;
    public Text PlayerHealthText;
    public Text PlayerMPText;
    public Text PlayerEXText;
    public Text PlayerDamageText;
    public Text PlayerDefendText;
    public Text PlayerStausText;
    public Text PlayerDamagedText;



    // Start is called before the first frame update

  
    void Awake()
    {
        player_MaxHealth = player_Health;
        player_MaxMP = player_MP;
        SpawnEnemy();
    }

     void Update()
    {
        UI();
        EX();
        player_Attack_Total = player_Attack + staus_Attack;

    }

    void FixedUpdate()
    {
        
;    }
    public void Player_Health_Down()
    {
        Invoke("PlayerDamagedTextRemove", 1);
        if (player_Health > 1)
        {
            PlayerDamagedTextobj.SetActive(true);
            float playerDamaged = GameObject.Find("Enemy_Snake(Clone)").GetComponent<Enemy_Snake>().enemyAttack - staus_Defend;
            if (playerDamaged <= 1)
                playerDamaged = 1;
            player_Health = player_Health - playerDamaged;
            //PlayerDamagedText.text = (GameObject.Find("Enemy_Snake(Clone)").GetComponent<Enemy_Snake>().enemyAttack).ToString();
            PlayerDamagedText.text = playerDamaged.ToString();

        }
        //죽음
        else
        {
            //Aill Health UI Off


            //Player Die Effect
            GameObject.Find("Player").GetComponent<Player>().OnDie();

            //Resilt UI
            Debug.Log("죽음");

            //Retry Button UI
            UIRestartButton.SetActive(true);
        }
    }
    void PlayerDamagedTextRemove()
    {
        PlayerDamagedTextobj.SetActive(false);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void EX()
    {
        if(current_Player_EX >= levelUp_Player_EX)
        {
            LevelUP();
            current_Player_EX = current_Player_EX - levelUp_Player_EX;
            levelUp_Player_EX = Mathf.Round(levelUp_Player_EX * 1.15f);
        }
        
    }

    void LevelUP()
    {
        player_Level += 1;
        status_Point += 5;
        player_Health = player_MaxHealth;
        player_MP = player_MaxMP;
    }
    private void UI()
    {
        if (Input.GetButtonUp("Status"))
        {
            if (UIStats.activeSelf == true) //부모오브젝트가 비활성이면 자식도 비활성임 이럴떈 activeInHierarchy 함수씀
                UIStats.SetActive(false);
            else
                UIStats.SetActive(true);
        }


        PlayerHealthText.text = (player_Health)+"/"+(player_MaxHealth).ToString();
        PlayerMPText.text = (player_MP)+"/"+(player_MaxMP).ToString();
        PlayerEXText.text = (current_Player_EX)+"/"+(levelUp_Player_EX).ToString();
        PlayerDamageText.text = (player_Attack_Total).ToString();
        PlayerDefendText.text = (staus_Defend).ToString();
        PlayerStausText.text = (status_Point).ToString();

        PlayerDamagedText.transform.position = PlayerDamagedPosition.transform.position;
        //EnemyDamagedText.transform.position = EnemyDamagedTextobj.transform.position;
        

    }

    void SpawnEnemy()
    {
        int ranPoint = Random.Range(0, 7);
        Instantiate(eneymySnakeObjec, spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);
        Invoke("SpawnEnemy", 5);
    }




    






    public void player_MaxHealthUP()
    {
        if (status_Point > 0)
        {
            player_MaxHealth += 5;
            status_Point -= 1;
        }
        
    }

    public void player_MaxMPUP()
    {
        if (status_Point > 0)
        {
            player_MaxMP += 5;
            status_Point -= 1;
        }
    }

    public void staus_AttackUP()
    {
        if(status_Point > 0)
        {
            staus_Attack += 1;
            status_Point -= 1;
        }
        
    }

    public void staus_DefendUP()
    {
        if (status_Point > 0)
        {
            staus_Defend += 1;
            status_Point -= 1;
        }
        
    }
}
