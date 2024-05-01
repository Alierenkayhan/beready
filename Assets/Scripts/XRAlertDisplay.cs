using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class XRAlertDisplay : MonoBehaviour
{
    public AlertController controller;
    public resetPickedItems p;
    public UnityEvent itemResetEvent;
    public statecontrol Statecontrol;

    public void L0NextLevel()
    {
        SceneManager.LoadScene("Level 1");
    }
    
    public void L0ContactPerson()
    {
        controller.alert("İletişim", "Tanıdıkların arasında ortak bir iletişim kişisi belirleyeceksin. Acil durum anında, bu kişi herkesin durumundan haberdar olacak ve gereken koordinasyonu sağlayacak.", "Tamam");
    }
    
    public void L0ResetItems()
    {
        if (itemResetEvent == null)
        {
            itemResetEvent = new UnityEvent();
        }
        itemResetEvent.RemoveAllListeners();
        itemResetEvent.AddListener(p.Reset);
        controller.alert("Sıfırla", "Eşyalar sıfırlanacak. Kabul ediyor musunuz?", "Sıfırla", "İptal", itemResetEvent);
    }

    public void L1SelectMasa()
    {
        Statecontrol.otherBinding = "masa";
    }
    
    public void L1SelectDolap()
    {
        Statecontrol.otherBinding = "dolap";
    }
    
    public void L1SelectPencere()
    {
        Statecontrol.otherBinding = "pencere";
    }
    
    public void L1SelectExit()
    {
        Statecontrol.otherBinding = "exit";
    }
    
    public void L1SelectNothing()
    {
        Statecontrol.otherBinding = null;
    }
    
}
