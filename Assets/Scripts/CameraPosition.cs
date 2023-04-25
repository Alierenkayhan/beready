using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour {
    public GameObject head;
    private Vector3 offset;
    
    // Start is called before the first frame update
    void Start() {
        offset = head.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update() {
        transform.position = head.transform.position + offset;
    }
}
