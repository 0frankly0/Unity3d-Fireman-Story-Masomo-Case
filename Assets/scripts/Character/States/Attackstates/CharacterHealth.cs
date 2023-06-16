using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CharacterHealth : MonoBehaviour
{
    // Bu kod blo�u ana karakterin can�n� kontrol etmek �zere kodland� 
     
    public Image _healthBar;

    public Animator _animCharacter;

    public CharacterMovement _characterMovementSc; 

    public GameObject _gunManagerObject,_defeatPanel,_hurtPanel,_hurtSound,_defeatSound,_gameSound; 


    public void Update()
    {
        if(_healthBar.fillAmount <= 0f) // karakter�n can� �mage fill amountuna ba�l� olarak ayarland�
        {
            _animCharacter.SetBool("isDead", true);
            _characterMovementSc.enabled = false;
            _gunManagerObject.SetActive(false);
            Invoke("DefeatPanelController", 1f); 
        }
    }

    public void IncreaseHealth(float _healthPlus) // karakter�n can�n� artt�ran kod blo�u 
    {
        _healthBar.fillAmount += _healthPlus; 
    }

    public void DecreaseHealth(float _healthDown) // karakter�n can�n� azaltan kod blo�u 
    {
        _healthBar.fillAmount -= _healthDown;
        if (_healthBar.fillAmount != 0)
        {
            StartCoroutine(HurtController());
        }
    }

    public IEnumerator HurtController()
    {
        _hurtPanel.SetActive(true);
        _hurtSound.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        _hurtPanel.SetActive(false);
        _hurtSound.SetActive(false);
    }

    public void DefeatPanelController() // game over panel kontrol fonksiyon blo�u
    {
        _defeatPanel.SetActive(true);
        _gameSound.SetActive(false);
        _defeatSound.SetActive(true);
    }
}
