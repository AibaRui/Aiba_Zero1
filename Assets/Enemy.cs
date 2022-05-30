using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float _nockback = 2;
    Rigidbody2D rb;
    private bool isDamage;
    public Renderer sp;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sp = GetComponent<Renderer>();
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        

    
    
        
    
        if(collision.gameObject.tag=="P_Attack")
        {
            StartCoroutine(OnDamage());
            Debug.Log("aa");
            
            rb.AddForce(transform.right*_nockback, ForceMode2D.Impulse);

        Destroy(this.gameObject,1.5f);
        }

    }

    public IEnumerator OnDamage()
    {

        if (isDamage)
        {
            yield break;
        }

        isDamage = true;





        
        for (int i = 0; i < 10; i++)
        {

            sp.enabled = false;
            yield return new WaitForSeconds(0.05f);
            sp.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }

        isDamage = false;

        // ’Êíó‘Ô‚É–ß‚·

    }



}
