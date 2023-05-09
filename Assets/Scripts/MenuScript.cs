using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
    public GameObject buttons;
    public GameObject slider;
    private Slider sliderElement;
    private List<GameObject> allSceneGameObjects;

    private int taggedObjects = 0;

    private void Start() {
        GameManager.earthquakeRigidbodies.Clear();
        sliderElement = slider.GetComponent<Slider>();
        StartCoroutine(LoadGameCoroutine());
    }

    private IEnumerator LoadGameCoroutine() {
        if (taggedObjects == 0) {
            allSceneGameObjects = GameManager.GetAllSceneGameObjects();
            foreach (var obj in allSceneGameObjects) {
                if (obj.CompareTag("EarthquakeRigidbody")) {
                    taggedObjects++;
                }
            }
        }
        sliderElement.maxValue = taggedObjects;
        sliderElement.value = 0;
        yield return null;

        var c = 0;
        
        foreach (var obj in allSceneGameObjects) {
            if (obj.CompareTag("EarthquakeRigidbody")) {
                var rb = obj.AddComponent<Rigidbody>();
                float volume = 1;
                if (obj.TryGetComponent(out Renderer component)) {
                    Vector3 size = component.bounds.size;
                    volume = size.x * size.y * size.z;
                }
                
                if (rb != null) {
                    rb.mass = 2.4f * volume;
                    rb.isKinematic = true;
                }
                else {
                    Debug.LogError("Failed to add Rigidbody component to object: " + obj.name);
                }
                obj.AddComponent<PhotonView>();
                obj.AddComponent<PhotonRigidbodyView>();
                if (obj.TryGetComponent(out Collider cld)) {
                    if (cld.isTrigger) {
                        var mc = obj.AddComponent<MeshCollider>();
                        mc.convex = true;
                    }
                } else {
                    var mc = obj.AddComponent<MeshCollider>();
                    mc.convex = true;
                }
                
                GameManager.earthquakeRigidbodies.Add(rb);
                sliderElement.SetValueWithoutNotify(sliderElement.value += 1);
            }

            if (++c > 10) {
                c = 0;
                yield return null;
            }
        }
        slider.SetActive(false);
        buttons.SetActive(true);
    }
}
