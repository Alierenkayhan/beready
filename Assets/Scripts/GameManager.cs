using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviourPun
{
    //[SerializeField] GameObject[] levels;
    
    public static bool start = false;
    private bool doOnce = false;

    public static List<Rigidbody> earthquakeRigidbodies = new List<Rigidbody>();
    
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (gameObject.name == "Lvl 1")
    //     {
    //         SceneManager.LoadScene("Level 1", LoadSceneMode.Additive);
    //     }
    // }

    public GameObject sound1;
    public GameObject sound2;

    private void Update() {
        if (start) {
            if (!doOnce) {
                if (PhotonNetwork.IsMasterClient) {
                    foreach (var rb in earthquakeRigidbodies) {
                        StartCoroutine(RandomTimedForce(rb, Random.Range(3f, 9f), Random.Range(1f, 5f * rb.mass)));
                    }
                }
                doOnce = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.M)) {
            var s1 = sound1.GetComponent<AudioSource>();
            var s2 = sound2.GetComponent<AudioSource>();

            s1.mute = !s1.mute;
            s2.mute = !s2.mute;
        }
    }

    IEnumerator RandomTimedForce(Rigidbody rb, float time, float forceAmount) {
        var direction = Random.insideUnitSphere;
        yield return new WaitForSeconds(time);
        rb.isKinematic = false;
        rb.AddForce(direction * forceAmount, ForceMode.VelocityChange);
        rb.AddTorque(Random.onUnitSphere * Random.Range(0f, 0.8f), ForceMode.Impulse);
    }

    #region Scene Utility
    public static List<T> GetAllSceneComponents<T>(int depthLimit = 0) {
        var all = new List<T>();
        foreach (var obj in GetAllSceneGameObjects(depthLimit, requireActive: true)) {
            if (obj.TryGetComponent(out T component)) {
                if (component != null) {
                    all.Add(component);
                }
            }
        }

        return all;
    }
	
    public static GameObject GetSceneGameObjectByName(string name, int depthLimit = 0, bool requireActive = false) {
        return GetAllSceneGameObjectsByName(name, depthLimit, requireActive)[0];
    }

    public static List<GameObject> GetAllSceneGameObjectsByName(string name, int depthLimit = 0, bool requireActive = false) {
        List<GameObject> list = new List<GameObject>();

        foreach (var obj in GetAllSceneGameObjects(depthLimit, requireActive)) {
            if (obj.name.Equals(name)) {
                list.Add(obj);
            }
        }

        if (list.Count < 1) {
            list.Add(null);
        }

        return list;
    }
	
    public static List<GameObject> GetAllSceneGameObjects(int depthLimit = 0, bool requireActive = false) {
        var rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        var all = new List<GameObject>();
        foreach (var rootObject in rootObjects) {
            all.AddRange(GetChildGameObjects(rootObject, 0, depthLimit));
        }
		
        List<GameObject> GetChildGameObjects(GameObject obj, int currentDepth, int dl) {
            var objList = new List<GameObject>();

            if ((requireActive && obj.activeSelf) || !requireActive) {
                if (dl == 0 || currentDepth < dl) {
                    for (var i = 0; i < obj.transform.childCount; i++) {
                        objList.AddRange(GetChildGameObjects(obj.transform.GetChild(i).gameObject, currentDepth + 1, dl));
                    }
                }

                objList.Add(obj);
            }

            return objList;
        }

        return all;
    }
    #endregion
}
