using UnityEngine;

public class CameraPosition : MonoBehaviour {
    public GameObject head;
    private Vector3 offset;
    private Vector3 referenceEuler;
    
    // Start is called before the first frame update
    void Start() {
        offset = head.transform.position - transform.position;
        referenceEuler = new Vector3(0, 270, 0);
    }

    // Update is called once per frame
    void Update() {
        Vector3 deviation = transform.eulerAngles - referenceEuler;
        deviation *= Mathf.PI;
        deviation /= 180;
        transform.position = head.transform.position + new Vector3(Mathf.Cos(deviation.y), 0, -Mathf.Sin(deviation.y)) * offset.z;
    }
}
