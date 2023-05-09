using System;
using System.Collections;
using System.Collections.Generic;
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
        exitscreen.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        try {
            GameObject.Find("Kemal(Clone)").GetComponentInChildren<kamerashake>().enabled = true;
        } catch (NullReferenceException){}
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("EarthquakeRigidbody")) return;
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (exitscreen.activeSelf) {
                exitscreen.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            } else {
                exitscreen.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                try {
                    GameObject.Find("Kemal(Clone)").GetComponentInChildren<kamerashake>().enabled = true;
                } catch (NullReferenceException){}
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
    public void RestartGame()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame()
    {
        PhotonNetwork.Disconnect();
        Application.Quit();
    }
    public void exitscreenclose()
    {
        exitscreen.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}


