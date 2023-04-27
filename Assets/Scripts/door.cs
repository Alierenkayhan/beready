using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    Animator doors;
    private bool open = false;
    private bool canSwitch = true;

    private void Start()
    {
        doors = this.GetComponent<Animator>();
    }
    private void OnTriggerStay(Collider other) {
        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            if (canSwitch) {
                doors.SetTrigger(open ? "CloseDoor" : "OpenDoor");
                open = !open;
                StartCoroutine(InputIgnoreCoroutine());
            }
        }
    }

    IEnumerator InputIgnoreCoroutine() {
        canSwitch = false;
        yield return new WaitForSecondsRealtime(1.5f);
        canSwitch = true;
    }
}
