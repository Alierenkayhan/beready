using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    public GameObject Door1;
    private Vector3 Door1Scale;
    private Vector3 Door1ScaleShort;
    public GameObject Door2;
    private Vector3 Door2Scale;
    private Vector3 Door2ScaleShort;
    private bool open = false;
    private bool canSwitch = true;
    private bool switching = false;
    
    private float t0 = 0f;
    private float targetTime = 1f;

    private void Start() {
        Door1Scale = Door1.transform.localScale;
        Door1ScaleShort = new Vector3(0.01f, Door1Scale.y, Door1Scale.z);
        Door2Scale = Door2.transform.localScale;
        Door2ScaleShort = new Vector3(0.01f, Door2Scale.y, Door2Scale.z);
    }

    private void OnTriggerStay(Collider other) {
        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            print("Intercepted");
            if (canSwitch) {
                print("Switching!");
                switching = true;
            }
        }
    }

    private void Update() {
        if (switching) {
            if (!open) {
                canSwitch = false;
                if (t0 < 1f) {
                    t0 += Time.deltaTime;
                    Door2.transform.localScale = Vector3.Lerp(Door2Scale, Door2ScaleShort, t0 / targetTime);
                    Door1.transform.localScale = Vector3.Lerp(Door1Scale, Door1ScaleShort, t0 / targetTime);
                } else {
                    canSwitch = true;
                    switching = false;
                    t0 = 0;
                    open = !open;
                }
            } else {
                canSwitch = false;
                if (t0 < 1f) {
                    t0 += Time.deltaTime;
                    Door2.transform.localScale = Vector3.Lerp(Door2ScaleShort, Door2Scale, t0 / targetTime);
                    Door1.transform.localScale = Vector3.Lerp(Door1ScaleShort, Door1Scale, t0 / targetTime);
                } else {
                    canSwitch = true;
                    switching = false;
                    t0 = 0;
                    open = !open;
                }
            }
        }
    }
}
