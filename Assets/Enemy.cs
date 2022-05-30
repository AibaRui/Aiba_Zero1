using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    

  

    void Start()
    {
        
        
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        
    
    
        
    
        if(collision.gameObject.tag=="P_Attack")
        {
            Debug.Log("aa");
            Destroy(this.gameObject,0.1f);

        }

    }

    



}
