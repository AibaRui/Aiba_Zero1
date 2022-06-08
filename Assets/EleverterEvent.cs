using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleverterEvent : MonoBehaviour
{

   [SerializeField]  EventsJudge eventsJudge;
    [SerializeField] PowerEL power;
    private bool _isEleverter=false;

    private bool _OnSquript=true;


    [SerializeField] Transform pos;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�X�N���v�g�̋@�\
        if (_OnSquript)
        {�@�@//�d�����ꂽ��
            if (power._power == true)
            {
                //�C�x���g�ɓ�������
                if (eventsJudge._isEvents)

                {   //�ꏊ���G���x�[�^�[��
                    if (_isEleverter)
                    {

                        Debug.Log("Eleverter");

                        GameObject p = GameObject.FindGameObjectWithTag("Player");
                        p.transform.position = new Vector2(-10, 18);
                           


                    }



                }
            }
        }

       if (_OnSquript)
        {
            if (power._power==false)
            {
                if (eventsJudge._isEvents)
                {
                    if(_isEleverter)
                    {
                        Debug.Log("NOPOWEr");


                    }


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
