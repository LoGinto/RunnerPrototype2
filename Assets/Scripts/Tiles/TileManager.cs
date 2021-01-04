using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] prefabs;
    public float zSpawn = 0f;
    public float tileLength = 30f;
    public int numberOfTiles = 5;
    Transform player;
    private List<GameObject> activeTiles = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        for(int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
            {
                SpawnTiles(0);
            }
            else
            {
                SpawnTiles(Random.Range(0, prefabs.Length));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z - 35 > zSpawn - (numberOfTiles*tileLength))
        {
            SpawnTiles(Random.Range(0, prefabs.Length));
            DeleteTile();
        }
    }
    public void SpawnTiles(int tileIndex)
    {
        GameObject go = Instantiate(prefabs[tileIndex],transform.forward*zSpawn,transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength; 
    }
    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
