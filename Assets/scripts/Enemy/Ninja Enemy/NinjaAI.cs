using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaAI : MonoBehaviour
{
    // ninja karakterli enemy düþmanýnýn yapay zeka kodudur 

    public Transform[] patrolPoints; // varýþ noktalarý
    public float moveSpeed;
    public float waitTime = 2f; // Bekleme süresi

    private int currentPointIndex = 0;
    private bool isWaiting = false; // Bekleme durumu kontrolü
    private float waitTimer = 0f; // Bekleme süresini hesaplamak için kullanýlan zamanlayýcý

    public RightDedectorState _rightDedectorStateSc;
    public LeftDedectorState _leftDedectorStateSc;

    Animator _animEnemy;

    CharacterHealth _characterHealthSc;

    
    private void Start()
    {
        patrolPoints = new Transform[2]; // listin uzunlugunu belirledim

        // prefab olarak spawnlanacaðý için oyunda bulunan objeleri ismiyle çaðýrdým
        _characterHealthSc = GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterHealth>();
        _animEnemy = this.gameObject.GetComponent<Animator>();
        patrolPoints[0] = GameObject.Find("patrolpoz").GetComponent<Transform>();
        patrolPoints[1] = GameObject.Find("patrolpoz (1)").GetComponent<Transform>();
       
        transform.position = patrolPoints[currentPointIndex].position;
        _animEnemy.SetBool("isMove", true);
        _animEnemy.SetBool("isAttack", false);
        UpdateTargetPoint();
    }

    private void Update()
    {
        moveSpeed = Random.Range(0f, 6f);

        if(currentPointIndex % 2 == 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (!_rightDedectorStateSc._isTouchedCharacterRight && !_leftDedectorStateSc._isTouchedCharacterLeft && !isWaiting)
        {
            _animEnemy.SetBool("isAttack", false);
            _animEnemy.SetBool("isMove", true);
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, moveSpeed * Time.deltaTime);

            if (transform.position == patrolPoints[currentPointIndex].position)
            {
                StartCoroutine(WaitBeforeNextPoint());
            }
        }
        else if (_rightDedectorStateSc._isTouchedCharacterRight)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            _animEnemy.SetBool("isMove", false);
            _animEnemy.SetBool("isAttack", true );
        }
        else if (_leftDedectorStateSc._isTouchedCharacterLeft)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            _animEnemy.SetBool("isMove", false);
            _animEnemy.SetBool("isAttack", true);
        }
    }

    private System.Collections.IEnumerator WaitBeforeNextPoint()
    {
        isWaiting = true;
        _animEnemy.SetBool("isMove", false); 
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        _animEnemy.SetBool("isMove", true);
        UpdateTargetPoint();
    }

    private void UpdateTargetPoint()
    {
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
    }
    public void DamageGive()
    {
        _characterHealthSc.DecreaseHealth(0.1f);
    }
   
}

