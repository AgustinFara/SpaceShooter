using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    [SerializeField]
    private float _shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    private float shakeAmount = 0.7f;
    private float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (_shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            _shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            _shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }

    public void Shake(float _shakeDurationget)
    {
        _shakeDuration = _shakeDurationget;
    }

    
}