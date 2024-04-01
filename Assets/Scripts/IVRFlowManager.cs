using System;
using System.Collections;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using VR;
using Update = UnityEngine.PlayerLoop.Update;

public class IVRFlowManager : Singleton<IVRFlowManager>
{
    public bool EditorBypassCaution;
    public bool EnableShake;
    
    private bool firstStart = true;

    private AlertController playerAlert;
    private AlertController playerAlertCancellable;
    private UnityEvent promptStartEvent;
    private UnityEvent promptCancelEvent;
    private UnityEvent promptRestartScene;

    private GameObject xrOrigin;
    private GameObject worldUI;
    private GameObject xrCamera;
    private GameObject xrCameraCollisionTrigger;
    
    private CameraShake xrCameraShake;

    private void Start()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (firstStart)
        {
            //Variable init startup
            DoAssignments();
            
            //Ensure things are set offline
            worldUI.SetActive(false);
            
            //UI event init startup
            IntermediateStart();
            if (!Application.isEditor || !EditorBypassCaution)
            {
                playerAlertCancellable.alert("Dikkat!", "Hoşgeldin, biraz sonra bir deprem simülasyonuna katılacaksın. Eğer bu deneyim konusunda endişeliysen oyundan ayrılabilirsin.\n\nDeprem anında yaşanacaklar bu oyundakinden daha farklı olabilir, amacımız deprem anında yaşanacak olası bir senaryoyu hissettirmek. Devam etmek istiyor musun?", "Devam et", "Ayrıl", promptStartEvent, promptCancelEvent);
            }
            else
            {
                StartEarthquake();
            }
        }
    }

    private void IntermediateStart()
    {
        promptStartEvent = new UnityEvent();
        promptStartEvent.AddListener(StartEarthquake);
        
        promptCancelEvent = new UnityEvent();
        promptCancelEvent.AddListener(EndIvr);
        
        promptRestartScene = new UnityEvent();
        promptRestartScene.AddListener(RestartScene);
    }

    private void FixedUpdate()
    {
        //Pre-physics update
        //Set the trigger position as the actual in-world position before shaking
        if (EnableShake)
        {
            var transform1 = xrCameraShake.transform;
            var x = transform1.position;
            transform1.localPosition = Vector3.zero;
            xrCameraCollisionTrigger.transform.position = xrCamera.transform.position;
            transform1.localPosition = x;
        }
        // print($"{xrCamera.transform.position}");
        // print($"{xrCameraShake.transform.localPosition}");
        // print($"{xrCameraCollisionTrigger.transform.position}");
        // print($"{xrCameraShake.initialPosition}   {xrCamera.transform.position}   {xrCameraShake.transform.position}");
    }

    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (firstStart)
        {
            firstStart = false;
            //Mobility startup
            ImmobilizePlayer();
        }
        
        if(EnableShake)
        {
            xrCameraShake.transform.localPosition = xrCameraShake.shakePosition;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DoAssignments();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #region Helpers

    private void DoAssignments()
    {
        AssignXROrigin();
        AssignWorldUI();
        AssignCameraCollisionTrigger();
    }

    private void AssignXROrigin()
    {
        var go = GameObject.FindGameObjectWithTag("XROrigin");
        xrOrigin = go.gameObject;
        xrCamera = xrOrigin.transform.GetChild(0).GetChild(0).gameObject;
        if(EnableShake)
        {
            xrCameraShake = xrCamera.GetComponent<CameraShake>();
        }
        var xrCanvas = GameObject.FindGameObjectWithTag("XRCanvasUI").transform;
        playerAlert = xrCanvas.GetChild(0).GetComponent<AlertController>();
        playerAlertCancellable = xrCanvas.GetChild(1).GetComponent<AlertController>();
    }

    private void AssignWorldUI()
    {
        var go = GameObject.FindGameObjectWithTag("XRWorldUI");
        worldUI = go.gameObject;
    }

    private void AssignCameraCollisionTrigger()
    {
        var go = GameObject.FindGameObjectWithTag("XRCameraPosition");
        xrCameraCollisionTrigger = go.gameObject;
        
        //Inject self reference into go
        var x = xrCameraCollisionTrigger.GetComponent<CameraTrigger>();
        x.manager = Instance;
    }

    #endregion

    #region Actions

    public void ChooseTeleportDestination(TeleportingEventArgs args)
    {
        print($"interactable: {args.interactableObject}\n\ninteractor: {args.interactorObject}\n\nrequest: {args.teleportRequest}");
    }

    #endregion

    #region IVR

    private void ImmobilizePlayer()
    {
        // var tpd = xrCamera.GetComponent<TrackedPoseDriver>();
        // tpd.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
    }
    
    private void ReleasePlayer()
    {
        // var tpd = xrCamera.GetComponent<TrackedPoseDriver>();
        // tpd.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
    }

    private void StartEarthquake()
    {
        //Mobilize player
        ReleasePlayer();
        worldUI.SetActive(true);
        StartCoroutine(Earthquake());
        return;

        IEnumerator Earthquake()
        {
            if(!Application.isEditor || !EditorBypassCaution)
            {
                yield return new WaitForSeconds(10f);
            }
            else
            {
                yield return new WaitForSeconds(3f);
            }
            if(EnableShake)
            {
                xrCameraShake.doShake = true;
            }
        }
    }

    private void EndIvr()
    {
        xrCamera.GetComponent<TrackedPoseDriver>().enabled = false;
        playerAlert.alert("Son", "VR deprem simülasyonu sonlandırıldı. VR gözlüğünü çıkarabilirsin.", "Yeniden Başlat", "", promptRestartScene);
    }

    private void RestartScene()
    {
        firstStart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    #endregion

    #region ExternalBindings

    public void SetWindowButton(bool state)
    {
        worldUI.transform.GetChild(0).GetChild(2).GetChild(0).gameObject.SetActive(state);
    }

    #endregion
}
