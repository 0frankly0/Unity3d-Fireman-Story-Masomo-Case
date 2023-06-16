using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SesControls : MonoBehaviour
{
    AudioSource ses_kontrols; 
    void Start()
    {
        ses_kontrols = GetComponent<AudioSource>(); // ses verici olan audio source erisildi 
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("ses") == 1) // eger kontrolcu olan ses player prefsi 1 e esitse 
        {
            ses_kontrols.mute = false; // sesi mute liyoruz
        }
        else
        {
            ses_kontrols.mute = true;
        }
    }
}
