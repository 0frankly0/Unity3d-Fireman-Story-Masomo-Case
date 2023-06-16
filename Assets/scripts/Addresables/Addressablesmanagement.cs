using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations; 

public class Addressablesmanagement : MonoBehaviour
{
    public GameObject _gamePanel, _starterPanel, _character, _roundManagerObject, _mainMenuSound, _gameSound;

    public void Awake()
    {
        _mainMenuSound.SetActive(true);
        _gameSound.SetActive(false);
    }

    public void SpacestartGame()
    {
        AsyncOperationHandle<GameObject> asyncOperationHandle =
            Addressables.LoadAssetAsync<GameObject>("Assets/prefabs/spacearena.prefab");

        asyncOperationHandle.Completed += AsyncOperationHandle_Completed;
    }

    public void DesertstartGame()
    {
        AsyncOperationHandle<GameObject> asyncOperationHandle =
            Addressables.LoadAssetAsync<GameObject>("Assets/prefabs/desertarena.prefab");

        asyncOperationHandle.Completed += AsyncOperationHandle_Completed;
    }

    private void AsyncOperationHandle_Completed(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
        try
        {
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                Instantiate(asyncOperationHandle.Result);
                _character.SetActive(true);
                _gamePanel.SetActive(true);
                _starterPanel.SetActive(false);
                _roundManagerObject.SetActive(true);
                _mainMenuSound.SetActive(false);
                _gameSound.SetActive(true);
            }
            else
            {
                Debug.Log("Failed to load! ");
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
}
