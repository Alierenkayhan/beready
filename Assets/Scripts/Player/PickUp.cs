using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUp : MonoBehaviour
{
    private bool isDragging = false;
    private Rigidbody rb;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private string[] droppedObjectNames;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        CheckAndSetActive();
    }

    // private void OnMouseDown()
    // {
    //     isDragging = true;
    // }
    //
    // private void OnMouseUp()
    // {
    //     if (isDragging)
    //     {
    //         isDragging = false;
    //     }
    // }
    //
    // private void Update()
    // {
    //     if (isDragging)
    //     {
    //         DragObject();
    //     }
    // }
    //
    // private void DragObject()
    // {
    //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     RaycastHit hit;
    //
    //     if (Physics.Raycast(ray, out hit))
    //     {
    //         Vector3 targetPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
    //         rb.MovePosition(Vector3.Lerp(rb.position, targetPosition, Time.deltaTime * 10f));
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("firstaidkit"))
        {
            if (PlayerPrefs.HasKey("DroppedObjectNames"))
            {
                string savedNames = PlayerPrefs.GetString("DroppedObjectNames");
                droppedObjectNames = savedNames.Split(',');
            }
            else
            {
                droppedObjectNames = new string[0];
            }

            Array.Resize(ref droppedObjectNames, droppedObjectNames.Length + 1);
            droppedObjectNames[droppedObjectNames.Length - 1] = this.gameObject.name;

            PlayerPrefs.SetString("DroppedObjectNames", string.Join(",", droppedObjectNames));

            Debug.Log("Dropped Object Names: " + string.Join(",", droppedObjectNames));

            if (PlayerPrefs.HasKey("Revision") && PlayerPrefs.GetString("Revision") == "true")
            {
                var x = PlayerPrefs.GetString("RevisedObjects");
                var y = x.Split(",").ToList();
                y.Add(gameObject.name);
                PlayerPrefs.SetString("RevisedObjects", string.Join(",", y));
            }
            PlayerPrefs.Save();
            this.gameObject.SetActive(false);
        }
        else
        {
            transform.position = initialPosition;
        }
    }

    private void CheckAndSetActive()
    {
        if (PlayerPrefs.HasKey("DroppedObjectNames"))
        {
            string[] savedNames = PlayerPrefs.GetString("DroppedObjectNames").Split(',');

            foreach (string name in savedNames)
            {
                if (name == this.gameObject.name)
                {
                    this.gameObject.SetActive(false);
                    break;
                }
            }
        }
    }

    public void OnResetItem()
    {
        isDragging = false;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        gameObject.SetActive(true);
    }

}
