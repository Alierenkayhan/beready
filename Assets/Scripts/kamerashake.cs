using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kamerashake : MonoBehaviour
{
    [SerializeField] private Transform _cam;
    [SerializeField] private Vector3 _startPos;

    [SerializeField] private float _shakePower;
    [SerializeField] private float _shakePowerinit;
    [SerializeField] private float _shakeDuration;
    private float _initialDuration;
    [SerializeField] private float _downAmount;
    [SerializeField] private bool _isShake = false;
    [SerializeField] private int timer;

    void Start()
    {
        
        StartCoroutine(ExampleCoroutine());
    }

    void Update()
    {
        _cam = gameObject.transform;
        _startPos = _cam.localPosition;
        _initialDuration = _shakeDuration;

        if (_isShake)
        {
            if (_shakePowerinit != _shakePower)
            {
                _cam.localPosition = _startPos + Random.insideUnitSphere * _shakePowerinit;
                _shakePowerinit += _downAmount * Time.deltaTime;
            }
            //else
            //{
            //    _isShake = false;
            //    _cam.localPosition = _startPos;
            //    _shakeDuration = _initialDuration;
            //}
        }
    }

    IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(timer);
        _isShake = true;

    }

       
}
