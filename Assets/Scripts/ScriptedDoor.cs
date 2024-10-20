﻿using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.XR;
using Random = UnityEngine.Random;

public class ScriptedDoor: MonoBehaviourPunCallbacks, IOnEventCallback {
    public GameObject Door;
    // private Vector3 DefaultRotation;
    // public Vector3 OpenRotation;
    // // private bool open = false;
    // // private bool canSwitch = true;
    // private bool switching = false;
    //     
    // private float t0 = 0f;
    // private float targetTime = 1f;
    
    public const byte DoorOpenEventCode = 1;
    public const byte DoorCloseEventCode = 2;
    
    private void Start() {
        // DefaultRotation = Door.transform.rotation.eulerAngles;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
        base.OnDisable();
    }

    // private void OnTriggerStay(Collider other) {
    //     if (other.CompareTag("EarthquakeRigidbody")) return;
    //     if (Input.GetKeyUp(KeyCode.B) || (Input.GetButtonUp("OculusBButton") && XRSettings.loadedDeviceName == "Oculus")) 
    //     {
    //         if (canSwitch) {
    //             object[] content = new object[] { transform.position }; // Array contains the target position and the IDs of the selected units
    //             RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
    //             if (open) {
    //                 PhotonNetwork.RaiseEvent(DoorCloseEventCode, content, raiseEventOptions, SendOptions.SendReliable);
    //             } else {
    //                 PhotonNetwork.RaiseEvent(DoorOpenEventCode, content, raiseEventOptions, SendOptions.SendReliable);
    //             }
    //         }
    //     }
    // }
    
    private void Update() {
        // if (switching) {
        //     if (!open) {
        //         canSwitch = false;
        //         if (t0 < 1f) {
        //             t0 += Time.deltaTime;
        //             Door.transform.rotation = Quaternion.Lerp(Quaternion.Euler(DefaultRotation.x, DefaultRotation.y, DefaultRotation.z), Quaternion.Euler(OpenRotation.x, OpenRotation.y, OpenRotation.z), t0 / targetTime);
        //         } else {
        //             canSwitch = true;
        //             switching = false;
        //             t0 = 0;
        //             open = !open;
        //         }
        //     } else {
        //         canSwitch = false;
        //         if (t0 < 1f) {
        //             t0 += Time.deltaTime;
        //             Door.transform.rotation = Quaternion.Lerp(Quaternion.Euler(OpenRotation.x, OpenRotation.y, OpenRotation.z), Quaternion.Euler(DefaultRotation.x, DefaultRotation.y, DefaultRotation.z), t0 / targetTime);
        //         } else {
        //             canSwitch = true;
        //             switching = false;
        //             t0 = 0;
        //             open = !open;
        //         }
        //     }
        // }
    }
    
    // private void OpenDoor() {
    //     open = false;
    //     switching = true;
    // }
    //
    // private void CloseDoor() {
    //     open = true;
    //     switching = true;
    // }

    public void OnEvent(EventData photonEvent) {
        if (photonEvent.Code is DoorCloseEventCode or DoorOpenEventCode) {
            // object[] data = (object[])photonEvent.CustomData;
            // Vector3 objID = (Vector3)data[0];
            // if (Vector3.Distance(objID, transform.position) < 1) {
            //     if (photonEvent.Code == DoorOpenEventCode) {
            //         OpenDoor();
            //     } else if (photonEvent.Code == DoorCloseEventCode) {
            //         CloseDoor();
            //     }
            // }
        }
    }
}