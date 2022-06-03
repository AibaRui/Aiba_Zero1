using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //�e����ԑ���
    [SerializeField] float _speed = 3f;
    //�e�̐�������
    [SerializeField] float _deleteTime = 5f;



    [SerializeField] GameObject _muzzle;
    [SerializeField] GameObject player;

    public int _scale;
    void Start()
    {

        this.transform.position = _muzzle.transform.position;
        Vector2 pos = player.transform.position - this.gameObject.transform.position;
        Vector2 dir = player.transform.position - _muzzle.transform.position;
        transform.right = dir;



        // �e���v���C���[�ɔ�΂�
        Rigidbody2D _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = pos.normalized * _speed;
        // �e�̐�������
        Destroy(this.gameObject, _deleteTime);
    }
}
