using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerEL : MonoBehaviour
{

    [SerializeField] EventsJudge eventsJudge;
    private bool _judge=false;
    public bool _power=false;


    private bool _OnSquript=true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (_OnSquript)
        {
            if (eventsJudge._isEvents)
            {
                if (_judge)
                {
                    _power = true;
                    Debug.Log("On");
                }
            }
        }
    }

    void OffSquript()
    {
        _OnSquript = false;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _judge = true;

        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _judge = false;

        }




    }




}
