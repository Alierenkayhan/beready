using System;
using System.Linq;
using UnityEngine;

namespace Rewrite.Level0
{
    public class Draggable : MonoBehaviour {
        private Rigidbody rb;
    
        public bool canDrag;

        [NonSerialized] public bool dragging;
        public bool IsChosenForBag;
        private bool WasChosenForBag;
        
        private Vector3 dragOffset = Vector3.zero;
        private Vector3 targetPosition = Vector3.zero;
        
        private Vector3 currentPos = Vector3.zero;
        private Vector3 previousPos = Vector3.zero;

        public float distanceFromCamera;

        private float lerpTime;

        private DraggableItemHandler handler;

        private void OnEnable()
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }

            handler = null;
            EndDrag();
        }

        private void OnDisable()
        {
            lerpTime = 0;
            EndDrag();
        }

        public void StartDrag(DraggableItemHandler h)
        {
            handler = h;
            distanceFromCamera = Vector3.Distance(transform.position, handler.cam.transform.position);
            WasChosenForBag = IsChosenForBag;
        }

        /// <summary>
        /// Drags the object to a given position. Use in Update.
        /// </summary>
        /// <param name="position"></param>
        public void Drag(Vector3 position)
        {
            if (!dragging)
            {
                dragging = true;
                currentPos = rb.position;
                previousPos = rb.position;
            }
            
            targetPosition = position;
            
            rb.velocity = (targetPosition - currentPos) / (Time.fixedDeltaTime * 5);
        }

        public void SetScrollPosition(Vector3 pos, float distanceFromCam)
        {
            distanceFromCamera = distanceFromCam;
            rb.position = pos;
            currentPos = pos;
            previousPos = pos;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        public void EndDrag()
        {
            EnsureHandler();
            if (!WasChosenForBag && IsChosenForBag)
            {
                MoveIntoBag();
                handler.OnUpdateBagList();
            } else if (WasChosenForBag && !IsChosenForBag){
                MoveOutOfBag();
                handler.OnUpdateBagList();
            }
            dragging = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            currentPos = rb.position;
            previousPos = rb.position;
            if (handler != null)
            {
                handler.RemoveActiveDraggable();
                handler = null;
            }
        }
        
        private void FixedUpdate() {
            if (dragging)
            {
                previousPos = currentPos;
                currentPos = rb.position;
                dragOffset = Vector3.Lerp(dragOffset, Vector3.zero, lerpTime);
                lerpTime += Time.fixedDeltaTime / 25f;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("firstaidkit"))
            {
                IsChosenForBag = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("firstaidkit"))
            {
                IsChosenForBag = false;
            }
        }

        public void MoveIntoBag()
        {
            if (PlayerPrefs.HasKey("bagItems"))
            {
                var items = PlayerPrefs.GetString("bagItems", "");
                if (items != "")
                {
                    var replacement = items.Split("|").ToList();
                    if (!replacement.Contains(name))
                    {
                        PlayerPrefs.SetString("bagItems", items + "|" + name.Trim());
                    }
                }
                else
                {
                    PlayerPrefs.SetString("bagItems", name.Trim());
                }
            }
            else
            {
                PlayerPrefs.SetString("bagItems", name.Trim());
            }
            PlayerPrefs.Save();
            print(PlayerPrefs.GetString("bagItems"));
            IsChosenForBag = true;
        }
        
        public void MoveOutOfBag()
        {
            if (PlayerPrefs.HasKey("bagItems"))
            {
                var items = PlayerPrefs.GetString("bagItems", "");
                if (items != "")
                {
                    var replacement = items.Split("|").ToList();
                    replacement.Remove(name.Trim());
                    PlayerPrefs.SetString("bagItems", string.Join("|", replacement));
                }
            }
            PlayerPrefs.Save();
            print(PlayerPrefs.GetString("bagItems"));
            IsChosenForBag = false;
        }

        private void EnsureHandler()
        {
            if (handler == null)
            {
                handler = transform.parent.GetComponent<DraggableItemHandler>();
            }
        }
    }
}