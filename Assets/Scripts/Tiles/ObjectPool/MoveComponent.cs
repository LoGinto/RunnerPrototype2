using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float objectDistance = -40f;
    [SerializeField] float despawnDistance = -110f;
    bool canSpawnGround = true;

    private void Update()
    {
        transform.position += -transform.forward * speed * Time.deltaTime;
        if(transform.position.z <= objectDistance && transform.tag == "Ground"&&canSpawnGround)
        {
            ObjectSpawner.instance.SpawnGround();
            canSpawnGround = false;
        }
        if (transform.position.z <= despawnDistance)
        {
            canSpawnGround = true;
            gameObject.SetActive(false);
        }
    }
}
