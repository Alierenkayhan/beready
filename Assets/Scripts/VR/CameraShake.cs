using System;
using System.Collections;
using UnityEngine;

namespace VR
{
    public class CameraShake : MonoBehaviour
    {
        private GameObject camera;
        private bool doShake;
        public bool shakeOverride = false;
        
        public float amplitude;
        public float frequency;

        public float prevX;
        public float prevZ;

        private void Awake()
        {
            camera = gameObject;
        }

        // Start is called before the first frame update
        void Start()
        {
            if (PlayerPrefs.GetInt("doShake") == 1)
            {
                doShake = true;
            }
        }

        private void Update()
        {
            if (doShake || shakeOverride)
            {
                float elapsedTime = Time.time;
                var position = camera.transform.localPosition;

                float lerpTime = Mathf.PingPong(elapsedTime * frequency, 1f);
                float newXPos = position.x + Mathf.Sin(lerpTime * 2 * Mathf.PI) * amplitude;
                float newZPos = position.z + Mathf.Cos(lerpTime * 2 * Mathf.PI) * amplitude;

                newXPos -= prevX;
                newZPos -= prevZ;

                position = new Vector3(newXPos, position.y, newZPos);
                prevX = newXPos;
                prevZ = newZPos;
                camera.transform.localPosition = position;
            }
            else
            {
                camera.transform.localPosition = Vector3.zero;
            }
        }
    }
}
