using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadStart : MonoBehaviour
{
    [SerializeField] BoxCollider mainCollider;
    [SerializeField] BoxCollider trigger;
    [SerializeField] float waitForSeconds = 1.5f;
    public bool shouldMove = true;
    void TurnEverythingOffOnRoadStart()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        mainCollider.enabled = false;
        trigger.enabled = false;
        shouldMove = false; 
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"&&shouldMove)
        {
            StartCoroutine(WaitAndDisable());
        }
    }
    public IEnumerator WaitAndDisable()
    {
        yield return new WaitForSeconds(waitForSeconds);
        TurnEverythingOffOnRoadStart();
    }
}
