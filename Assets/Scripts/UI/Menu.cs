using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    public void startGame()
    {
        SceneManager.LoadScene("Level 0");
    }
}
