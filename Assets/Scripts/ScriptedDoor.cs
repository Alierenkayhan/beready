using UnityEngine;

public class ScriptedDoor: MonoBehaviour {
    public GameObject Door;
    private Vector3 DefaultRotation;
    public Vector3 OpenRotation;
    private bool open = false;
    private bool canSwitch = true;
    private bool switching = false;
        
    private float t0 = 0f;
    private float targetTime = 1f;

    private void Start() {
        DefaultRotation = Door.transform.rotation.eulerAngles;
    }

    private void OnTriggerStay(Collider other) {
        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            if (canSwitch) {
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
                    Door.transform.rotation = Quaternion.Lerp(Quaternion.Euler(DefaultRotation.x, DefaultRotation.y, DefaultRotation.z), Quaternion.Euler(OpenRotation.x, OpenRotation.y, OpenRotation.z), t0 / targetTime);
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
                    Door.transform.rotation = Quaternion.Lerp(Quaternion.Euler(OpenRotation.x, OpenRotation.y, OpenRotation.z), Quaternion.Euler(DefaultRotation.x, DefaultRotation.y, DefaultRotation.z), t0 / targetTime);
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