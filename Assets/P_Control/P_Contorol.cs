using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Contorol : MonoBehaviour
{
    //ˆÚ“®
    public float p_h ;
    [SerializeField] float p_movePower = 5f;
    
    [SerializeField] float moveSpeed;
    [SerializeField] float StartSpeed;
    [SerializeField] float MinasStartSpeed;

    

    //ƒWƒƒƒ“ƒv
    //private bool OnGround;
    private bool jump;
  [SerializeField]   float jumpPower= 10f;
    [SerializeField] int jumpLimit=2;
    public int jumpCount=0;

    float p_ScaleX;

    Rigidbody2D rb;
    private Animator anim = null;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        p_h = Input.GetAxisRaw("Horizontal");
        FlipX(p_h);


        if (Input.GetButtonDown("Jump"))
        {
            
            if(jumpCount<jumpLimit)
            {
                jump = true;
               
            }
            

        }


    } 


     void FixedUpdate()
    {


        if (rb.velocity.magnitude < 10)
        {
            rb.AddForce(Vector2.right * p_h * p_movePower, ForceMode2D.Force);
        }

        
        if(p_h < 0 || p_h > 0)
        {
            anim.SetBool("run", true);
        }
        else {anim.SetBool("run", false);
 }

        

        if (jump)
        {
            Debug.Log("BBB");
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
            jumpCount++;

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
            jumpCount = 0;
        }

    }


}
