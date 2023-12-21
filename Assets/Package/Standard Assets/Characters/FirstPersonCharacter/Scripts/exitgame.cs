using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class exitgame : MonoBehaviour
{
    [SerializeField] GameObject feedbackscreen;
    [SerializeField] GameObject exitscreen;
    [SerializeField] GameObject cam;
 
    private void Start()
    {
        if(exitscreen != null) exitscreen.SetActive(false);
        Time.timeScale = 1f;
        // Cursor.visible = true;
        // Cursor.lockState = CursorLockMode.None;
        try {
            GameObject.Find("Kemal(Clone)").GetComponentInChildren<kamerashake>().enabled = true;
        } catch (NullReferenceException){}
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("EarthquakeRigidbody")) return;
        if (other.CompareTag("Player"))
        {
            if (feedbackscreen = null)
            {
                feedbackscreen = other.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            }
        }
        feedbackscreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        try {
            GameObject.Find("Kemal(Clone)").GetComponentInChildren<kamerashake>().enabled = false;
        } catch (NullReferenceException){}
    }
    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     if (exitscreen != null && exitscreen.activeSelf) {
        //         exitscreen.SetActive(false);
        //         Cursor.visible = false;
        //         Cursor.lockState = CursorLockMode.Locked;
        //     } else {
        //         exitscreen.SetActive(true);
        //         Cursor.visible = true;
        //         Cursor.lockState = CursorLockMode.None;
        //         try {
        //             GameObject.Find("Kemal(Clone)").GetComponentInChildren<kamerashake>().enabled = true;
        //         } catch (NullReferenceException){}
        //     }
        // }
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     RestartGame();
        // }
    }
    public void RestartGame()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);

    }
    public void ExitGame()
    {
        PhotonNetwork.Disconnect();
        Application.Quit();
    }
    public void exitscreenclose()
    {
        exitscreen.SetActive(false);
        // Cursor.visible = true;
        // Cursor.lockState = CursorLockMode.None;
    }
}


