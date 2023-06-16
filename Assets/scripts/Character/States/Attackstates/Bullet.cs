using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Bu kod ile mermiye fizik ve ate�leme sistemi ile beraber less accuracy eklemeyi ama�lad�m 

    public float speed = 70f;
    public float maxSpreadAngle = 50f;
    public float maxSpreadDistance = 0.2f;
    public float reuseDelay = 0.3f;

    private Rigidbody2D rb;

    public GameObject _collisionEffectPrefab,_bloodEffectPrefab,_prefabImpactHolder;
    public AudioClip _collisionSound,_bodyImpactSound;
    public float _effectDuration = 0.01f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Invoke("ReuseBullet", reuseDelay);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void ReuseBullet()
    {
        gameObject.SetActive(false);
    }

    public void Fire(Vector2 direction) // merminin ate�lenme sistemi ve a��sal de�erleri
    {
        float spreadAngle = Random.Range(-maxSpreadAngle, maxSpreadAngle);
        float spreadDistance = Random.Range(0f, maxSpreadDistance);

        Quaternion spreadRotation = Quaternion.Euler(0f, 0f, spreadAngle);
        Vector2 spreadOffset = spreadRotation * direction * spreadDistance;

        rb.velocity = (direction + spreadOffset) * speed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Ground") // mermi zeminle temasa ge�erse 
        {
            Debug.Log("�arpt� Duvar");

            gameObject.SetActive(false); // mermiyi false yap

            // �arpma efekti olu�turma
           _prefabImpactHolder =  Instantiate(_collisionEffectPrefab, collision.contacts[0].point, Quaternion.identity);

            // �arpma sesini �alma
            AudioSource.PlayClipAtPoint(_collisionSound, collision.contacts[0].point);

            Destroy(_prefabImpactHolder, _effectDuration);  // 1 saniye sonra efekti yok et
        }

        if (collision.gameObject.tag == "Enemy") //  mermi d��manla temasa ge�erse
        {
            Debug.Log("�arpt� Enemy");

            NinjaHealth _ninjaHealthSc;

            NinjaAI _ninjaAI; 

            _ninjaAI = collision.gameObject.GetComponent<NinjaAI>();

            _ninjaHealthSc = collision.gameObject.GetComponent<NinjaHealth>();

            _ninjaHealthSc.decreaseHealthEnemy(20);

            _ninjaAI.moveSpeed = 0;

            gameObject.SetActive(false);

            // �arpma efekti olu�turma
            _prefabImpactHolder = Instantiate(_bloodEffectPrefab, collision.contacts[0].point, Quaternion.identity); // d��mana de�di�inde kan efekti olu�tursun

            // �arpma sesini �alma
            AudioSource.PlayClipAtPoint(_bodyImpactSound, collision.contacts[0].point);

            Destroy(_prefabImpactHolder, _effectDuration);  // 1 saniye sonra efekti yok et
        }

    }
}

