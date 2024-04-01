using System;
using Unity.VisualScripting;
using UnityEngine;

namespace VR
{
    public class CameraTrigger : MonoBehaviour
    {
        public bool DolapStanding;
        public bool DolapCrouching;
        public bool MasaStanding;
        public bool MasaCrouching;
        public bool PencereNearby;
        public bool PencereJumped;
        
        [HideInInspector]
        public IVRFlowManager manager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("XRPositionTrigger"))
            {
                if (other.gameObject.name.Equals("Standing"))
                {
                    var otherName = other.transform.parent.gameObject.name;
                    if (otherName.Equals("Dolap"))
                    {
                        DolapStanding = true;
                        DolapCrouching = false;
                    } else if (otherName.Equals("Masa"))
                    {
                        MasaStanding = true;
                        MasaCrouching = false;
                    }
                } else if (other.gameObject.name.Equals("Crouching"))
                {
                    var otherName = other.transform.parent.gameObject.name;
                    if (otherName.Equals("Dolap"))
                    {
                        DolapCrouching = true;
                        DolapStanding = false;
                    } else if (otherName.Equals("Masa"))
                    {
                        MasaCrouching = true;
                        MasaStanding = false;
                    }
                }
                
                if (other.gameObject.name.Equals("Pencere"))
                {
                    if (manager != null)
                    {
                        manager.SetWindowButton(true);
                        PencereNearby = true;
                    }
                }
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("XRPositionTrigger"))
            {
                if (other.gameObject.name.Equals("Standing"))
                {
                    var otherName = other.transform.parent.gameObject.name;
                    if (otherName.Equals("Dolap"))
                    {
                        DolapStanding = false;
                    } else if (otherName.Equals("Masa"))
                    {
                        MasaStanding = false;
                    }
                } else if (other.gameObject.name.Equals("Crouching"))
                {
                    var otherName = other.transform.parent.gameObject.name;
                    if (otherName.Equals("Dolap"))
                    {
                        DolapCrouching = false;
                    } else if (otherName.Equals("Masa"))
                    {
                        MasaCrouching = false;
                    }
                }
                
                if (other.gameObject.name.Equals("Pencere"))
                {
                    if (manager != null)
                    {
                        manager.SetWindowButton(false);
                        PencereNearby = false;
                    }
                }
            }
        }
    }
}
