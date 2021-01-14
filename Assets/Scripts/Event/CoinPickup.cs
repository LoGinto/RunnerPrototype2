using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public float rotationSpeed = 5f;
    private void Update()
    {
        this.transform.Rotate(Vector3.right * rotationSpeed *Time.deltaTime);   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Plane")
        {
            //maybe play sound too
            Destroy(gameObject);
        }
    }
}
