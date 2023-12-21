using System;
using UnityEngine;

namespace VR
{
    public class VRElevationTracker : MonoBehaviour
    {
        public AudioSource audioSource;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Crouch"))
            {
                // audioSource.Play();
                print("Trigger set by crouch event");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Crouch"))
            {
                // audioSource.Stop();
                print("Stopped crouching");
            }
            
            
        }
    }
}
