using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<GameObject> activeTiles;
    public List<GameObject> prefabs;
    GameStart gameStartScript;
    [SerializeField]Camera mainCamera;
    public float tileLength = 29.86f;
    PlayerController playerController;
    public float xAdditive = 2.98f;
    public bool shouldChange = false;   
    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        gameStartScript = FindObjectOfType<GameStart>();
    }

    private void Update()
    {
        if (shouldChange)
        {
            SpawnTile();
            SpawnTile();            
            shouldChange = false;
        }
        if (gameStartScript.gameStarted)
        {
            if (mainCamera.WorldToViewportPoint(activeTiles[0].GetComponent<Platform>().endPoint.position).z < 0)
            {
                DeleteTile();
            }
        }
    }

    private void LateUpdate()
    {
        if(gameStartScript.gameStarted && playerController.isAirborn == false)
        {
            gameStartScript.GetFirstTile().transform.Translate(gameStartScript.GetFirstTile().transform.forward * playerController.speed * Time.deltaTime);
            foreach (var tile in activeTiles)
            {
                tile.transform.Translate(tile.transform.forward * playerController.speed * Time.deltaTime);
            }
        }
    }
    void SpawnTile()
    {
        GameObject tile = Instantiate(prefabs[Random.Range(0, activeTiles.Count-1)]);
        tile.transform.rotation = gameStartScript.GetFirstTile().transform.rotation;
        tile.transform.position = new Vector3(activeTiles[activeTiles.Count-1].transform.position.x+xAdditive,gameStartScript.GetFirstTile().transform.position.y, activeTiles[activeTiles.Count-1].transform.position.z + tileLength);
        
        activeTiles.Add(tile);
    }
    public void SpawnFirstTile()
    {
        GameObject tile = Instantiate(prefabs[Random.Range(0, activeTiles.Count)]);
        tile.transform.rotation = gameStartScript.GetFirstTile().transform.rotation;
        tile.transform.position = new Vector3(gameStartScript.GetFirstTile().transform.position.x + xAdditive, gameStartScript.GetFirstTile().transform.position.y, gameStartScript.GetFirstTile().transform.position.z+ tileLength);
        activeTiles.Add(tile);
    }
    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);

    }

}