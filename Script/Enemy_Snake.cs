//GameObject.Find("Enemy_Snake").GetComponent<Enemy_Snake>().;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy_Snake : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsulCollider;
    public Text EnemyDamagedText;
    public GameObject EnemyDamagedTextobj;
    public GameObject eneymySnakeObjec;
    public Slider slEnemyHealth;
    public GameObject EnemyDamagedPoisiton;


    public float enemyHealth;
    public float enemyMaxHealth;
    public int enemyAttack;
    public int nextMove;
    // Start is called before the first frame update
    void Awake()
    {
        enemyMaxHealth = enemyHealth;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsulCollider = GetComponent<CapsuleCollider2D>();
        
        Invoke("Think", 0.4f);  //주어진 시간이 지난 뒤 지정된 함수를 실행하는 함수
        Invoke("OffDamagedFix", 10);//몬스터 무적되고 안풀리는 버그 정정
        
    }
    private void Start()
    {
        slEnemyHealth = GetComponent<Slider>();
    }
    public void Update()
    {
        //slEnemyHealth.transform.position = EnemyHeadUpPoisiton.transform.position;
        EnemyDamagedText.transform.position = EnemyDamagedPoisiton.transform.position;
        //slEnemyHealth.value = enemyHealth / enemyMaxHealth;

        if (Input.GetButtonDown("Fire3"))
            PlayerFire();
    }

    void FixedUpdate()
    { //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector2.down, new Color(1, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector2.down, 1.5f, LayerMask.GetMask("Platform")); //RaycastHit2D : Ray에 닿은 오브젝트
                                                                                                              //Platform에 닿아야지
        if (rayHit.collider == null)//물리엔진이기에 collider
            Turn();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GameManager")
        {//Sound
         //audioSource.clip = audioDamagedout;
         //audioSource.Play();
         //Player Reposition
           
            EnemyDestroy();
        }

        // if (collision.gameObject.tag == "Player")

    }

    void Think()
    {
        //Set Next Active
        nextMove = Random.Range(-1, 2);//최저값은 포함되지만 최대값은 포함 안됨

        //Sprite Animation
        anim.SetInteger("WalkSpeed", nextMove);

        //Flip Sprite
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == -1;

        float nextThinkTime = Random.Range(3f, 5f);
        Invoke("Think", nextThinkTime); // 재귀함수 : 본인을 호출함  딜레이없이 하면과부하걸려서 위험
    }

    void ThinkDeath()
    {
        Debug.Log("죽는생각");
        //Set Next Active
        nextMove = nextMove*-1;//최저값은 포함되지만 최대값은 포함 안됨
        
        //Sprite Animation
        anim.SetInteger("WalkSpeed", nextMove);

        //Flip Sprite
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == -1;

        
        Invoke("ThinkDeath", 0.1f); // 재귀함수 : 본인을 호출함  딜레이없이 하면과부하걸려서 위험
    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == -1;

        CancelInvoke();
        Invoke("Think", 2);
    }



    public void EnemyHealthDown()
    {

        Invoke("EnemyDamagedTextRemove", 1);
        EnemyDamagedTextobj.SetActive(true);
        enemyHealth -= GameObject.Find("GameManager").GetComponent<GameManager>().player_Attack_Total;

        if (enemyHealth > 0)
        {

            EnemyDamagedText.text = (GameObject.Find("GameManager").GetComponent<GameManager>().player_Attack_Total).ToString();


            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            gameObject.layer = 13;
            Invoke("OffDamaged", 0.55f);

        }
        else
        {
            Die();
        }
    }


        void Die()
    {
        nextMove = 1;
        
        //GameObject.Find("Game Manager").GetComponent<GameManager>().stagePoint += 10;
        // gameManager.stagePoint += 10;
        //Sprite Alpha 투명화
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

            gameObject.layer = 15;
            //Sprite Flip Y
            spriteRenderer.flipY = true;
        // capsulCollider.enabled = false;
        //rigid.velocity = new Vector2(rigid.velocity.y, 7);
        
        
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        ThinkDeath();


        //GameObject.Find("Enemy_Snake_HealthBar").GetComponent<Enemy_HpBar>().Delete();
        GameObject.Find("GameManager").GetComponent<GameManager>().current_Player_EX += 3;
             
          
    }
    
    void EnemyDamagedTextRemove()
    {
        EnemyDamagedTextobj.SetActive(false);
    }

    void PlayerFire()
    {
        
            Debug.DrawRay(rigid.position, Vector2.right * GameObject.Find("Player").GetComponent<Player>().x * -1.12f, new Color(1, 1, 0));
            RaycastHit2D rayHitfire = Physics2D.Raycast(rigid.position, Vector2.right * GameObject.Find("Player").GetComponent<Player>().x, -1.12f, LayerMask.GetMask("Player"));
            if (rayHitfire.collider != null)
            {
                Debug.Log("몬스터가 뭐랑닿았는지 :" + rayHitfire.collider.name);
            }
            if (rayHitfire.collider != null && gameObject.layer ==11)
            {

                EnemyHealthDown();
            }
            
    }

   

    public void EnemyDestroy()
    {
        Destroy(gameObject);
    }

    void OffDamaged()
    {
        gameObject.layer = 11;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void OffDamagedFix()
    {
        gameObject.layer = 11;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        Invoke("OffDamagedFix",15);
    }

}
