using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Contorol : MonoBehaviour
{
    //移動
    public float _h;
    [SerializeField] float _speed;
    [SerializeField] float StartSpeed;
    [SerializeField] float MinasStartSpeed;



    //ジャンプ
    private bool OnGround;
    private bool jump;
    [SerializeField] float jumpPower = 10f;
    [SerializeField] int jumpLimit = 2;

    public int jumpCount = 0;

    float p_ScaleX;

    //攻撃
    [SerializeField] float _attackmove1 = 5;
    [SerializeField] float _attackmove2 = 3;
    [SerializeField] float _Yattackmove1 = 5;
    [SerializeField] float _attackinterval = 1f;

    [SerializeField] int _attackcount=0;

    private bool isAttack;
    private bool ppp = false;
    
    [SerializeField] Animator runEfect = null;
    [SerializeField] Animator attackEffect = null;

   [SerializeField] float time = 0.2f;
  float m_timer;




     Rigidbody2D rb;
    private Animator anim = null;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        m_timer = _attackinterval;
    }

    // Update is called once per frame
    void Update()
    {
        float _h = Input.GetAxisRaw("Horizontal");


        
        Attack();
        //攻撃時に他の動きができないように
        if (isAttack == false)
        {
            JudgeJump();
            Move();
            FlipX(_h);
        }




        JudgeBool();






    }


    void FixedUpdate()
    {
        Jump();
        AttackMove();
    }

   
    void JudgeAttack()
    {
        isAttack=false;　　 //Animationイベントで攻撃モーションが終わったら実行
    }

  
    void AttackMove()
    {
        if (ppp == true)
        {
            //プレイヤーの位置
            Vector2 _pPos = this.gameObject.transform.position;

            //マウスの位置
            Vector2 _mousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //ベクトル
            Vector2 _attackMovePos = _mousPos - _pPos;

            //飛ばす
            if (_attackcount == 0)
            {
                if (rb.velocity.magnitude < 10)
                {
                    rb.AddForce(_attackMovePos * _attackmove1, ForceMode2D.Impulse);
                    _attackcount++;
                }
            }
            else if(_attackcount>0)
            {
                rb.AddForce(_attackMovePos * _attackmove2, ForceMode2D.Impulse);
            }

            //攻撃時の向き
            float jjj = _mousPos.x - _pPos.x;
            int a = 1;
            if (jjj > 0)
            {
                a = 1;
            }
            else if (jjj < 0)
            {
                a = -1;
            }
            ppp = false;
            
            transform.localScale = new Vector3(a, this.transform.localScale.y, this.transform.localScale.z);
        }
    }


    void Attack()
    {
        
        
        

        m_timer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1"))
            {
                isAttack = true;
            if (m_timer > _attackinterval)
            {

                ppp = true;
                
                m_timer = 0;
                attackEffect.SetTrigger("Attack");
                anim.SetTrigger("Attack 0");


            }
        }
        


    }


    void JudgeJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            
            if(jumpCount<jumpLimit)
            {
                jump = true;
               
            }
        }

    }
    void Jump()
    {
        if (jump)
        {
            Debug.Log("BBB");
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("OnGround",false);
            jump = false;
            OnGround = false;

            jumpCount++;
           
        }
    }







    //移動
     void Move()
    {
        float _h = Input.GetAxisRaw("Horizontal");

        Vector2 velo = new Vector2(_h * _speed, rb.velocity.y);
        rb.velocity = velo;

          
        if (_h == 0)
        {
            anim.SetBool("run", false);
            runEfect.SetBool("Run", false);
        }
        else
        {
         anim.SetBool("run", true);
         runEfect.SetBool("Run", true);
        }
    }

  

    void JudgeBool()
    {

        if(rb.velocity.y!=0)
        {
            OnGround = false;
            anim.SetBool("OnGround", false);
        }

        if (rb.velocity.y == 0)
        {
            runEfect.SetBool("jump", false);
        }
        else
        {
            runEfect.SetBool("jump", true);
        }

        if (OnGround == true)
        {
            runEfect.SetBool("Ground", true);
        }
        else
        {
            runEfect.SetBool("Ground", false);

        }

        if (rb.velocity.y > 0)
        {
            anim.SetBool("jump", true);


        }

        if (rb.velocity.y <= 0)
        {
            anim.SetBool("jump", false);

        }

    }


    // 絵の振り向き
    void FlipX(float horizontal)
    {


        p_ScaleX = this.transform.localScale.x;

        if (horizontal > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);

        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1 * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);

        }
    }
        public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag =="Ground")
        {
            anim.SetBool("OnGround", true);
            jumpCount = 0;
            OnGround = true;
        }

    }


}
