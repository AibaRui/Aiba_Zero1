using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{


    [SerializeField] float Destroytime=0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        Destroy(this.gameObject,Destroytime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "P_Attack")
        {

            gameObject.layer = LayerMask.NameToLayer("DownEnemy");
            Debug.Log("D");

        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "P_Attack")
        {
            gameObject.layer = LayerMask.NameToLayer("DownEnemy");

            Debug.Log("DS");

        }


    }

}