using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoor : MonoBehaviour {

    public GameObject Door1;
    public GameObject Door2;
    private bool open = false;
    private bool canSwitch = true;
    private bool switching = false;
    
    private float t0 = 0f;
    private float targetTime = 1f;
    
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
            if (open) {
                canSwitch = false;
                if (t0 < 1f) {
                    t0 += Time.deltaTime;
                    Door2.transform.rotation = Quaternion.Lerp(Quaternion.Euler(-90, 180, 0), Quaternion.Euler(-90, 180, 90), t0 / targetTime);
                    Door1.transform.rotation = Quaternion.Lerp(Quaternion.Euler(-90, 180, 0), Quaternion.Euler(-90, 180, -90), t0 / targetTime);
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
                    Door2.transform.rotation = Quaternion.Lerp(Quaternion.Euler(-90, 180, 90), Quaternion.Euler(-90, 180, 0), t0 / targetTime);
                    Door1.transform.rotation = Quaternion.Lerp(Quaternion.Euler(-90, 180, -90), Quaternion.Euler(-90, 180, 0), t0 / targetTime);
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
