using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] float _Xattackmove1 = 5;
    private bool on =false;
    float time;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        time += Time.deltaTime;
if (Input.GetButtonDown("Fire1"))
        {
            on = true;
        }
    }

    void FixedUpdate()
    {

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if(on==true )
        { 
        Vector2 _pPos = this.gameObject.transform.position;
        Vector2 _mousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 _attackMovePos = _mousPos - _pPos;
            
            rb.AddForce(_attackMovePos * _Xattackmove1, ForceMode2D.Impulse);
            on = false;
        }

    }
}