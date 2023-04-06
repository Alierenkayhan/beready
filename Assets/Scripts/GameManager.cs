using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] levels;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "Lvl 1")
        {
            SceneManager.LoadScene("Level 1", LoadSceneMode.Additive);
        }
    }
}
