using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resetCurrentLvl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            // Leveli sıfırla (istersek 1 olarak ayarlayabiliriz)
            // PlayerPrefs.SetInt("CurrentLevel", 1);
            //
            // PlayerPrefs.Save();

            ResetLevel();
        } else if (Input.GetKeyDown(KeyCode.X))
        {
            ResetLevelWithDelete();
        }
    }

    public void OnRightPrimary()
    {
        ResetLevel();
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetLevelWithDelete()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        ResetLevel();
    }
}
