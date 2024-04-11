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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NextLevel"))
        {
            SceneManager.LoadScene("Level 1");
        } else if (other.name == "SifirlaTrigger")
        {
            if (itemResetEvent == null)
            {
                itemResetEvent = new UnityEvent();
            }
            itemResetEvent.RemoveAllListeners();
            itemResetEvent.AddListener(p.Reset);
            controller.alert("Sıfırla", "Eşyalar sıfırlanacak. Kabul ediyor musunuz?", "Sıfırla", "İptal", itemResetEvent);
        } else if (other.name == "IletisimTrigger")
        {
            controller.alert("İletişim", "Tanıdıkların arasında ortak bir iletişim kişisi belirleyeceksin. Acil durum anında, bu kişi herkesin durumundan haberdar olacak ve gereken koordinasyonu sağlayacak.", "Tamam");
        }
    }
}
