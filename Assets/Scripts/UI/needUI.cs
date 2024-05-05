using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class needUI : MonoBehaviour
{

    public TMP_Text textEkrani;
    public GameObject textEkraniGameObject;
    public GameObject end;
    public List<string> liste;

    private int currentIndex = 0;
    
    public TMP_Text text;

    public GameObject fare;

    public GameObject itemsObject;

    public AlertController alert;
    // Start is called before the first frame update

    private string[] required = {"Pil", "Su", "Telsiz", "İlaç", "Kibrit", "Fener", "Konserve", "Oyuncak"};

    public List<string> items = new List<string>();

    void Start()
    {
        // textEkraniGameObject.SetActive(false);
        end.SetActive(false);
        text.text = "İhtiyacın olanlar:\n\n" + string.Join(", ", required);
        items.AddRange(required);
        fare.SetActive(true);
        // Invoke("Yazdir", 7f);
    }

    // void Yazdir()
    // {
    //     if (currentIndex < liste.Count)
    //     {
    //         // textEkraniGameObject.SetActive(true);
    //         string item = liste[currentIndex];
    //         // textEkrani.text = item + " ihtiyacın var.";
    //
    //         items.Remove(item);
    //
    //         if (currentIndex >= liste.Count)
    //         {
    //             currentIndex = 0;
    //         }
    //     }
    //     else
    //     {
    //         textEkraniGameObject.SetActive(false);
    //         end.SetActive(true);
    //     }
    //
    // }
    void Update()
    {
        if (IsAllItemsPicked())
        {
            var n = new UnityEvent();
            n.RemoveAllListeners();
            n.AddListener(SwapToLevel0);
            alert.alert("Deprem çantası", "Deprem çantanda eksik veya gereksiz eşyalar var. Geri dönüp gözden geçirmelisin.", "Tamam", dismissCallbackAction: n);
        }
        else
        {
            text.text = "İhtiyacın olanlar:\n\n" + string.Join(", ", items);
        }
    }

    public void SwapToLevel0()
    {
        PlayerPrefs.SetString("Revision", "true");
        PlayerPrefs.DeleteKey("DroppedObjectNames");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Level 0");
    } 

    private bool IsAllItemsPicked()
    {
        foreach (Transform tf in itemsObject.transform)
        {
            if (tf.gameObject.activeSelf)
            {
                return false;
            }
        }

        return true;
    }
}
