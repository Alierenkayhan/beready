using UnityEngine;

public class depremetkisiniayarlama : MonoBehaviour
{
    [SerializeField] public float deprempower;
    [SerializeField] public Vector3 SceneSpawnPoint;

    private void Start() {
        // print("Startup");
        // var x = GameManager.GetAllSceneGameObjects();
        // foreach (var go in x) {
        //     if (go.name.StartsWith("Kemal")) {
        //         print("Kemal found");
        //         go.transform.position = SceneSpawnPoint;
        //     }
        // }
        // print("Positions set");
    }
}
