using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform startPoint;
    public float waitFor = 0.9f;
    public Transform endPoint;
    public GameObject[] obstacles; //Objects that contains different obstacle types which will be randomly activated

    public void ActivateRandomObstacle()
    {
        DeactivateAllObstacles();

        System.Random random = new System.Random();
        int randomNumber = random.Next(0, obstacles.Length);
        obstacles[randomNumber].SetActive(true);
    }

    public void DeactivateAllObstacles()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            // StartCoroutine(WaitAndChange());
            //if (FindObjectOfType<RoadStart>().shouldMove)
            //{
            //    StartCoroutine(FindObjectOfType<RoadStart>().WaitAndDisable());
            //}            
            FindObjectOfType<TileManager>().shouldChange = true;            
            StartCoroutine(WaitAndChange());
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        FindObjectOfType<TileManager>().shouldChange = false;
    //    }
    //}
    IEnumerator WaitAndChange()
    {
        yield return new WaitForSeconds(waitFor);
        FindObjectOfType<TileManager>().DeleteTile();
    }
}
