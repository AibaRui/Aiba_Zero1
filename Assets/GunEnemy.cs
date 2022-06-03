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

        if(_cooltime<_time)
        {
            _time = 0;

            GameObject _bulletP = Instantiate(_bullet);

        }


    }

 

     void Move()
    {
        Vector3 enemypos = this.transform.position;
        Vector3 playerpos = GameObject.FindGameObjectWithTag("Player").transform.position;
        float dis = Vector3.Distance(enemypos, playerpos);
        enemypos.y = Vector2.zero.y;
        playerpos.y = Vector2.zero.y;

        if (dis < 10 && 5 < dis)
            {
                _rb.velocity = (playerpos - enemypos).normalized * _speed;

            }
    
    }






}
