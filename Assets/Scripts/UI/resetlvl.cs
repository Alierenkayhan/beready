using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resetlvl : MonoBehaviour
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
           
            PlayerPrefs.DeleteAll();

            // Leveli sıfırla (istersek 1 olarak ayarlayabiliriz)
            PlayerPrefs.SetInt("CurrentLevel", 1);

            PlayerPrefs.Save();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
