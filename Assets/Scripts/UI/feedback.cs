using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class feedback : MonoBehaviour
{
    [SerializeField] private GameObject alert;
    [SerializeField] TextMeshProUGUI alertTitleTmp;
    [SerializeField] TextMeshProUGUI alertBodyTmp;
    [SerializeField] string title;
    [SerializeField] string bodys;

    [SerializeField] GameObject fare;
    void Start()
    {
        alert.SetActive(true);
        fare.SetActive(false);
        alertTitleTmp.text = title;
        alertBodyTmp.text = bodys; 
        Invoke("DeactivateAlert", 5f);
    }
    public void RestartTheLevel()
    {
        SceneManager.LoadScene("Level 1");
    }
    private void DeactivateAlert()
    {
        alert.SetActive(false);
        fare.SetActive(true);
        RestartTheLevel();
    }
}
