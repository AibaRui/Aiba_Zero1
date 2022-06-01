using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] float _speed = 2;
    [SerializeField] float _nockback = 2;
    
    Rigidbody2D _rb;
    private bool isDamage=false;
    private bool isDamage2 = false;
    public Renderer _sp;
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _sp = GetComponent<Renderer>();
    }

     void FixedUpdate()
    {
        if (!isDamage && !isDamage2)
        {
            Vector3 enemypos = this.transform.position;
            Vector3 playerpos = GameObject.FindGameObjectWithTag("Player").transform.position;
            float dis = Vector3.Distance(enemypos, playerpos);
          //  enemypos.y = Vector2.zero.y;
            //playerpos.y = Vector2.zero.y;
            if (dis < 10)
            {
                _rb.velocity = (playerpos - enemypos).normalized * _speed;

            }
        }
        else if (isDamage)
        {
            _rb.AddForce(transform.right*_nockback, ForceMode2D.Impulse);
           // StartCoroutine(OnDamage());
            isDamage = false;

        }
    }





    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="P_Attack")
        {
            
            Debug.Log("aa");
            
            isDamage = true;
            isDamage2 = true;

        Destroy(this.gameObject,1.0f);
        }

    }

    //public IEnumerator OnDamage()
    //{

    //    //if (isDamage)
    //    //{
    //    //    yield break;
    //    //}








    //    for (int i = 0; i < 10; i++)
    //    {

    //        _sp.enabled = false;
    //        yield return new WaitForSeconds(0.05f);
    //        _sp.enabled = true;
    //        yield return new WaitForSeconds(0.05f);
    //    }

        

    //    // ’Êíó‘Ô‚É–ß‚·

    //    }



    }
