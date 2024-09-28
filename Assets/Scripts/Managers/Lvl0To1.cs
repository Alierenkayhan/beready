using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Lvl0To1 : MonoBehaviour
{
    public AlertController controller;
    public resetPickedItems p;
    public UnityEvent itemResetEvent;
    public GameObject personFoto;

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("NextLevel"))
        {
            personFoto.SetActive(false);
            string contact_secondTime = PlayerPrefs.GetString("SecondTime");

            if (contact_secondTime == "true")
            {
                PlayerPrefs.SetString("SecondTimes", "false");
                SceneManager.LoadScene("Level 2");
            }
            else
            {
                SceneManager.LoadScene("Level 1");
            }
        } else if (other.name == "SifirlaTrigger")
        {
            if (itemResetEvent == null)
            {
                itemResetEvent = new UnityEvent();
            }
            itemResetEvent.RemoveAllListeners();
            itemResetEvent.AddListener(p.Reset);
            controller.alert("Sıfırla", "Eşyalar sıfırlanacak. Kabul ediyor musunuz?", "Sıfırla", "İptal", itemResetEvent);
            personFoto.SetActive(false);
        } else if (other.name == "IletisimTrigger")
        {
            var x = new UnityEvent();
            x.RemoveAllListeners();
            x.AddListener(AddContactPerson);
            personFoto.SetActive(true);
            controller.alert("Acil Durumda Aranılacak Kişi", "Adı = İlyas\nYakınlık = Baba\nTelefon = (507) 654-3210", "Tamam");
            PlayerPrefs.SetString("ContactPerson", "true");
        }
    }

    public void AddContactPerson()
    {
        PlayerPrefs.SetInt("ContactPerson", 1);
        PlayerPrefs.Save();
    }
}
