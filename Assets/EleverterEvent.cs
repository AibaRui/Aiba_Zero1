using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleverterEvent : MonoBehaviour
{

   [SerializeField]  EventsJudge eventsJudge;
    private bool _isEleverter=false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(eventsJudge._isEvents)

        {
            if(_isEleverter)
            {
                if(Input.GetKeyDown("space"))
                {
                    Debug.Log("Eleverter");

                }



            }



        }



    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            _isEleverter = true;

        }

    }


   private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isEleverter = false;

        }




    }




}
