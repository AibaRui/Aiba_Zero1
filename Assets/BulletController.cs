using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //�e����ԑ���
    [SerializeField] float _speed = 3f;
    //�e�̐�������
    [SerializeField] float _deleteTime = 5f;



            public Transform _muzzle;
              public GameObject player;

    public int _scale;

    public Vector2 pos = new Vector2(0,0);


    void Start()
    {

        this.transform.position = _muzzle.transform.position;
        //Vector2 pos = player.transform.position - this.gameObject.transform.position;
        Vector2 dir = player.transform.position - _muzzle.transform.position;
        transform.right = dir;
        
        

        // �e���v���C���[�ɔ�΂�
        Rigidbody2D _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = pos.normalized * _speed;
        // �e�̐�������
        


        Destroy(this.gameObject, _deleteTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      if(collision.gameObject.tag=="Player")
        {
            Destroy(this.gameObject);
        }

    }


}
