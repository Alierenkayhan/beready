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

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        // PlayerPrefs'ten kaydedilen count değerini al
        count = PlayerPrefs.GetInt("MasayaniCount", 0);

        // PlayerPrefs'ten kaydedilen renk değerlerini al
        masayanıTXT.color = StringToColor(PlayerPrefs.GetString("MasayanıTXTColor", ""));
        dolapyanıTXT.color = StringToColor(PlayerPrefs.GetString("DolapyanıTXTColor", ""));
        pencereyanıTXT.color = StringToColor(PlayerPrefs.GetString("PencereyanıTXTColor", ""));
        exitreyanıTXT.color = StringToColor(PlayerPrefs.GetString("ExitreyanıTXTColor", ""));
    }

    private void Update()
    {
        if (count == 5)
        {
            SceneManager.LoadScene("Level 2");
        }
        Debug.Log(count);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(count);
        Debug.Log(collision.gameObject.tag);

        PlayerPrefs.SetInt("MasayaniCount", count);

        if (collision.gameObject.tag == "live")
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                masayanıTXT.color = Color.grey;
                PlayerPrefs.SetString("MasayanıTXTColor", ColorToString(masayanıTXT.color));
            }
            else
            {
                masayanıTXT.color = Color.grey;
                PlayerPrefs.SetString("MasayanıTXTColor", ColorToString(masayanıTXT.color));
            }
        }
        if (collision.gameObject.tag == "dolap")
        {
            dolapyanıTXT.color = Color.grey;
            PlayerPrefs.SetString("DolapyanıTXTColor", ColorToString(dolapyanıTXT.color));
        }
        if (collision.gameObject.tag == "pencere")
        {
            pencereyanıTXT.color = Color.grey;
            PlayerPrefs.SetString("PencereyanıTXTColor", ColorToString(pencereyanıTXT.color));
        }
        if (collision.gameObject.tag == "exit")
        {
            exitreyanıTXT.color = Color.grey;
            PlayerPrefs.SetString("ExitreyanıTXTColor", ColorToString(exitreyanıTXT.color));
        }

        count++;
        PlayerPrefs.Save();
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
}
