using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    //[SerializeField] GameObject[] levels;

    public static bool start = false;
    public static bool shake = false;
    private bool doOnce = false;
    private bool resetOnce = false;
    public static FirstPersonController localPlayer;

    public static List<Rigidbody> earthquakeRigidbodies = new List<Rigidbody>();
    public static List<Vector3> earthquakeRigidbodiesPosition = new List<Vector3>();
    public static List<Quaternion> earthquakeRigidbodiesRotation = new List<Quaternion>();
    public bool skipRBProcessing;

    public static int activeLevel = 0;

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (gameObject.name == "Lvl 1")
    //     {
    //         SceneManager.LoadScene("Level 1", LoadSceneMode.Additive);
    //     }
    // }

    public void ReloadSceneRigidbodies()
    {
        earthquakeRigidbodies.Clear();
        earthquakeRigidbodiesRotation.Clear();
        earthquakeRigidbodiesPosition.Clear();
        var allSceneGameObjects = GetAllSceneGameObjects();
        foreach (var obj in allSceneGameObjects)
        {
            if (obj.CompareTag("EarthquakeRigidbody"))
            {
                var rb = obj.AddComponent<Rigidbody>();
                float volume = 1;
                if (obj.TryGetComponent(out Renderer component))
                {
                    Vector3 size = component.bounds.size;
                    volume = size.x * size.y * size.z;
                }

                if (rb != null)
                {
                    rb.mass = 2.4f * volume;
                    rb.isKinematic = true;
                }
                else
                {
                    Debug.LogError("Failed to add Rigidbody component to object: " + obj.name);
                }

                if (obj.TryGetComponent(out Collider cld))
                {
                    if (cld.isTrigger)
                    {
                        var mc = obj.AddComponent<MeshCollider>();
                        mc.convex = true;
                    }
                }
                else
                {
                    var mc = obj.AddComponent<MeshCollider>();
                    mc.convex = true;
                }

                earthquakeRigidbodies.Add(rb);
                earthquakeRigidbodiesPosition.Add(rb.position);
                earthquakeRigidbodiesRotation.Add(rb.rotation);
            }
        }
    }

    public void ResetRBStates()
    {
        for (int i = 0; i < earthquakeRigidbodies.Count; i++)
        {
            var r = earthquakeRigidbodies[i];
            r.isKinematic = true;
            r.angularVelocity = Vector3.zero;
            r.velocity = Vector3.zero;
            r.rotation = earthquakeRigidbodiesRotation[i];
            r.position = earthquakeRigidbodiesPosition[i];
        }
    }

    private void Start()
    {
        string droppedObjectName = PlayerPrefs.GetString("DroppedObjectNames");
        print(droppedObjectName);
        if (!skipRBProcessing) ReloadSceneRigidbodies();

        localPlayer = GetAllSceneGameObjectsByName("Player", 0, false)[0].GetComponent<FirstPersonController>();
    }

    private void OnEnable()
    {
        localPlayer = GetAllSceneGameObjectsByName("Player", 0, false)[0].GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        if (shake)
        {
            if (!doOnce)
            {

                doOnce = true;
                resetOnce = false;
            }
        }
        else if (!resetOnce)
        {
            doOnce = false;
            resetOnce = true;
            ResetRBStates();
            //for (int i = 1; i < 5; i++) {
            //    //GameObject.Find($"Lvl{i}label").SetActive(true);
            //}
        }
    }

    IEnumerator RandomTimedForce(Rigidbody rb, float time, float forceAmount)
    {
        var direction = Random.insideUnitSphere;
        yield return new WaitForSeconds(time);
        rb.isKinematic = false;
        rb.AddForce(direction * forceAmount, ForceMode.VelocityChange);
        rb.AddTorque(Random.onUnitSphere * Random.Range(0f, 0.8f), ForceMode.Impulse);
    }

    #region Scene Utility
    public static List<T> GetAllSceneComponents<T>(int depthLimit = 0)
    {
        var all = new List<T>();
        foreach (var obj in GetAllSceneGameObjects(depthLimit, requireActive: true))
        {
            if (obj.TryGetComponent(out T component))
            {
                if (component != null)
                {
                    all.Add(component);
                }
            }
        }

        return all;
    }

    public static GameObject GetSceneGameObjectByName(string name, int depthLimit = 0, bool requireActive = false)
    {
        return GetAllSceneGameObjectsByName(name, depthLimit, requireActive)[0];
    }

    public static List<GameObject> GetAllSceneGameObjectsByName(string name, int depthLimit = 0, bool requireActive = false)
    {
        List<GameObject> list = new List<GameObject>();

        foreach (var obj in GetAllSceneGameObjects(depthLimit, requireActive))
        {
            if (obj.name.Equals(name))
            {
                list.Add(obj);
            }
        }

        if (list.Count < 1)
        {
            list.Add(null);
        }

        return list;
    }

    public static List<GameObject> GetAllSceneGameObjects(int depthLimit = 0, bool requireActive = false)
    {
        var rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        var all = new List<GameObject>();
        foreach (var rootObject in rootObjects)
        {
            all.AddRange(GetChildGameObjects(rootObject, 0, depthLimit));
        }

        List<GameObject> GetChildGameObjects(GameObject obj, int currentDepth, int dl)
        {
            var objList = new List<GameObject>();

            if ((requireActive && obj.activeSelf) || !requireActive)
            {
                if (dl == 0 || currentDepth < dl)
                {
                    for (var i = 0; i < obj.transform.childCount; i++)
                    {
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
