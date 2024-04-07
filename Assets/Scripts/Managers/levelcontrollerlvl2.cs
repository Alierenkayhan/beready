using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelcontrollerlvl2 : MonoBehaviour
{
    public void Gobacktolvl0()
    {
        PlayerPrefs.DeleteKey("DroppedObjectNames");
        SceneManager.LoadScene("Level 0");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Gobacktolvl0();
        }

    }
}
