using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class statecontrol : MonoBehaviour
{
    public TMP_Text masayanıTXT;
    public TMP_Text dolapyanıTXT;
    public TMP_Text pencereyanıTXT;
    public TMP_Text exitreyanıTXT;
    public TMP_Text nothingTXT;
    private int count = 0;

    private Color darkGray;
    private PlayerAnimatorController anim;

    private Collider other = null;
    public string otherBinding = null;
    
    public Earthquake earthquake;
    private bool depremStart = false;

    public bool tookNoCover = false;
    private bool noCoverFeedbackShown = false;
    
    public AlertController alert;

    public GameManager manager;
    
    UnityEvent e = new UnityEvent();

    private void Awake()
    {
        darkGray = new Color(0.35f, 0.35f, 0.35f, 1f);
        anim = GetComponent<PlayerAnimatorController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        // PlayerPrefs'ten kaydedilen count değerini al
        count = GetChoiceCount();
        
        
        e.RemoveAllListeners();
        e.AddListener(RestartLevel);

        // PlayerPrefs'ten kaydedilen renk değerlerini al
        // masayanıTXT.color = StringToColor(PlayerPrefs.GetString("MasayanıTXTColor", ""));
        // dolapyanıTXT.color = StringToColor(PlayerPrefs.GetString("DolapyanıTXTColor", ""));
        // pencereyanıTXT.color = StringToColor(PlayerPrefs.GetString("PencereyanıTXTColor", ""));
        // exitreyanıTXT.color = StringToColor(PlayerPrefs.GetString("ExitreyanıTXTColor", ""));

        masayanıTXT.color = PlayerPrefs.GetInt("masaDone", 0) == 1 ? darkGray : Color.white;
        pencereyanıTXT.color = PlayerPrefs.GetInt("pencereDone", 0) == 1 ? darkGray : Color.white;
        dolapyanıTXT.color = PlayerPrefs.GetInt("dolapDone", 0) == 1 ? darkGray : Color.white;
        exitreyanıTXT.color = PlayerPrefs.GetInt("exitDone", 0) == 1 ? darkGray : Color.white;
        nothingTXT.color = PlayerPrefs.GetInt("nothingDone", 0) == 1 ? darkGray : Color.white;

        StartCoroutine(doOncePerSecond());
    }

    private void RestartLevel()
    {
        if (count < 5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
        }
        else
        {
            SceneManager.LoadScene("Lvl1-2");
        }
    }
    
    private void check()
    {
        var x = EvaluateAnswer();
        print($"Evaluate answer {x} is given");
        if (x == "masa")
        {
            alert.alert("Seçim",
                "Yaptığın seçim fena değildi. Uzmanlar sabitlenmiş eşyaların yanına çökülmesini ve tutunmayı öneriyor.",
                "Tamam", dismissCallbackAction: e);
        }
        else if (x == "dolap")
        {
            alert.alert("Seçim",
                "Yaptığın seçim uzmanlar tarafından önerilmiyor. Sabitlenmemiş eşyalar üzerine düşebilir ve sana zarar verebilir.",
                "Tamam", dismissCallbackAction: e);
        }
        else if (x == "pencere")
        {
            alert.alert("Seçim",
                "Yaptığın seçim uzmanlar tarafından önerilmiyor. Deprem esnasında pencerelerin kırılma ve cam parçaları ile seni yaralama riski var. Aynı zamanda sarsıntıdan aşağı düşebilirsin.",
                "Tamam", dismissCallbackAction: e);
        }
        else if (x == "exit")
        {
            alert.alert("Seçim",
                "Yaptığın seçim uzmanlar tarafından önerilmiyor. Deprem esnasında merdiven inmek veya asansöre binmek tehlikeli olabilir. Deprem durunca bina terk edilmelidir.",
                "Tamam", dismissCallbackAction: e);
        }
        else
        {
            alert.alert("Seçim",
                "Güvensiz yerde durmak uzmanlar tarafından önerilmiyor. En yakın güvenli yere çökmeli ve bir yere tutunmalısın.",
                "Tamam", dismissCallbackAction: e);
        }
        // if (control.EvaluateAnswer())
        // {
        //     feedbackTrue.SetActive(true);
        // }
        // else
        // {
        //     feedbackFalse.SetActive(true);
        // }
    }

    private void Update()
    {
        if (!depremStart && earthquake.isEarthquakeDone)
        {
            print("Masayanı script Check answers started");

            depremStart = true;           
            check();
        }
        // Debug.Log(count);
    }

    IEnumerator doOncePerSecond()
    {
        while (count < 5)
        {
            yield return new WaitForSecondsRealtime(1f);
            count = GetChoiceCount();
        }
    }

    public string EvaluateAnswer()
    {
        string x = "";

        if (otherBinding == null)
        {
            print("stay");
            x = "stay";
            
            PlayerPrefs.SetInt("nothingDone", 1);
            PlayerPrefs.Save();
            return x;
        }
        
        if (otherBinding == "live" || otherBinding == "masa")
        {
            // masayanıTXT.color = Color.grey;
            // PlayerPrefs.SetString("MasayanıTXTColor", ColorToString(masayanıTXT.color));
            PlayerPrefs.SetInt("masaDone", 1);
            print("Masa");
            x = "masa";
        }
        if (otherBinding == "dolap")
        {
            // dolapyanıTXT.color = Color.grey;
            // PlayerPrefs.SetString("DolapyanıTXTColor", ColorToString(dolapyanıTXT.color));
            PlayerPrefs.SetInt("dolapDone", 1);
            print("Dolap");
            x = "dolap";
        }
        if (otherBinding == "pencere")
        {
            // pencereyanıTXT.color = Color.grey;
            // PlayerPrefs.SetString("PencereyanıTXTColor", ColorToString(pencereyanıTXT.color));
            PlayerPrefs.SetInt("pencereDone", 1);
            print("Pencere");
            x = "pencere";
        }
        if (otherBinding == "exit")
        {
            // exitreyanıTXT.color = Color.grey;
            // PlayerPrefs.SetString("ExitreyanıTXTColor", ColorToString(exitreyanıTXT.color));
            PlayerPrefs.SetInt("exitDone", 1);
            print("exit");
            x = "exit";
        }
        
        print(otherBinding);
        
        count = GetChoiceCount();
        print(count);
        
        PlayerPrefs.Save();
        return x;
    }

    private string ColorToString(Color color)
    {
        return color.r + "," + color.g + "," + color.b + "," + color.a;
    }
    private Color StringToColor(string colorString)
    {
        string[] colorValues = colorString.Split(',');

        float r, g, b, a;
        if (colorValues.Length == 4 && float.TryParse(colorValues[0], out r) && float.TryParse(colorValues[1], out g) && float.TryParse(colorValues[2], out b) && float.TryParse(colorValues[3], out a))
        {
            return new Color(r, g, b, a);
        }

        // Hata durumunda beyaz rengi döndür
        return Color.white;
    }

    private int GetChoiceCount()
    {
        return PlayerPrefs.GetInt("masaDone", 0) +
               PlayerPrefs.GetInt("pencereDone", 0) +
               PlayerPrefs.GetInt("dolapDone", 0) +
               PlayerPrefs.GetInt("exitDone", 0) +
               PlayerPrefs.GetInt("nothingDone", 0);
    }
}
