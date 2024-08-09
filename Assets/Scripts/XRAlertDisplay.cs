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
    public GameObject personFoto;

    public void L0NextLevel()
    {
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
    }

    public void L0ContactPerson()
    {
        controller.alert("Acil Durumda Aranılacak Kişi","Adı = İlyas\nYakınlık = Baba\nTelefon = (507) 654-3210", "Tamam");
        PlayerPrefs.SetString("ContactPerson", "true");

        personFoto.SetActive(true);
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

        personFoto.SetActive(false);

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
