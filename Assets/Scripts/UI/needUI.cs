using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;

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

    private string[] required = { "Pil", "Su", "Telsiz", "İlaç", "Kibrit", "Fener", "Konserve", "Oyuncak", "Bant" };

    public List<string> items = new List<string>();

    void Start()
    {
        end.SetActive(false);
        text.text = "İhtiyacın olanlar:\n\n" + string.Join(", ", required);
        items.AddRange(required);
        fare.SetActive(true);
    }

    void Update()
    {
        string droppedObjectName = PlayerPrefs.GetString("DroppedObjectNames");
        string contact_person_check = PlayerPrefs.GetString("ContactPerson");
        Debug.Log("contact_person_check " + contact_person_check);

        string[] droppedObjectNamesArray = new string[0];

        if (!string.IsNullOrEmpty(droppedObjectName))
        {
            droppedObjectNamesArray = droppedObjectName.Split(',');
        }

        if (IsAllItemsPicked())
        {
            bool hasExtraItems = false;
            bool hasAllRequiredItems = true;

            foreach (string item in droppedObjectNamesArray)
            {
                if (!required.Contains(item))
                {
                    hasExtraItems = true;
                    break;
                }
            }

            foreach (string item in required)
            {
                if (!droppedObjectNamesArray.Contains(item))
                {
                    hasAllRequiredItems = false;
                    break;
                }
            }

            if (hasExtraItems || !hasAllRequiredItems)
            {
                var n = new UnityEvent();
                n.RemoveAllListeners();
                n.AddListener(SwapToLevel0);
                alert.alert("Deprem çantası", "Deprem çantanda eksik veya gereksiz eşyalar var. Geri dönüp gözden geçirmelisin.", "Tamam", dismissCallbackAction: n);
            }
            else
            {
                if (contact_person_check == "true")
                {
                    var n = new UnityEvent();
                    alert.alert("Deprem Eğitimi", "Tebrikler deprem eğitimini başarılı bir şekilde tamamladın", "Tamam", "", n);
                }
                else
                {
                    var n = new UnityEvent();
                    n.RemoveAllListeners();
                    n.AddListener(SwapToLevel0);
                    alert.alert("Deprem çantası", "Deprem çantanda depremden sonra iletişim kurman gereken kişinin bilgileri yok", "Tamam", dismissCallbackAction: n);
                }
            }
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
        PlayerPrefs.DeleteKey("ContactPerson");
        PlayerPrefs.Save();
        PlayerPrefs.SetString("SecondTime", "true");
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
