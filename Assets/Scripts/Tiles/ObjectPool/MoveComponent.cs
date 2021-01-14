using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    [SerializeField] public float speed = 5f;
    [SerializeField] public float objectDistance = -40f;
    [SerializeField] public float despawnDistance = -110f;   
    protected bool canSpawnGround = true;

    protected virtual void Update()
    {
        
            transform.position += -transform.forward * speed * Time.deltaTime;
            if (transform.position.z <= objectDistance && transform.tag == "Ground" && canSpawnGround)
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
       
            //transform.position += -transform.up * speed * Time.deltaTime;
            //if (transform.position.y <= objectDistance && transform.tag == "Ground" && canSpawnGround)
            //{
            //    ObjectSpawner.instance.SpawnGround();
            //    canSpawnGround = false;
            //}
            //if (transform.position.y <= despawnDistance)
            //{
            //    canSpawnGround = true;
            //    gameObject.SetActive(false);
            //}           
}
