using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightDedectorState : MonoBehaviour
{
    // enemy nesnesinin sag taraf�nda gizlenmi� halde bulunan nesnenin kodudur. Sag taraf� denetler

    public bool _isTouchedCharacterRight = false;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Character")
        {
            _isTouchedCharacterRight = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            _isTouchedCharacterRight = false;
        }
    }

}
