using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Bu kod sayesinde ate�leme an�nda kameraya shake efekti vermek ve karakteri smooth �ekilde takip etmesini ama�lad�m

    public float shakeDuration = 0.2f; // Titreme s�resi
    public float shakeMagnitude = 0.2f; // Titreme b�y�kl���

    public Transform cameraTransform, _characterTransform;
    private Vector3 initialPosition;
    private float shakeTimer = 0f;

    private void Start()
    {
        cameraTransform = transform;
        initialPosition = cameraTransform.localPosition; // Ba�lang�� pozisyonunu kameran�n mevcut pozisyonu olarak ayarla
        initialPosition.z = -10f; // Kameran�n z ekseni de�erini -10f olarak sabitle
    }

    private void Update()
    {
        this.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -10f);

        if (shakeTimer > 0f)
        {
            // Kameray� titret
            Vector2 shakeOffset = Random.insideUnitCircle * shakeMagnitude;
            Vector3 targetPosition = _characterTransform.position + new Vector3(shakeOffset.x, shakeOffset.y, initialPosition.z);
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, targetPosition, Time.deltaTime * 10f);

            shakeTimer -= Time.deltaTime;
        }
        else
        {
            // Kameray� s�f�rla ve karakteri smooth bir �ekilde takip et
            this.transform.position = Vector3.Lerp(this.gameObject.transform.position, new Vector3(_characterTransform.position.x, _characterTransform.position.y, -10f), 0.03f);
        }
    }

    public void ShakeCamera()
    {
        // Kameray� titretme i�lemini ba�lat
        shakeTimer = shakeDuration;
    }


}
