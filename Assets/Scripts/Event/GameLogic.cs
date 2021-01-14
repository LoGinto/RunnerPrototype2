using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public Animator playerAnimator;
    [SerializeField] ObjectSpawner spawner;
    public bool upward;
    public bool forward;
    public bool downward;
    public bool gameStarted = false;
    public bool doOnce = false;
    public GameObject startPlatform;
    public GameObject normalPlatform;
    public GameObject upwardPlatform;
    public List<string> startingAnim;
    private void Start()
    {
        if (upward)
        {
            normalPlatform.SetActive(false);
            upwardPlatform.SetActive(true);
        }
        else if (forward)
        {
            normalPlatform.SetActive(true);
            upwardPlatform.SetActive(false);
        }

    }
    private void Update()
    {
        if (!gameStarted)
        {
            playerAnimator.Play(startingAnim[Random.Range(0,startingAnim.Count)]);
            if (Input.anyKeyDown)
            {
                gameStarted = true;
            }
        }
        else if (!doOnce)
        {
            
            DoOnce();
        }
        if (upward)
        {
            spawner.spawnType = ObjectSpawner.SpawnType.upward;
        }
        else if (forward)
        {
            spawner.spawnType = ObjectSpawner.SpawnType.forward;
        }

    }
    void DoOnce()
    {
        if (upward)
        {

            //playerAnimator.Play("FlyUp");
            normalPlatform.SetActive(false);
            upwardPlatform.SetActive(true);  
        }
        else if(forward)
        {
            //playerAnimator.Play("RunForward");
            upwardPlatform.SetActive(false);
            normalPlatform.SetActive(true);
        }
        doOnce = true;  
    }
}
