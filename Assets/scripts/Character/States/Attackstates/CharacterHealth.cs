using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CharacterHealth : MonoBehaviour
{
    // Bu kod bloðu ana karakterin canýný kontrol etmek üzere kodlandý 
     
    public Image _healthBar;

    public Animator _animCharacter;

    public CharacterMovement _characterMovementSc; 

    public GameObject _gunManagerObject,_defeatPanel,_hurtPanel,_hurtSound,_defeatSound,_gameSound; 


    public void Update()
    {
        if(_healthBar.fillAmount <= 0f) // karakterýn caný ýmage fill amountuna baðlý olarak ayarlandý
        {
            _animCharacter.SetBool("isDead", true);
            _characterMovementSc.enabled = false;
            _gunManagerObject.SetActive(false);
            Invoke("DefeatPanelController", 1f); 
        }
    }

    public void IncreaseHealth(float _healthPlus) // karakterýn canýný arttýran kod bloðu 
    {
        _healthBar.fillAmount += _healthPlus; 
    }

    public void DecreaseHealth(float _healthDown) // karakterýn canýný azaltan kod bloðu 
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

    public void DefeatPanelController() // game over panel kontrol fonksiyon bloðu
    {
        _defeatPanel.SetActive(true);
        _gameSound.SetActive(false);
        _defeatSound.SetActive(true);
    }
}
