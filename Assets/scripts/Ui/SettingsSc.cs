using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingsSc : MonoBehaviour
{
    

    public GameObject ses_on, ses_off; // hem on hem off butonuna erisildi 

    public int Kalite_Ýndex,starterControlValue = 0;

    public void Awake()
    {
        

        if(starterControlValue == 0)
        {
            PlayerPrefs.SetInt("ses", 1);
        }
    }
    public void Update()
    {
        if(PlayerPrefs.GetInt("ses") == 1)
        {
            ses_on.SetActive(true);
            ses_off.SetActive(false);
        }
        else
        {
            ses_on.SetActive(false);
            ses_off.SetActive(true);
        }
    }

    public void sesDurum(string durum) // burdaki mantýk tusa tikladigimizda butonlarda atadigimiz sitring durumuna gore aktiflestirip kapatmak. 
    {
        if(durum == "acik") // eger string durumu aciksa yani on butonu aktifse 
        {
            ses_on.SetActive(false);
            ses_off.SetActive(true);
            PlayerPrefs.SetInt("ses", 0); // ses playerprefsini 0 yaptým
        }
        else if (durum == "kapali")
        {
            ses_on.SetActive(true);
            ses_off.SetActive(false);
            PlayerPrefs.SetInt("ses", 1);
        }
    }


    // eger grafik kalitesi eklemek istersek diye yazdýðým kod
    public void Load_Kalite()
    {
        if (PlayerPrefs.HasKey("Grafik"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Grafik"));
        }
        else
        {
            PlayerPrefs.SetFloat("Grafik", 0);
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Grafik"));
        }
    }

    public void Dusuk_Grafik()
    {
        Kalite_Ýndex = 1;
        PlayerPrefs.SetInt("Grafik", Kalite_Ýndex);
        Debug.Log(PlayerPrefs.GetInt("Grafik"));
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Grafik"));
    }

    public void Ultra_Grafik()
    {
        Kalite_Ýndex = 6;
        PlayerPrefs.SetInt("Grafik", Kalite_Ýndex);
        Debug.Log(PlayerPrefs.GetInt("Grafik"));
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Grafik"));
    }

}
