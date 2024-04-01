using UnityEngine;

namespace VR
{
    public class CameraShake : MonoBehaviour
    {
        [HideInInspector]
        public bool doShake;

        public bool enableShake = false;
        public bool disableShake = false;
        
        public float amplitude;
        public float frequency;
        
        [HideInInspector]
        public Vector3 initialPosition;
        
        [HideInInspector]
        public Vector3 shakePosition;
        
        [HideInInspector]
        public bool isShaking;
        private bool wasShaking;

        private void Update()
        {
            if (disableShake)
            {
                shakePosition = Vector3.zero;
                initialPosition = Vector3.zero;
                isShaking = false;
            }
            else if (doShake || enableShake)
            {
                isShaking = true;

                float shakeAmountX = Mathf.PerlinNoise(0f, Time.time * frequency) * amplitude;
                float shakeAmountY = Mathf.PerlinNoise(10f, Time.time * frequency) * 0;
                float shakeAmountZ = Mathf.PerlinNoise(20f, Time.time * frequency) * amplitude;

                shakePosition = initialPosition + new Vector3(shakeAmountX, shakeAmountY, shakeAmountZ);
            }

            if (!isShaking && wasShaking)
            {
                print("Stopped shaking");
                wasShaking = false;
            } else if (isShaking && !wasShaking)
            {
                print("Started shaking");
                wasShaking = true;
            }
        }
    }
}
