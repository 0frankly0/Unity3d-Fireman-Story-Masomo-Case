using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Bu kodu finite state machine kullanmadan tasarladým. Amacým iki tarzý da sabitleþtirmek ve geliþtirmek! karakterin hareketinden sorumludur

    public bool _rightMove = false, _leftMove = false;

    Rigidbody2D _rig;

    public float _speed = 2f,_jumpTimer = 0;

    public Vector3 _moveDirection;

    Animator _anim;

    SpriteRenderer _spriteRenderer;

    public Transform _bulletSpawnPos;

    public GameObject _footstepSound, _jumpFx,_jumpSound; 

    private void Awake()
    {
        _rig = this.gameObject.GetComponent<Rigidbody2D>();

        _moveDirection = new Vector3(1,0,transform.position.z);

        _anim = this.gameObject.GetComponent<Animator>();

        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

    }
    public void Update()
    {
        if (_rightMove)
        {
            _anim.SetBool("isMove", true);
            _rig.velocity = new Vector2(_speed, _rig.velocity.y);
        }
        else if (_leftMove)
        {
            _anim.SetBool("isMove", true);
            _rig.velocity = new Vector2(-_speed, _rig.velocity.y);
        }
        else
        {
            _anim.SetBool("isMove", false);
            _rig.velocity = new Vector2(0f, _rig.velocity.y);
        }

        if (_rightMove || _leftMove)
        {
            _anim.SetBool("isMove", true);
        }

    }

    public void rightMoveOn()
    {
        _rightMove = true;
        _footstepSound.SetActive(true);
        _bulletSpawnPos.localRotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void rightMoveOff()
    {
        _footstepSound.SetActive(false);
        _rightMove = false;
    }

    public void leftMoveOn()
    {
        _footstepSound.SetActive(true);
        _leftMove = true;
        _bulletSpawnPos.localRotation = Quaternion.Euler(0,180, 180);
        transform.rotation = Quaternion.Euler(0,-180f,0);
    }

    public void leftMoveOff()
    {
        _footstepSound.SetActive(false);
        _leftMove = false; 
    }

    public void JumpOn()
    {
       
            _rig.AddForce(new Vector2(0f, 6f), ForceMode2D.Impulse);
            StartCoroutine(jumpController());
    }

    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "Ground")
    //    {
    //        _jumpMove = true; 
    //    }
    //}

    //public void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        _jumpMove = false;
    //    }
    //}

    IEnumerator jumpController()
    {
        _jumpSound.SetActive(true);
        _anim.SetBool("isJump", true);
        _jumpFx.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        _anim.SetBool("isJump", false);
        _jumpFx.SetActive(false);
        _jumpSound.SetActive(false);

    }

}
