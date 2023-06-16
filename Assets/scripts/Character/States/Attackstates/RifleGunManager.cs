using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleGunManager : MonoBehaviour
{
    // Bu kod ile otomatik silah ate�lemesini ayarl�yoruz

    public ObjectPooling _bulletPool;
    public Transform _firePoint;
    private bool _canFire = true, _isFiring = false;

    public float _fireInterval = 0.2f; // Ate�leme aral���n� ayarl�yoruz (frame rate firing)
    private float _fireTimer = 0f, _recoilForce = 150f;

    public Rigidbody2D _rigCharacter;
    public CameraShake _cameraShakeSc;
    public GameObject _firingSound,_fireFx; 

    public void Update()
    {
        if(_isFiring)
        {
            _fireTimer += Time.deltaTime; // Timer'� art�r

            if (_fireTimer >= _fireInterval)
            {
                FireBullet();
                _fireTimer = 0f; // Timer ate�leme aral���na ula�t���nda s�f�rla
            }
        }
    }
    public void FireBullet()
    {

        GameObject bulletObject = _bulletPool.GetBullet();
        for (int i = 0; i < _bulletPool.bullets.Count; i++)
        {
            _bulletPool.bullets[i].gameObject.GetComponent<Bullet>().maxSpreadAngle = 50;// E�er shotgun ise a��sal de�eri azalt�p isabet oran� artt�r�yoruz
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
        _fireFx.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        _canFire = true;
        _fireFx.SetActive(false);

    }
    // Ui �zerindeki ate�leme butonlar�n� aktifle�tirdik 
    public void firingOn()
    {
        _isFiring = true;
        _firingSound.SetActive(true);
    }
    public void firingOff()
    {
        _isFiring = false; 
        _fireTimer = 0f;
        _firingSound.SetActive(false);
    }


}
