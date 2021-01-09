using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameStart : MonoBehaviour
{
    public bool gameStarted;
    [SerializeField] bool doneOnce;
    [SerializeField] List<string> randomBeginningAnims = new List<string>();
    [SerializeField] string jumpBeginningAnim = "JumpOff";
    [SerializeField] GameObject mainCam;
    [SerializeField] CanvasGroup gameStartCanvasGroup;
    [SerializeField] GameObject tileManager;
    public GameObject fallcam;
    TileManager managerOfTiles;
    public GameObject startCineCamRegulator;
    [SerializeField] GameObject firstTile;
    [SerializeField] Vector3 instantiateTileAt;//3.09,-20,-0.08
    [SerializeField] Vector3 eulerOfTile = new Vector3(-76.769f, -110.449f, 114.193f);
    Animator playerAnimator;
    AnimationPlayer animationPlayer;
    GameObject firstTileInstance;
    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;
        doneOnce = false;
        managerOfTiles = tileManager.GetComponent<TileManager>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        animationPlayer = GetComponent<AnimationPlayer>();
        //cinemachineRegulator.SetActive(false);        
        firstTileInstance = Instantiate(firstTile, instantiateTileAt, Quaternion.identity);
        firstTileInstance.transform.eulerAngles = eulerOfTile;
    }
    public GameObject GetFirstTile()
    {
        return firstTileInstance;
    }
    public bool GetDoneOnceBool()
    {
        return doneOnce;
    }
    // Update is called once per frame
    void Update()
    {                           
        if (Input.anyKeyDown) {
            gameStarted = true; 
        }
        if (!gameStarted)
        {
            fallcam.SetActive(false);
            //animationPlayer.PlayTargetedAnim(playerAnimator, randomBeginningAnims[Random.Range(0, randomBeginningAnims.Count)], false,false);
            playerAnimator.Play(randomBeginningAnims[Random.Range(0, randomBeginningAnims.Count)]);
        }
        else
        {
            if (gameStartCanvasGroup.alpha > 0)
            {
                gameStartCanvasGroup.alpha -= Time.deltaTime;
            }
            if (!doneOnce)
            {
                DoOnce();
            }
        }
    }

    private void DoOnce()
    {
        for (int i = 0; i < managerOfTiles.numberOfTiles; i++)
        {
            if (i == 0)
            {
                managerOfTiles.SpawnFirstTile(firstTileInstance.transform, 0);
            }
            else
            {
                managerOfTiles.SpawnTile(firstTileInstance.transform, Random.Range(0, managerOfTiles.prefabs.Length));
            }
        }
        gameStartCanvasGroup.interactable = false;
        gameStartCanvasGroup.blocksRaycasts = false;
        //animationPlayer.PlayTargetedAnim(playerAnimator, jumpBeginningAnim, true,true);
        playerAnimator.Play(jumpBeginningAnim);
        StartCoroutine(WaitAndOpenCam());
        doneOnce = true;
    }

    IEnumerator WaitAndOpenCam()
    {
        yield return new WaitForSeconds(1f);
        startCineCamRegulator.SetActive(false);
        fallcam.SetActive(true);
    }
}
