using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class RoundManager : MonoBehaviour
{
    // Spawnlama noktasý
    public GameObject spawnPoint;
    // Spawnlanacak obje
    public GameObject objectToSpawn;
    // Spawnlanacak obje sayýsý
    public int spawnCount;
    // Spawnlama koþulu
    public bool spawnCondition;

    // Yarým saniyelik zaman aralýðý
    private float spawnInterval = 0.5f;
    // Zaman sayacý
    private float timer = 0f;
    // Spawnlanan obje sayacý
    private int spawned = 0;

    // Round numarasý
    private int round = 1;
    // Roundda öldürülen düþman sayýsý
    public int roundKills = 0,_finalKillValue = 0;
    // Roundun bitmesi için gerekli düþman öldürme sayýsý
    private int roundKillLimit;
    // Her roundda spawnlanacak düþman sayýsýnýn artýþ miktarý
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

        // Eðer spawnlama koþulu saðlandýysa ve spawnlanan obje sayýsý istenen sayýdan azsa
        if (spawnCondition && spawned < spawnCount)
        {
            // Zaman sayacýný arttýr
            timer += Time.deltaTime;
            // Eðer zaman sayacý zaman aralýðýndan büyük veya eþitse
            if (timer >= spawnInterval)
            {
                // Zaman sayacýný sýfýrla
                timer = 0f;
                // Spawn noktasýndan objeyi spawnla
                Instantiate(objectToSpawn, spawnPoint.transform.position, Quaternion.identity);
                // Spawnlanan obje sayacýný arttýr
                spawned++;
            }
        }

        // Eðer roundda öldürülen düþman sayýsý limiti aþtýysa
        if (roundKills >= roundKillLimit)
        {
            // Roundu bir arttýr
            round++;
            // Roundda öldürülen düþman sayýsýný sýfýrla
            roundKills = 0;
            // Spawnlanacak obje sayýsýný arttýr
            spawnCount += roundSpawnIncrease;
            // Spawnlanan obje sayacýný sýfýrla
            spawned = 0;

            StartCoroutine(RoundShowerText());
        }
    }

    // Düþman öldürüldüðünde çaðrýlacak fonksiyon
    public void EnemyKilled()
    {
        // Roundda öldürülen düþman sayacýný arttýr
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
