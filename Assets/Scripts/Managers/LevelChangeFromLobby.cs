using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class LevelChangeFromLobby : MonoBehaviour
{
    public int scenelevel;

    private void OnTriggerEnter(Collider other) {
        if (GameManager.activeLevel != 0) return;
        if (other.name.StartsWith("Kemal")) {
            var cam_list = GameManager.GetAllSceneComponents<kamerashake>();
            foreach (var cam in cam_list) {
                cam._shakePowerinit = (4f + scenelevel) / 10f;
                cam.ResetCameraShake();
                GameManager.shake = false;
            }

            for (int i = 1; i < 5; i++) {
                GameObject.Find($"Lvl{i}label").SetActive(false);
            }

            //other.transform.position = GetPositionFromSceneIndex(scenelevel);
            GameManager.activeLevel = scenelevel;
            var ac = other.transform.GetChild(0).transform.GetChild(0).transform.GetComponentInChildren<AlertController>();
            ac.alert("Seviye " + GameManager.activeLevel, "Birazdan deprem olacak, deprem anında en uygun yere saklanmanı ve deprem sonrası binayı en uygun şekilde terketmeni bekliyoruz.", "Tamam");
        }
        
        // if (other.name.StartsWith("Kemal")) {
        //     StartCoroutine(SceneLoader());
        // }

        // IEnumerator SceneLoader() {
        //     if (doOnce) {
        //         doOnce = false;
        //         yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + scenelevel);
        //         var scene = SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + scenelevel);
        //         yield return null;
        //         GameManager.shake = true;
        //
        //         doOnce = true;
        //     }
        // }
    }

    private Vector3 GetPositionFromSceneIndex(int index) {
        switch (index) {
            case 1: {
                return new Vector3(-12.0299997f, 2.56999993f, -6.90999985f);
            }
            case 2: {
                return new Vector3(-12.0299997f, 2.56999993f, -6.90999985f);
            }
            case 3: {
                return new Vector3(4.62533522f, 2.56999993f, 14.6244602f);
            }
            case 4: {
                return new Vector3(29.56464f, 2.56999993f, -9.009202f);
            }
            default: {
                return new Vector3(-12.0299997f, 2.56999993f, -6.90999985f);
            }
        }
    }
}
