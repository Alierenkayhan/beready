using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

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

    public UnityEvent startEvent;
    void Start()
    {
        startEvent.AddListener(StartAfterAnnouncement);
    }

    public void StartOnline() {
        if (string.IsNullOrEmpty(onlineRoomName)) {
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
        if (string.IsNullOrEmpty(onlineRoomName)) {
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
        localController.m_MouseLook.SetCursorLock(false);
        localController.m_MouseLook.m_cursorIsLocked = false;
        localPlayer.transform.GetChild(0).transform.GetChild(0).transform.GetComponentInChildren<AlertController>().alert("Dikkat!", "Hoşgeldin, biraz sonra bir deprem simülasyonuna katılacaksın, eğer bu deneyim konusunda endişeliysen oyundan ayrılabilirsin. Deprem anında yaşanacaklar bu oyundakinden daha farklı olabilir, amacımız deprem anında yaşanacak olası bir senaryoyu hissettirmek. Devam etmek istiyor musun?", "Devam et", "Ayrıl", startEvent);
        //PhotonNetwork.Instantiate("Kemal", new Vector3(35.261f, 2.633f, 6.858f), Quaternion.identity, 0, null);
    }

    public void StartAfterAnnouncement() {
        localController.m_MouseLook.m_cursorIsLocked = true;
        localController.m_MouseLook.SetCursorLock(true);
        StartCoroutine(GameStart(onlineRoomName.StartsWith("BRXOffline") ? 7 : 20)); //7 for SP, 20 for MP
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
