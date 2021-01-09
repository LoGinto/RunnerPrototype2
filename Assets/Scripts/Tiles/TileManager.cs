using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] prefabs;
    public float zSpawn = 0f;
    public float tileLength = 30f;
    public int numberOfTiles = 3;
    public float xmultiplier = -3.1f;
    public float xadditive = 3.1f;
    Transform player;
    [HideInInspector] GameStart gameStartScript;
    [HideInInspector] PlayerController playerController;    
    private List<GameObject> activeTiles = new List<GameObject>();
    // Start is called before the first frame update
    private void Awake()
    {
        gameStartScript = FindObjectOfType<GameStart>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //change logic here
        if (player.transform.position.z - 35 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(gameStartScript.GetFirstTile().transform, Random.Range(0, prefabs.Length));
            DeleteTile();
        }
    }
    private void LateUpdate()
    {
        if (gameStartScript.gameStarted && playerController.isAirborn == false)
        {
            foreach (var tile in activeTiles)
            {
                tile.transform.Translate(tile.transform.forward * playerController.speed * Time.deltaTime);
            }
        }
    }
    public void SpawnTile(Transform firstTile, int tileIndex)
    {
        //change logic here
        GameObject tileGameObject = Instantiate(prefabs[tileIndex]);
        tileGameObject.transform.position = firstTile.position;
        tileGameObject.transform.rotation = firstTile.rotation;
        tileGameObject.transform.position = new Vector3(tileGameObject.transform.position.x + xmultiplier, tileGameObject.transform.position.y, firstTile.position.z + zSpawn);
        activeTiles.Add(tileGameObject);
        zSpawn += tileLength;
        xmultiplier += xadditive;
    }
    public void SpawnFirstTile(Transform firstTile, int tileIndex)
    {
        GameObject tileGameObject = Instantiate(prefabs[tileIndex]);
        tileGameObject.transform.position = firstTile.position;
        tileGameObject.transform.rotation = firstTile.rotation;
        tileGameObject.transform.position = new Vector3(firstTile.transform.position.x + xmultiplier, tileGameObject.transform.position.y, firstTile.position.z + tileLength);
        activeTiles.Add(tileGameObject);
        xmultiplier += xadditive;
    }
    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}