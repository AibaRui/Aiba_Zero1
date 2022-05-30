using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    

  

    void Start()
    {
        
        
    }

    // Update is called once per frame

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="P_Attack")
        {


        }

    }




}
