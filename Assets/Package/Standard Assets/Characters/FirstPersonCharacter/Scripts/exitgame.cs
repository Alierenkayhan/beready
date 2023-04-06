using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitgame : MonoBehaviour
{
    [SerializeField] GameObject exitscreen;

    private void Start()
    {
        exitscreen.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    {
        exitscreen.SetActive(true);
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


