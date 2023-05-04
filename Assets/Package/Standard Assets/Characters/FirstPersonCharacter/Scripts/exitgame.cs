using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class exitgame : MonoBehaviour
{
    [SerializeField] GameObject feedbackscreen;
    [SerializeField] GameObject exitscreen;
    [SerializeField] GameObject camera;
 
    private void Start()
    {
        exitscreen.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.Find("Kemal(Clone)").GetComponentInChildren<kamerashake>().enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        feedbackscreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("Kemal(Clone)").GetComponentInChildren<kamerashake>().enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitscreen.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameObject.Find("Kemal(Clone)").GetComponentInChildren<kamerashake>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void exitscreenclose()
    {
        exitscreen.SetActive(false);
    }
}


