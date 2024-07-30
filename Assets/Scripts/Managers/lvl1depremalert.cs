using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UI;

public class lvl1depremalert : MonoBehaviour
{
    public AlertController alert;

    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Level 1")
        {
            Invoke("AlertShow", 4f);
        }
    }

    void AlertShow()
    {
        alert.alert("Deprem Uyarısı",
            "Bir süre sonra deprem başlayacak. Konumunuzu seçiminize göre belirleyin.",
            "Tamam");
    }
}
