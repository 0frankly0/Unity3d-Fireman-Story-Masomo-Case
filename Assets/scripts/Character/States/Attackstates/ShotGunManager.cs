using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.EventSystems; 

public class ShotGunManager : MonoBehaviour
{
    // Bu kod ile shotgun silah ateþlemesini ayarlýyoruz

    public ObjectPooling _bulletPool;
    public Transform _firePoint;
    private bool _canFire = true, _isFiring = false;

    public float _fireInterval = 0.2f; // Ateþleme aralýðýný ayarlýyoruz (frame rate firing)
    private float _fireTimer = 0f, _recoilForce = 150f;

    public int _burstCount = 3; // Ateþleme miktarý
    public float _burstInterval = 0.1f; // Ateþlemeler arasý süre
    private int _currentBurstCount = 0;
    private float _burstTimer = 0f;

    public Rigidbody2D _rigCharacter;
    public CameraShake _cameraShakeSc;
    public GameObject _firingSound, _fireFx,_shotGunBt;

    public void Update()
    {
        if (_isFiring)
        {
            _burstTimer += Time.deltaTime; // Timer'ý artýr

            if (_burstTimer >= _burstInterval)
            {
                FireBullet();
                _currentBurstCount++;

                if (_currentBurstCount >= _burstCount)
                {
                    _isFiring = false; // Belirtilen ateþleme miktarýna ulaþýldýðýnda ateþlemeyi durdur
                    _burstTimer = 0f; // Timer'ý sýfýrla
                }
                else
                {
                    _burstTimer = 0f; // Ateþleme aralýðýna ulaþýldýðýnda timer'ý sýfýrla
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
            _bulletPool.bullets[i].gameObject.GetComponent<Bullet>().maxSpreadAngle = 110; // Eðer shotgun ise açýsal deðeri artýrýp isabet oraný azaltýyoruz
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

            _cameraShakeSc.ShakeCamera(); // kameraya sarsýntý efekti ekledik

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
