using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    // Declare spawn points and players
    public Transform spawnPoint1, spawnPoint2;
    public GameObject player1, player2;

    // Spawn the players at the spawn points
    private void Awake()
    {
        Instantiate(player1, spawnPoint1.position, spawnPoint1.rotation);
        Instantiate(player2, spawnPoint2.position, spawnPoint2.rotation);
    }
}
