using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private bool spawningObject = false;
    public static ObjectSpawner instance;
    [SerializeField] float vectorz = 60f;
    [SerializeField] float vectory = 60f;
    [SerializeField] float groundSpawnDistance = 50f;
    public enum SpawnType { upward,forward}
    public SpawnType spawnType = SpawnType.forward;
    private void Awake()
    {
        instance = this;
    }
    public void SpawnGround()
    {
        if (spawnType == SpawnType.forward)
        {
            ObjectPooler.instance.SpawnFromPool("ground", new Vector3(0, 0, vectorz), Quaternion.identity);
        }
        else if(spawnType == SpawnType.upward)
        {
            ObjectPooler.instance.SpawnFromPool("upward", new Vector3(0, vectory, 0), Quaternion.identity);
        }
    }
}
