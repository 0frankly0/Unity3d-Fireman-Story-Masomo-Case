using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Bu kod ile mermiye fizik ve ateþleme sistemi ile beraber less accuracy eklemeyi amaçladým 

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

    public void Fire(Vector2 direction) // merminin ateþlenme sistemi ve açýsal deðerleri
    {
        float spreadAngle = Random.Range(-maxSpreadAngle, maxSpreadAngle);
        float spreadDistance = Random.Range(0f, maxSpreadDistance);

        Quaternion spreadRotation = Quaternion.Euler(0f, 0f, spreadAngle);
        Vector2 spreadOffset = spreadRotation * direction * spreadDistance;

        rb.velocity = (direction + spreadOffset) * speed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Ground") // mermi zeminle temasa geçerse 
        {
            Debug.Log("Çarptý Duvar");

            gameObject.SetActive(false); // mermiyi false yap

            // Çarpma efekti oluþturma
           _prefabImpactHolder =  Instantiate(_collisionEffectPrefab, collision.contacts[0].point, Quaternion.identity);

            // Çarpma sesini çalma
            AudioSource.PlayClipAtPoint(_collisionSound, collision.contacts[0].point);

            Destroy(_prefabImpactHolder, _effectDuration);  // 1 saniye sonra efekti yok et
        }

        if (collision.gameObject.tag == "Enemy") //  mermi düþmanla temasa geçerse
        {
            Debug.Log("Çarptý Enemy");

            NinjaHealth _ninjaHealthSc;

            NinjaAI _ninjaAI; 

            _ninjaAI = collision.gameObject.GetComponent<NinjaAI>();

            _ninjaHealthSc = collision.gameObject.GetComponent<NinjaHealth>();

            _ninjaHealthSc.decreaseHealthEnemy(20);

            _ninjaAI.moveSpeed = 0;

            gameObject.SetActive(false);

            // Çarpma efekti oluþturma
            _prefabImpactHolder = Instantiate(_bloodEffectPrefab, collision.contacts[0].point, Quaternion.identity); // düþmana deðdiðinde kan efekti oluþtursun

            // Çarpma sesini çalma
            AudioSource.PlayClipAtPoint(_bodyImpactSound, collision.contacts[0].point);

            Destroy(_prefabImpactHolder, _effectDuration);  // 1 saniye sonra efekti yok et
        }

    }
}

