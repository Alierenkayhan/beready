using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class kamerashake : MonoBehaviourPun
{
    [SerializeField] private Transform _cam;
    [SerializeField] private Vector3 _startPos;

    [SerializeField] public float _shakePower;
    [SerializeField] private float _shakePowerinit;
    [SerializeField] private float _shakeDuration;
    private float _initialDuration;
    [SerializeField] private float _downAmount;
    [SerializeField] private bool _isShake = false;
    [SerializeField] private int timer;
    
    private bool doOnce = false;

    void Start()
    {
        if (!photonView.IsMine) {
            Destroy(this);
            return;
        }
    }

    private void Update() {
        if (GameManager.start) {
            if (!doOnce) {
                StartCoroutine(CameraShake());
                doOnce = true;
            }
        }
    }
    
    void LateUpdate()
    {
        _cam = transform;
        _startPos = _cam.localPosition;
        _initialDuration = _shakeDuration;

        if (_isShake)
        {
            _cam.localPosition = _startPos + Random.insideUnitSphere * _shakePowerinit;
        }
    }

    IEnumerator CameraShake()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(timer);
        _isShake = true;
        StartCoroutine(IncreaseIntensity(_shakeDuration / 2, _shakePower));
        yield return new WaitForSeconds(_shakeDuration/2);
        yield return new WaitForSeconds(_shakeDuration);
        StartCoroutine(DecreaseIntensity(_shakeDuration / 2, _shakePower));
        yield return new WaitForSeconds(_shakeDuration/2);
        _isShake = false;

    }

    IEnumerator IncreaseIntensity(float time, float upper) {
        float deltaTime = 0f;

        while (deltaTime < time) {
            deltaTime += Time.deltaTime;
            _shakePowerinit = Mathf.Lerp(0, upper, deltaTime / time);
            yield return null;
        }
        yield return null;
    }
    
    IEnumerator DecreaseIntensity(float time, float upper) {
        float deltaTime = 0f;

        while (deltaTime < time) {
            deltaTime += Time.deltaTime;
            _shakePowerinit = Mathf.Lerp(upper, 0, deltaTime / time);
            yield return null;
        }
        yield return null;
    }

       
}
