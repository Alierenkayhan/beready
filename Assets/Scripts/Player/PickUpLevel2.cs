using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUpLevel2 : MonoBehaviour
{
    private bool isDragging = false;
    private Rigidbody rb;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private string[] droppedObjectNames;
    private Transform parent;

    private needUI needs;
    
    

    private void Start()
    {
        needs = GameObject.Find("Managers").transform.GetChild(3).GetComponent<needUI>();
        print($"Added needUI object {needs.name}");
        rb = GetComponent<Rigidbody>();
        parent = transform.parent;
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // CheckAndSetActive();
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
            needs.items.Remove(gameObject.name);
            gameObject.transform.SetParent(parent);
            print($"{gameObject.name} added, {string.Join(", ", needs.items)} remaining");
            gameObject.SetActive(false);
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
