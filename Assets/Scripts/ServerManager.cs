using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using WebSocketSharp;

public class ServerManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public float xvalue;
    public float yvalue;
    public float zvalue;

    public GameObject soundObject;
    public GameObject localPlayer;
    public static FirstPersonController localController;

    public string onlineRoomName = null;
    // void Start()
    // {
    //     PhotonNetwork.ConnectUsingSettings();
    // }

    public void StartOnline() {
        if (onlineRoomName.IsNullOrEmpty()) {
            onlineRoomName = null;
        }
        PhotonNetwork.ConnectUsingSettings();
    }

    public void SetOnlineName(string _name) {
        onlineRoomName = _name;
    }
    
    public void StartOffline() {
        onlineRoomName = $"BRXOfflineLobby{Random.Range(1, 10000)}";
        PhotonNetwork.ConnectUsingSettings();
    }
    
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        if (onlineRoomName.IsNullOrEmpty()) {
            onlineRoomName = "BOUNCET";
        }

        if (onlineRoomName.StartsWith("BRXOffline")) {
            PhotonNetwork.JoinOrCreateRoom(onlineRoomName, new RoomOptions {MaxPlayers=1, IsOpen=true, IsVisible=false}, TypedLobby.Default); ;
        } else {
            PhotonNetwork.JoinOrCreateRoom(onlineRoomName, new RoomOptions {MaxPlayers=0, IsOpen=true, IsVisible=true}, TypedLobby.Default); ;
        }
    }
   
    public override void OnJoinedRoom() 
    {
        base .OnJoinedRoom();
        localPlayer = PhotonNetwork.Instantiate("Kemal", new Vector3(xvalue, yvalue, zvalue), Quaternion.identity, 0, null);
        localController = localPlayer.GetComponent<FirstPersonController>();
        StartCoroutine(GameStart(onlineRoomName.StartsWith("BRXOffline") ? 7 : 20)); //7 for SP, 20 for MP
        localController.m_MouseLook.SetCursorLock(true);
        //PhotonNetwork.Instantiate("Kemal", new Vector3(35.261f, 2.633f, 6.858f), Quaternion.identity, 0, null);
    }

    IEnumerator GameStart(float time) {
        yield return new WaitForSeconds(time);
        soundObject.SetActive(true);
        GameManager.start = true;
        StartCoroutine(StartSiren(8)); //Start the siren sound after 8s
    }

    IEnumerator StartSiren(float time) {
        yield return new WaitForSeconds(time);
        soundObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
