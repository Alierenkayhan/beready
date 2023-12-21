using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VR
{
    public class CanvasController : MonoBehaviour
    {
        public GameObject obj;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(WaitForRestart(4));
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private IEnumerator WaitForRestart(int seconds)
        {
            print("Queued restart action");
            yield return new WaitForSeconds(seconds);
            print("Canvas active");
            obj.SetActive(true);
        }

        public void RestartScene()
        {
            PlayerPrefs.SetInt("doShake", 0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        public void RestartSceneWithShaking()
        {
            PlayerPrefs.SetInt("doShake", 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
