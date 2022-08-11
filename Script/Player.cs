//GameObject.Find("Player").GetComponent<Player>().;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;

    public string walksound;
    private AudioManager theAudio;
    
    public int x;
    public int moveInt;
    public Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    CapsuleCollider2D capsulCollider;
    AudioSource audioSource;
    // Start is called before the first frame update


    // Update is called once per frame

    void Awake()
    {
        moveInt = 1;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        capsulCollider = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
        theAudio = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (rigid.velocity.x > 0)
            x = 1;
        else if (rigid.velocity.x < 0)
            x = -1;
        
        anim.SetInteger("x", x);
        JumpUpdate();
        WalkingUpdate();

        if (Input.GetButtonDown("Fire3"))
            Fire();
       
            
    }

    void FixedUpdate()
    {
        WalkingFixedUpdate();
        JumpFixedUpdate();
    }

    void WalkingUpdate()
    {
        

        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            anim.SetBool("isWalkingRight", false);
            anim.SetBool("isWalkingLeft", false);
        }
        else if ((rigid.velocity.x) >= 0.3)
        {
            anim.SetBool("isWalkingRight", true);  
        }
            
        else
        {    
            anim.SetBool("isWalkingLeft", true);
        }
            
       


        //멈출때 속도
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.45f, rigid.velocity.y); //normalized : 벡터 크기를 1로 만든 상태 (방향 구할떄 좌우니까) 
        }
    }




    void WalkingFixedUpdate()
    {
        
        //좌우 움직임
        float h = Input.GetAxisRaw("Horizontal");
        if(moveInt==1)
        rigid.velocity = new Vector2(maxSpeed * h, rigid.velocity.y);

        /*float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)//오른쪽 최대 속도 velocity=리지드바디의 현재 속도
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))//왼쪽 최고 속도
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);*/
    }

    void JumpUpdate()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.down, 0.6f, LayerMask.GetMask("Platform"));
        if (rayHit.collider != null)
            if (Input.GetButtonDown("Jump") && rayHit.collider.name=="Platform")
         {
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);

            //PlaySound
            //PlaySound("JUMP");
        }
    }

    void JumpFixedUpdate()
    {
        //Landing Platform
        //바닥과 닿는거   물체와 닿는 이벤트 함수로도 할 수 있지만 raycast함수로도 가능

        //점프모션관련 레이케스트
        Debug.DrawRay(rigid.position, Vector2.down, new Color(1, 1, 0)); //RayCast : 오브젝트 검색을 위해 Ray를 쏘는 방식
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.down, 1, LayerMask.GetMask("Platform")); //RaycastHit2D : Ray에 닿은 오브젝트
                                                                                                                 //Platform에 닿아야지
        if(rigid.velocity.y < 0)
            anim.SetBool("isJumping", false);
        if (rayHit.collider != null && rigid.velocity.y < 0)//물리엔진이기에 collider, 없는게 아니라면   rayHIT은 관통안함 부딪히면 끝
        {
            if (rayHit.distance < 0.68f) //값이 작으면 공중에 있는거로 인식, 값이 크면 점프 모션안나와서 2단 점프
            {
                //크기 1이고 중앙에서 시작하니까
                anim.SetBool("isJumping", false);
                
            }
            
            else
            {
                anim.SetBool("isJumping", true);
            }
        }
        //플렛폼 충돌 감지
        if (rayHit.collider != null)
        {
            Debug.Log("플레이어가 닿아있는 바닥 :" + rayHit.collider.name);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("플레이어가 '" + collision.gameObject.tag + "'와 물리 충돌");
        if (collision.gameObject.tag == "Enemy")
        {
                OnDamaged(collision.transform.position);
        }
    }

    void Fire()
    {
        theAudio.Play(walksound);
        anim.SetTrigger("Player_Attack_Right");
            anim.SetTrigger("Player_Attack_End");
       /* rigid.position = new Vector2(rigid.position.x, rigid.position.y - 0.15f);
             Debug.DrawRay(rigid.position, Vector2.right * x *1.1f, new Color(1, 1, 0));
        RaycastHit2D rayHitfire = Physics2D.Raycast(rigid.position, Vector2.right * x ,1.1f, LayerMask.GetMask("Enemy"));
        if (rayHitfire.collider != null)
        {
            Debug.Log("뭐랑닿았는지 :" + rayHitfire.collider.name);
        }
        if (rayHitfire.collider != null )
        {
          
            GameObject.Find("Enemy_Snake(Clone)").GetComponent<Enemy_Snake>().EnemyHealthDown();
        }*/

    }

    void OnDamaged(Vector2 targetPosition)
    {
        //healt Down
        GameObject.Find("GameManager").GetComponent<GameManager>().Player_Health_Down();

        // Change Layer
        gameObject.layer = 12;

        //깜빡임
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //튕겨나감

        int dirc = transform.position.x - targetPosition.x > 0 ? 1 : -1;  //0보다 크면 1  아니면 -1
        
        rigid.velocity=  new Vector2(dirc*12, 12);


        //Anmation
        //anim.SetTrigger("doDamaged");
        //anim.SetTrigger("offDamaged");
        Invoke("OffDamaged", 0.9f);

        //Sound
       // PlaySound("DAMAGED");

    }

    void OffDamaged()
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void OnDie()
    {
        //Sprite Alpha 투명화
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //Sprite Flip Y
        spriteRenderer.flipY = true;
        //Collider Disable
        capsulCollider.enabled = false;
        //Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        //Sound
        //PlaySound("DIE");
    }
    public void Move()
    {
        moveInt = 1;
    }

    public void NotMove()
    {
        moveInt = 0;
    }
}
