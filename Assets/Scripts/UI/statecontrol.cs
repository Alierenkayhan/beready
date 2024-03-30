using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class statecontrol : MonoBehaviour
{
    public TMP_Text masayanıTXT;
    public TMP_Text dolapyanıTXT;
    public TMP_Text pencereyanıTXT;
    public TMP_Text exitreyanıTXT;
    private int count = 0;

    private Color darkGray;
    private PlayerAnimatorController anim;

    private Collider other;

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

        // PlayerPrefs'ten kaydedilen renk değerlerini al
        // masayanıTXT.color = StringToColor(PlayerPrefs.GetString("MasayanıTXTColor", ""));
        // dolapyanıTXT.color = StringToColor(PlayerPrefs.GetString("DolapyanıTXTColor", ""));
        // pencereyanıTXT.color = StringToColor(PlayerPrefs.GetString("PencereyanıTXTColor", ""));
        // exitreyanıTXT.color = StringToColor(PlayerPrefs.GetString("ExitreyanıTXTColor", ""));

        masayanıTXT.color = PlayerPrefs.GetInt("masaCrouchDone", 0) + PlayerPrefs.GetInt("masaDone", 0) == 2 ? darkGray : Color.white;
        pencereyanıTXT.color = PlayerPrefs.GetInt("pencereDone", 0) == 1 ? darkGray : Color.white;
        dolapyanıTXT.color = PlayerPrefs.GetInt("dolapDone", 0) == 1 ? darkGray : Color.white;
        exitreyanıTXT.color = PlayerPrefs.GetInt("exitDone", 0) == 1 ? darkGray : Color.white;

        StartCoroutine(doOncePerSecond());
    }

    private void Update()
    {
        if (count == 5)
        {
            SceneManager.LoadScene("Level 2");
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

    private void OnTriggerEnter(Collider o)
    {
        // Debug.Log(collision.gameObject.tag);

        // PlayerPrefs.SetInt("MasayaniCount", count);

        other = o;
    }

    private void OnTriggerExit(Collider o)
    {
        if (other == o)
        {
            other = null;
        }
    }

    public bool EvaluateAnswer()
    {
        print($"Statecontrol answer evaluation with other object {other.gameObject.name}");
        bool x = false;
        
        if (other.gameObject.CompareTag("live"))
        {
            if (anim.IsCrouching())
            {
                // masayanıTXT.color = Color.grey;
                // PlayerPrefs.SetString("MasayanıTXTColor", ColorToString(masayanıTXT.color));
                PlayerPrefs.SetInt("masaCrouchDone", 1);
                print("Masa crouch");
                x = true;
            }
            else
            {
                // masayanıTXT.color = Color.grey;
                // PlayerPrefs.SetString("MasayanıTXTColor", ColorToString(masayanıTXT.color));
                PlayerPrefs.SetInt("masaDone", 1);
                print("Masa");
            }
        }
        if (other.gameObject.CompareTag("dolap"))
        {
            // dolapyanıTXT.color = Color.grey;
            // PlayerPrefs.SetString("DolapyanıTXTColor", ColorToString(dolapyanıTXT.color));
            PlayerPrefs.SetInt("dolapDone", 1);
            print("Dolap");
        }
        if (other.gameObject.CompareTag("pencere"))
        {
            // pencereyanıTXT.color = Color.grey;
            // PlayerPrefs.SetString("PencereyanıTXTColor", ColorToString(pencereyanıTXT.color));
            PlayerPrefs.SetInt("pencereDone", 1);
            print("Pencere");
        }
        if (other.gameObject.CompareTag("exit"))
        {
            // exitreyanıTXT.color = Color.grey;
            // PlayerPrefs.SetString("ExitreyanıTXTColor", ColorToString(exitreyanıTXT.color));
            PlayerPrefs.SetInt("exitDone", 1);
            print("exit");
        }
        
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
        return PlayerPrefs.GetInt("masaCrouchDone", 0) +
               PlayerPrefs.GetInt("masaDone", 0) +
               PlayerPrefs.GetInt("pencereDone", 0) +
               PlayerPrefs.GetInt("dolapDone", 0) +
               PlayerPrefs.GetInt("exitDone", 0);
    }
}
