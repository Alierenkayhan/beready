using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

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
    public GameObject[] firstkiditems;
    public AlertController alert;
    // Start is called before the first frame update

    private string[] required = { "Pil", "Su", "Telsiz", "Bant", "İlaç", "Kibrit", "Fener", "Konserve", "Oyuncak" };

    private List<string> items = new List<string>();

    void Start()
    {
        CheckDroppedObjectName();
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

    void CheckDroppedObjectName()
    {
        string droppedObjectName = PlayerPrefs.GetString("DroppedObjectNames");
         
        if (!string.IsNullOrEmpty(droppedObjectName))
        {
           
            string[] droppedObjectNamesArray = droppedObjectName.Split(',');

            foreach (var item in firstkiditems)
            {
                if (!droppedObjectNamesArray.Contains(item.name))
                {
                    item.SetActive(false);
                }
                else
                {
                    item.SetActive(true);
                }
            }
        }
    }

    void Update()
    {
        string contact_person_check = PlayerPrefs.GetString("ContactPerson");
        string droppedObjectName = PlayerPrefs.GetString("DroppedObjectNames");
    
     

        string[] droppedObjectNamesArray = new string[0];

        if (!string.IsNullOrEmpty(droppedObjectName))
        {
            droppedObjectNamesArray = droppedObjectName.Split(',');
        }



        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("firstaidkit"))
                {
                    hit.collider.gameObject.SetActive(false);

                    items.Remove(hit.collider.gameObject.name);
                    text.text = "İhtiyacın olanlar:\n\n" + string.Join(", ", items);
                }
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
                Debug.Log(hasExtraItems);
                Debug.Log( hasAllRequiredItems );
                Debug.Log( contact_person_check);
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
                        alert.alert("Deprem Eğitimi", "Tebrikler deprem eğitimini başarılı bir şekilde tamamladın", "Tamam", dismissCallbackAction: n);
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
