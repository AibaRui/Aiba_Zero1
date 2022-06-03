using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemy : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] float _speed = 2;

    [SerializeField] float _cooltime = 3.0f;
             private float _time;

    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject player;
    [SerializeField] Transform _muzzle;
    [SerializeField] float _keepdistance=10;
    [SerializeField] float _comedistance=20;



    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }


     void Update()
    {
        Attack();
    }


    void FixedUpdate()
    {
        Move();
    }        


    void Attack()
    {
        _time += Time.deltaTime;


        
        Vector2 pos = player.transform.position - this.gameObject.transform.position;
        Vector2 dir = player.transform.position - _muzzle.transform.position;


        

        if (_cooltime<_time)
        {
            _time = 0;

            GameObject _bulletP = Instantiate(_bullet);
            _bulletP.GetComponent<BulletController>().pos = pos;
            _bulletP.GetComponent<BulletController>()._muzzle = _muzzle;
            _bulletP.GetComponent<BulletController>().player = player;
        }


    }

 

     void Move()
    {
        Vector3 enemypos = this.transform.position;
        Vector3 playerpos = GameObject.FindGameObjectWithTag("Player").transform.position;
        float dis = Vector3.Distance(enemypos, playerpos);
        enemypos.y = Vector2.zero.y;
        playerpos.y = Vector2.zero.y;

        if (dis < _comedistance && _keepdistance < dis)
            {
                _rb.velocity = (playerpos - enemypos).normalized * _speed;

            }
    
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "P_Attack")
        {

            Debug.Log("aa");

            //isDamage = true;
            //isDamage2 = true;

            Destroy(this.gameObject, 1.0f);
        }

    }



}
