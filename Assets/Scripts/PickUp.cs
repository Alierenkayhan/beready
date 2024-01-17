using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private bool isPickedUp = false;
    private Transform originalParent;

    private void Start()
    {
        originalParent = transform.parent;
    }

    private void Update()
    {
        if (isPickedUp && Input.GetKeyDown(KeyCode.E))
        {
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPickedUp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Pick();
            }
        }
    }

    private void Pick()
    {
        isPickedUp = true;
        transform.SetParent(Camera.main.transform); // Attach to the camera or any other object you want
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Drop()
    {
        isPickedUp = false;
        transform.SetParent(originalParent);
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
