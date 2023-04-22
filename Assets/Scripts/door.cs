using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    Animation doors;

    private void Start()
    {
        doors = this.GetComponent<Animation>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            doors.enabled = !doors.enabled;
        }
    }
}
