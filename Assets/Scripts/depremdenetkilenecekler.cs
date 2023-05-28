using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class depremdenetkilenecekler : MonoBehaviour
{
    [SerializeField] Rigidbody[] gameObjects;
    public int randomforce = Random.Range(0, 10);

    private void Update()
    {
        foreach (var item in gameObjects)
        {
            item.AddForce(randomforce, randomforce, randomforce);
        }
    }
}
