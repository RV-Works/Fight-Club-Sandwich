using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [Header("Spawn Points")]
    public Transform[] SpawnPoints;
    public CameraController cameraController;

    public void SetSpawn(PlayerInput player)
    {
        if (SpawnPoints == null || SpawnPoints.Length == 0)
        {
            Debug.LogError(" Geen spawn points ingesteld in PlayerManager!");
            return;
        }

        if (player == null)
        {
            Debug.LogError(" PlayerInput is null in SetSpawn!");
            return;
        }

        int index = player.playerIndex;

        // add target in camera
        cameraController.AddTarget(player.transform);

        if (index >= SpawnPoints.Length)
        {
            Debug.LogWarning($" Spelerindex {index} groter dan aantal spawnpoints ({SpawnPoints.Length}), gebruik index 0.");
            index = 0;
        }

        player.transform.parent.position = SpawnPoints[index].position;
        player.transform.parent.rotation = SpawnPoints[index].rotation;


        Debug.Log($" Speler {player.playerIndex} gespawned op spawnpoint {index}");
    }
}
