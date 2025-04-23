using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    _instance = obj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    [SerializeField] List<GameObject> spawnPoints;
    [SerializeField] GameObject playerPrefab;
    private int currentSpawnPointIndex = 0;
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer()
    {
        GameObject spawnPoint = spawnPoints[currentSpawnPointIndex];
        player = Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    public void GetCheckPoint(int checkPointIndex)
    {
        currentSpawnPointIndex = checkPointIndex;
    }

    public void RespawnPlayer()
    {
        player.transform.position = spawnPoints[currentSpawnPointIndex].transform.position;
        player.transform.rotation = spawnPoints[currentSpawnPointIndex].transform.rotation;
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
