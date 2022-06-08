using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Contorol : MonoBehaviour
{
    //�ړ�
    public float _h;
    [SerializeField] float _speed;
    [SerializeField] float StartSpeed;
    [SerializeField] float MinasStartSpeed;



    //�W�����v
    private bool OnGround;
    private bool jump;
    [SerializeField] float jumpPower = 10f;
    [SerializeField] int jumpLimit = 2;

    public int jumpCount = 0;

    float p_ScaleX;

    //�U��
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



    //���
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
            //����������͍U���ł��Ȃ�
            if (_isEvasion == false)
            {
                Attack();
            }
            //�U�����ɑ��̓������ł��Ȃ��悤��
            if (isAttack == false)
            {
                JudgeJump();
                Move();
                FlipX(_h);
            }


        }
        //������[�V����
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
            //����������͍U���ł��Ȃ�
            if (_isEvasion == false)
            {
                AttackMove();
            }
            Jump();
        }
    }




    IEnumerator Evasion()
    {
        //����̃N�[���^�C��
        _timeE += Time.deltaTime;

        //����̃N�[���^�C�����I�������
        if (_isE)
        {
            if (_timelimitE < _timeE)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    //������[�V��������bool
                    _isEvasion = true;

                    Debug.Log("222");
                    //��𒆂̎���
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

            //������[�V��������bool
            _isEvasion = false;
        }
    }

   void E()
    {
        if (_secondE)
        {
            if (Input.GetMouseButtonDown(1))
            {
                //�}�E�X���W�Ɉړ�
                this.transform.position = mous.transform.position;
                _isStop = true;

                //�G�t�F�N�g
                evasionEffect.SetTrigger("Evasion");
                anim.SetTrigger("Evasion");
                //������[�V��������bool

                _isEvasion = false;
                _iscoolTimeE = false;
                _isE = false;
                _coolTimeE = 0;
                Time.timeScale = 1f;
                _secondE = false;
            }
        }
    }

    void OnStop() //�A�j���[�V�����Ŏg�p
    {
        if (_isStop)
        {
            rb.velocity = Vector2.zero;
        }
    }
    void OffStop()  //�A�j���[�V�����Ŏg�p
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
        isAttack=false;�@�@ //Animation�C�x���g�ōU�����[�V�������I���������s
    }

  
    void AttackMove()
    {
        if (ppp == true)
        {
            //�v���C���[�̈ʒu
            Vector2 _pPos = this.gameObject.transform.position;

            //�}�E�X�̈ʒu
            Vector2 _mousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //�x�N�g��
            Vector2 _attackMovePos = _mousPos - _pPos;
            Vector2 aaa = _attackMovePos.normalized;


           //�U�����̌���
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




            //��΂�
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
    //�ړ�
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


    // �G�̐U�����
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
