using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaHealth : MonoBehaviour
{
    // ninja enemy nin health sistemini tanýmladýðým kod dosyasýdýr

    public int _enemyHealth = 100;

    Animator _animatorEnemy;

    SpriteRenderer _spriteRenderer; 

    NinjaAI _nýnjaAISc;

    BoxCollider2D _colliderEnemy;

    Rigidbody2D _rigEnemy;

    RoundManager _roundManagerSc;

    void Start()
    {
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>(); 
        _animatorEnemy = this.gameObject.GetComponent<Animator>();
        _roundManagerSc = GameObject.Find("RoundManager").GetComponent<RoundManager>();
        _nýnjaAISc = this.gameObject.GetComponent<NinjaAI>();
        _colliderEnemy = this.gameObject.GetComponent<BoxCollider2D>();
        _rigEnemy = this.gameObject.GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
    if(_enemyHealth <= 0)
        {
            Dead();
            _colliderEnemy.enabled = false;
            _rigEnemy.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }

    public void decreaseHealthEnemy(int _healthDown)
    {
        _enemyHealth -= _healthDown;
        StartCoroutine(EnemyHurt());

        if(_enemyHealth <= 0)
        {
            _roundManagerSc.EnemyKilled();
        }
    }

    public void Dead()
    {
        _nýnjaAISc.enabled = false;
        _animatorEnemy.SetBool("isDead", true);
        Destroy(this.gameObject, 2f);
    }

    public IEnumerator EnemyHurt()
    {
        _spriteRenderer.color = Color.black; 
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.white;
    }

}
