using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    // Bu kod sayesinde belirli sayýdaki mermiyi üretip sürekli onlarý kullanmayý amaçladým

    public GameObject _bulletPrefab;
    public int _poolSize;

    public List<GameObject> bullets;

    private void Awake()
    {
        bullets = new List<GameObject>();

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab);
            bullet.SetActive(false);
            bullets.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }

        return null;
    }
}
