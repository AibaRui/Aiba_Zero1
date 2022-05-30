using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Contorol : MonoBehaviour
{
    //ˆÚ“®
    public float _h ;   
    [SerializeField] float _speed;
    [SerializeField] float StartSpeed;
    [SerializeField] float MinasStartSpeed;
   
    

    //ƒWƒƒƒ“ƒv
    private bool OnGround;
    private bool jump;
    [SerializeField]   float jumpPower= 10f;
    [SerializeField] int jumpLimit=2;
    public int jumpCount=0;

    float p_ScaleX;


    [SerializeField] float _attackmove1 = 5;
    [SerializeField] Transform _crosshair;
    




    Rigidbody2D rb;
    private Animator anim = null;
    [SerializeField] Animator runEfect = null;
    [SerializeField] Animator attackEffect = null;

    [SerializeField] float _attackinterval = 1f;
    float m_timer;
    private bool isAttack;

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
        if (isAttack==false)
        {
            JudgeJump();
            Move();

        }
       

        FlipX(_h);
       
        JudgeBool();


        

       
        
    } 


     void FixedUpdate()
    {
        Jump();


         Vector2 dir = _crosshair.position - transform.position;
        
    }



    void Attack()
    {
        m_timer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1")&&m_timer > _attackinterval)
        {
            m_timer = 0;
            attackEffect.SetTrigger("Attack");
            anim.SetTrigger("Attack 0");
            isAttack = true;
        }
        else
        {
        isAttack = false;

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







    //ˆÚ“®
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


    // ŠG‚ÌU‚èŒü‚«
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
