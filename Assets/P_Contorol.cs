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
    [SerializeField] float _attackinterval = 1f;
    [SerializeField] float limitSpeed1Y;
    [SerializeField] float limitSpeed2Y;
    [SerializeField] float limitSpeed3Y;

    [SerializeField] float limitSpeed1X;
    [SerializeField] float limitSpeed2X;
    

    [SerializeField] int _attackcount=0;

    private bool isAttack;
    private bool ppp = false;
    
    [SerializeField] Animator runEfect = null;
    [SerializeField] Animator attackEffect = null;

    private bool _hitenemy=false;
    private bool _timecount;



   [SerializeField] float time = 0f;
  float m_timer;



    //回避
    [SerializeField] GameObject mous;
    [SerializeField] LayerMask _layerGound;
    [SerializeField] float _timelimitE =3f;
    public float _timeE=0;
    bool _secondE=false;

    bool _iscoolTimeE=false;
    bool _isE=true;
    float _coolTimeE=0;
    [SerializeField] float _coolTimeLimitE=5;
    [SerializeField] Animator evasionEffect = null;

    bool _isEvasion=false;
    bool _isStop = false;

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

        if (_isStop == false)
        {
            //回避準備中は攻撃できない
            if (_isEvasion == false)
            {
                Attack();
            }
            //攻撃時に他の動きができないように
            if (isAttack == false)
            {
                JudgeJump();
                Move();
                FlipX(_h);
            }


        }
        //回避モーション
        StartCoroutine(Evasion());
        CoolTimeE();
        E();

        JudgeBool();
        OnStop();


        HitEnemy();
        TimeCount();

    }


    void FixedUpdate()
    {
        if (_isStop==false)
        {
            //回避準備中は攻撃できない
            if (_isEvasion == false)
            {
                AttackMove();
            }
            Jump();
        }
    }




    IEnumerator Evasion()
    {
        //回避のクールタイム
        _timeE += Time.deltaTime;

        //回避のクールタイムが終わったか
        if (_isE)
        {
            if (_timelimitE < _timeE)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    //回避モーション中のbool
                    _isEvasion = true;

                    Debug.Log("222");
                    //回避中の時間
                    _iscoolTimeE = true;

                    _timeE = 0;
                    Time.timeScale = 0.5f;
                    
                    Debug.Log("ppp");
                    Vector2 mouspos = GameObject.Find("CrossHair").transform.position;
                    RaycastHit2D hit2D = Physics2D.Raycast(this.gameObject.transform.position, mouspos, _layerGound);
                    Debug.DrawLine(this.gameObject.transform.position, mouspos, Color.blue);

                    
                    if (hit2D.collider)
                    {

                    }
                    yield return new WaitForSeconds(0.1f);
                    _secondE = true;
                    

                }
            }
        }
    }

    void CoolTimeE()
    {
        if(_iscoolTimeE)
        {
        _coolTimeE += Time.deltaTime;
        }


        if (_coolTimeE < _coolTimeLimitE)
        {
            _isE = true;
        }
        else if (_coolTimeE > _coolTimeLimitE)  
        {
             _iscoolTimeE = false;
            _isE = false;
            _coolTimeE = 0;
            Time.timeScale = 1f;
           _secondE = false;

            //回避モーション中のbool
            _isEvasion = false;
        }
    }

   void E()
    {
        if (_secondE)
        {
            if (Input.GetMouseButtonDown(1))
            {
                //マウス座標に移動
                this.transform.position = mous.transform.position;
                _isStop = true;

                //エフェクト
                evasionEffect.SetTrigger("Evasion");
                anim.SetTrigger("Evasion");
                //回避モーション中のbool

                _isEvasion = false;
                _iscoolTimeE = false;
                _isE = false;
                _coolTimeE = 0;
                Time.timeScale = 1f;
                _secondE = false;
            }
        }
    }

    void OnStop() //アニメーションで使用
    {
        if (_isStop)
        {
            rb.velocity = Vector2.zero;
        }
    }
    void OffStop()  //アニメーションで使用
    {
        _isStop = false;
    }


    void HitEnemy()
    {
        if(_hitenemy)
        {
            _hitenemy = false;
            Time.timeScale = 0.5f;
            _timecount = true;

        }else
        {
            if(time>2)
            {
                _timecount = false;
                Time.timeScale = 1;
                time = 0;
            }
        }

    }

    void TimeCount()
    {
        if (_timecount)
        {
            time += Time.deltaTime;
        }
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
            Vector2 aaa = _attackMovePos.normalized;


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
            transform.localScale = new Vector3(a, this.transform.localScale.y, this.transform.localScale.z);




            //飛ばす
            if (_attackcount == 0)
            { 
                    rb.AddForce(aaa* _attackmove1, ForceMode2D.Impulse);
                    _attackcount++;

                


                if (rb.velocity.y > limitSpeed1Y)
                {
                    rb.velocity = new Vector2(rb.velocity.x, limitSpeed1Y);
                }
            }
            else if(_attackcount == 1)
            {
                rb.AddForce(aaa * _attackmove1, ForceMode2D.Impulse);
                _attackcount++;
                if (rb.velocity.y > limitSpeed2Y)
                {
                    rb.velocity = new Vector2(rb.velocity.x, limitSpeed2Y);
                }
                    if (a == 1 && rb.velocity.x > limitSpeed1X)
                {
                    rb.velocity = new Vector2(limitSpeed1X, rb.velocity.y);
                }
                else if (a == -1 && rb.velocity.x > -limitSpeed1X)
                {
                    rb.velocity = new Vector2(-limitSpeed1X, rb.velocity.y);
                }


            }
            else if(_attackcount>1)
            {
                rb.AddForce(aaa * _attackmove2, ForceMode2D.Impulse);

                
               if (rb.velocity.x < -limitSpeed2X)
                {
                    Debug.Log("LO");
                    rb.velocity = new Vector2(-limitSpeed2X, rb.velocity.y);
                }
                else if (a == 1 && rb.velocity.x > limitSpeed2X)
                {
                    Debug.Log("ok");
                    rb.velocity = new Vector2(limitSpeed2X, rb.velocity.y);
                }

                //if (rb.velocity.magnitude > limitSpeed2Y)
                //{
                //    rb.velocity = new Vector2(rb.velocity.x, limitSpeed2Y);
                //}
            }

            



            
            ppp = false;
            
            
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
        if (Input.GetKeyDown("w"))
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



        if (_h > 0 || _h < 0)
        {
            Vector2 velo = new Vector2(_h * _speed, rb.velocity.y);
            rb.velocity = velo;
        }
          
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

        //if (Input.GetKeyDown("w"))
        //{
        //    Vector2 velo = new Vector2(rb.velocity.x,);
        //    rb.velocity = velo;


        //}
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
            _attackcount = 0;
        }

        if(collision.gameObject.tag=="Enemy")
        {
            _hitenemy = true;

        }



    }


   






}
