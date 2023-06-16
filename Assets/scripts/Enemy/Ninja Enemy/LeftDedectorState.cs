using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftDedectorState : MonoBehaviour
{
    // enemy nesnesinin sol taraf�nda gizlenmi� halde bulunan nesnenin kodudur. Sol taraf� denetler

    public bool _isTouchedCharacterLeft = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            _isTouchedCharacterLeft = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            _isTouchedCharacterLeft = false;
        }
    }
}
