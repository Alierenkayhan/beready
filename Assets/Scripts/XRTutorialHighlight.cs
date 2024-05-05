using UI;
using UnityEngine;
using UnityEngine.Events;

public class XRTutorialHighlight : MonoBehaviour
{
    public Material defaultMaterial;
    public Material highlightMaterial;

    public GameObject startButton;

    public GameObject leftThumbStick;
    public GameObject rightThumbStick;
    
    public GameObject leftTrigger;
    public GameObject rightTrigger;
    
    public GameObject leftBumper;
    public GameObject rightBumper;

    public AlertController controller;

    public GameObject cubeObject;

    private void Start()
    {
        cubeObject.SetActive(false);
        // startButton.SetActive(false);
        var n = new UnityEvent();
        n.RemoveAllListeners();
        n.AddListener(TurnAround);
        HighlightObjects(leftTrigger, rightTrigger);
        controller.alert("Tutorial", "BeReady eğitimsel deprem uygulamasına hoşgeldin. Kontrolleri kısaca tanıtacağız.\n\nTrigger butonu ile Ok tuşuna bas.", "Ok", dismissCallbackAction:n);
    }

    public void TurnAround()
    {
        var n = new UnityEvent();
        n.RemoveAllListeners();
        n.AddListener(MoveAround);
        HighlightObjects(rightThumbStick);
        controller.alert("Tutorial", "Sağ başparmak çubuğuyla vücudunu döndür.", "Ok", dismissCallbackAction:n);
    }
    
    public void MoveAround()
    {
        var n = new UnityEvent();
        n.RemoveAllListeners();
        n.AddListener(GrabItem);
        HighlightObjects(leftThumbStick);
        controller.alert("Tutorial", "Sol başparmak çubuğuyla yürü.", "Ok", dismissCallbackAction:n);
    }
    
    public void GrabItem()
    {
        HighlightObjects(rightBumper, leftBumper);
        print("enabled cube");
        cubeObject.SetActive(true);
        controller.alert("Tutorial", "Bir Bumper butonu ile bir objeyi tut.", "Ok");
    }
    
    public void MoveGrabbedItem()
    {
        var n = new UnityEvent();
        n.RemoveAllListeners();
        n.AddListener(OnPlayReady);
        HighlightObjects(rightThumbStick);
        controller.alert("Tutorial", "Sağ başparmak çubuğuyla tuttuğun objeyi ileri veya geri ittir.", "Ok", dismissCallbackAction:n);
    }

    public void OnPlayReady()
    {
        startButton.SetActive(true);
        print("Play ready");
    }

    private void HighlightObjects(GameObject gameObject1, GameObject gameObject2 = null, GameObject gameObject3 = null, GameObject gameObject4 = null)
    {
        leftThumbStick.GetComponent<MeshRenderer>().material = defaultMaterial;
        rightThumbStick.GetComponent<MeshRenderer>().material = defaultMaterial;

        leftTrigger.GetComponent<MeshRenderer>().material = defaultMaterial;
        rightTrigger.GetComponent<MeshRenderer>().material = defaultMaterial;

        leftBumper.GetComponent<MeshRenderer>().material = defaultMaterial;
        rightBumper.GetComponent<MeshRenderer>().material = defaultMaterial;
        
        gameObject1.GetComponent<MeshRenderer>().material = highlightMaterial;
        if (gameObject2 != null)
        {
            gameObject2.GetComponent<MeshRenderer>().material = highlightMaterial;
        }
        if (gameObject3 != null)
        {
            gameObject3.GetComponent<MeshRenderer>().material = highlightMaterial;
        }
        if (gameObject4 != null)
        {
            gameObject4.GetComponent<MeshRenderer>().material = highlightMaterial;
        }
    }
}
