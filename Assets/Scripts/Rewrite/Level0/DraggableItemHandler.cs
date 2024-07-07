using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Rewrite.Level0
{
    public class DraggableItemHandler : MonoBehaviour
    {
        private Draggable activeDraggable = null;

        [NonSerialized] public Camera cam;
        public BagContentsOnTV tvItems;

        private void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            //Catch object
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    var obj = hit.collider.GetComponent<Draggable>();
                    if (obj != null && obj.canDrag)
                    {
                        if (Vector3.Distance(obj.transform.position, cam.transform.position) < 10)
                        {
                            activeDraggable = obj;
                            activeDraggable.StartDrag(this);
                        }
                    }
                }
            }

            //Release object
            if (Input.GetMouseButtonUp(0))
            {
                if (activeDraggable)
                {
                    activeDraggable.EndDrag();
                    activeDraggable = null;
                }
            }

            //Object controls
            if (activeDraggable)
            {
                var scrollDelta = Input.mouseScrollDelta.y;
                
                var x1 = activeDraggable.transform.position;
                var x2 = cam.transform.position;

                float d = 0.2f;
                
                //Bring object closer
                if (scrollDelta < 0){
                    if (activeDraggable.distanceFromCamera > d)
                    {
                        var dist = activeDraggable.distanceFromCamera - d;
                        Ray r = cam.ScreenPointToRay(Input.mousePosition);
                
                        activeDraggable.SetScrollPosition(r.GetPoint(dist), dist);
                    }
                }
                //Push object away
                else if (scrollDelta > 0)
                {
                    var dist = activeDraggable.distanceFromCamera + d;
                    Ray r = cam.ScreenPointToRay(Input.mousePosition);
            
                    activeDraggable.SetScrollPosition(r.GetPoint(dist), dist);
                }
                else
                {
                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                
                    activeDraggable.Drag(ray.GetPoint(activeDraggable.distanceFromCamera));
                }
            }
        }

        public void RemoveActiveDraggable()
        {
            activeDraggable = null;
        }

        public void OnUpdateBagList()
        {
            List<string> dl = new List<string>();
            for (var i = 0; i < transform.childCount; i++)
            {
                var item = transform.GetChild(i).gameObject;
                var d = item.GetComponent<Draggable>();
                if (d != null && d.IsChosenForBag)
                {
                    dl.Add(d.name);
                }
            }
            tvItems.displayItems(dl);
        }
    }
}