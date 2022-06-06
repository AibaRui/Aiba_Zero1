using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsJudge : MonoBehaviour
{
    public bool _isEvents;
    private bool _judge=false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(_judge)
        {
            if(Input.GetKeyDown("space"))
            {
                Debug.Log("Event");

                _isEvents = true;

            }


        }


    }

    
    void StopEvent()
    {
        _isEvents = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _judge = true;


        }
    }

    private void OnTriggerExit2D(Collider2D collision1)
    {
        if (collision1.gameObject.tag == "Player")
        {
            _judge = false;

        }
    }




}
