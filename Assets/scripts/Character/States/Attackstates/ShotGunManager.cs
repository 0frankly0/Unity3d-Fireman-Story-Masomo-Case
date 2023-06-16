using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.EventSystems; 

public class ShotGunManager : MonoBehaviour
{
    // Bu kod ile shotgun silah ate�lemesini ayarl�yoruz

    public ObjectPooling _bulletPool;
    public Transform _firePoint;
    private bool _canFire = true, _isFiring = false;

    public float _fireInterval = 0.2f; // Ate�leme aral���n� ayarl�yoruz (frame rate firing)
    private float _fireTimer = 0f, _recoilForce = 150f;

    public int _burstCount = 3; // Ate�leme miktar�
    public float _burstInterval = 0.1f; // Ate�lemeler aras� s�re
    private int _currentBurstCount = 0;
    private float _burstTimer = 0f;

    public Rigidbody2D _rigCharacter;
    public CameraShake _cameraShakeSc;
    public GameObject _firingSound, _fireFx,_shotGunBt;

    public void Update()
    {
        if (_isFiring)
        {
            _burstTimer += Time.deltaTime; // Timer'� art�r

            if (_burstTimer >= _burstInterval)
            {
                FireBullet();
                _currentBurstCount++;

                if (_currentBurstCount >= _burstCount)
                {
                    _isFiring = false; // Belirtilen ate�leme miktar�na ula��ld���nda ate�lemeyi durdur
                    _burstTimer = 0f; // Timer'� s�f�rla
                }
                else
                {
                    _burstTimer = 0f; // Ate�leme aral���na ula��ld���nda timer'� s�f�rla
                }
            }
        }
    }
    public void FireBullet()
    {
        GameObject bulletObject = _bulletPool.GetBullet();
        StartCoroutine(shotgunSoundController());

        for (int i = 0; i < _bulletPool.bullets.Count; i++)
        {
            _bulletPool.bullets[i].gameObject.GetComponent<Bullet>().maxSpreadAngle = 110; // E�er shotgun ise a��sal de�eri art�r�p isabet oran� azalt�yoruz
        }

        if (bulletObject != null)
        {
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bulletObject.transform.position = _firePoint.position;
            bulletObject.transform.rotation = _firePoint.rotation;
            bullet.Fire(_firePoint.right);

            // Geri tepme uygula. Karaktere geri tepme veriyoruz burda
            Vector2 recoilDirection = -_firePoint.right;
            _rigCharacter.AddForce(recoilDirection * _recoilForce);

            _cameraShakeSc.ShakeCamera(); // kameraya sars�nt� efekti ekledik

            _canFire = false;
            StartCoroutine(ResetFire());
        }
    }
    private IEnumerator ResetFire()
    {
        _shotGunBt.gameObject.GetComponent<EventTrigger>().enabled = false;
        _shotGunBt.gameObject.GetComponent<Image>().color = Color.red;  
        _fireFx.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        _canFire = true;
        _fireFx.SetActive(false);
        _shotGunBt.gameObject.GetComponent<EventTrigger>().enabled = true;
        _shotGunBt.gameObject.GetComponent<Image>().color = Color.white;
    }
    public void firingOn()
    {
        _isFiring = true;
        _currentBurstCount = 0;
       
    }
    public void firingOff()
    {
        _isFiring = false;
        _fireTimer = 0f;
        _burstTimer = 0f;
    }

    public IEnumerator shotgunSoundController()
    {
        _firingSound.SetActive(true);
        yield return new WaitForSeconds(0.9f);
        _firingSound.SetActive(false);
    }
   
}
