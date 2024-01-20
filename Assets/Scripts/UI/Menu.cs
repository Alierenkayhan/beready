using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerPrefs.DeleteKey("DroppedObjectNames");
    }
    public void startGame()
    {
        SceneManager.LoadScene("Level 0");
    }
}
