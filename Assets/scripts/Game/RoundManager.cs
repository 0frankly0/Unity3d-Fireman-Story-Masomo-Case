using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class RoundManager : MonoBehaviour
{
    // Spawnlama noktas�
    public GameObject spawnPoint;
    // Spawnlanacak obje
    public GameObject objectToSpawn;
    // Spawnlanacak obje say�s�
    public int spawnCount;
    // Spawnlama ko�ulu
    public bool spawnCondition;

    // Yar�m saniyelik zaman aral���
    private float spawnInterval = 0.5f;
    // Zaman sayac�
    private float timer = 0f;
    // Spawnlanan obje sayac�
    private int spawned = 0;

    // Round numaras�
    private int round = 1;
    // Roundda �ld�r�len d��man say�s�
    public int roundKills = 0,_finalKillValue = 0;
    // Roundun bitmesi i�in gerekli d��man �ld�rme say�s�
    private int roundKillLimit;
    // Her roundda spawnlanacak d��man say�s�n�n art�� miktar�
    private int roundSpawnIncrease = 1;

    public TextMeshProUGUI _roundValueText, _roundTotalKillValueText, _roundShowerAnimText,_gameOverTotalKillText;

    public GameObject _roundShowerAnimObject,_roundSoundObject; 

    public void Awake()
    {
        StartCoroutine(RoundShowerText());
    }

    void Update()
    {
        roundKillLimit = spawnCount;

        _roundValueText.text = "ROUND: " + round;
        _roundTotalKillValueText.text = "ENEMY: " + roundKillLimit;
        _gameOverTotalKillText.text = "KILL: " + _finalKillValue;

        // E�er spawnlama ko�ulu sa�land�ysa ve spawnlanan obje say�s� istenen say�dan azsa
        if (spawnCondition && spawned < spawnCount)
        {
            // Zaman sayac�n� artt�r
            timer += Time.deltaTime;
            // E�er zaman sayac� zaman aral���ndan b�y�k veya e�itse
            if (timer >= spawnInterval)
            {
                // Zaman sayac�n� s�f�rla
                timer = 0f;
                // Spawn noktas�ndan objeyi spawnla
                Instantiate(objectToSpawn, spawnPoint.transform.position, Quaternion.identity);
                // Spawnlanan obje sayac�n� artt�r
                spawned++;
            }
        }

        // E�er roundda �ld�r�len d��man say�s� limiti a�t�ysa
        if (roundKills >= roundKillLimit)
        {
            // Roundu bir artt�r
            round++;
            // Roundda �ld�r�len d��man say�s�n� s�f�rla
            roundKills = 0;
            // Spawnlanacak obje say�s�n� artt�r
            spawnCount += roundSpawnIncrease;
            // Spawnlanan obje sayac�n� s�f�rla
            spawned = 0;

            StartCoroutine(RoundShowerText());
        }
    }

    // D��man �ld�r�ld���nde �a�r�lacak fonksiyon
    public void EnemyKilled()
    {
        // Roundda �ld�r�len d��man sayac�n� artt�r
        roundKills++;
        _finalKillValue++;
    }

    public IEnumerator RoundShowerText()
    {
        _roundShowerAnimText.text = "ROUND  " + round;
        _roundSoundObject.SetActive(true);
        _roundShowerAnimObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        _roundShowerAnimObject.SetActive(false);
        _roundSoundObject.SetActive(false);
    }
}
