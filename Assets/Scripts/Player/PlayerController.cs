using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float gravity;
    [SerializeField] bool isOnGround;
    [SerializeField]bool isAirborn = false;
    [SerializeField] bool jetpack;
    [SerializeField] float speed = 2f; 
    [SerializeField] float laneDistance = 4f;
    [SerializeField] GameObject runCam;
    [SerializeField] Transform groundCheck;
    [SerializeField] float interPolation;
    Vector3 dir;
    [HideInInspector] CharacterController characterController;
    [SerializeField] Collider myCollider;
    [SerializeField] float groundTo;
    Animator animator;
    Vector3 movementVector;
    float distToGround;
    GameStart gameStartScript;
    int lane = 1;
    // Start is called before the first frame update
    void Start()
    {
        runCam.SetActive(false);
        characterController = GetComponent<CharacterController>();
            //characterController.detectCollisions = false;
        distToGround = myCollider.bounds.extents.y;
        animator = GetComponent<Animator>();
        gameStartScript = FindObjectOfType<GameStart>();    
    }

    public bool CharacterIsGrounded()
    {
        return Physics.Raycast(groundCheck.position, -Vector3.up,groundTo); 
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(groundCheck.position, -Vector3.up * groundTo, Color.red);
        if (!jetpack)
        {
            movementVector.y = gravity;
            characterController.Move(movementVector * Time.deltaTime);            
        }                         
    }
    private void Update()
    {
        dir.z = speed;
        Debug.Log("Grounded method " + CharacterIsGrounded());
        Debug.Log("CC is grounded " + characterController.isGrounded);        
        
        if (!jetpack && gameStartScript.gameStarted)
        {           
            if (characterController.isGrounded == false)
            {               
                if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping Down")&& !CharacterIsGrounded())
                {
                    isAirborn = true;
                }
                else if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping Down") && CharacterIsGrounded())
                {
                    isAirborn = false; 
                }
                if (isAirborn)
                {
                    gameStartScript.fallcam.SetActive(true);
                    runCam.SetActive(false);
                }             
            }
            else if (characterController.isGrounded == true)
            {             
                if (isAirborn == true)
                {                    
                    animator.applyRootMotion = false;
                    animator.Play("Landed");                    
                    characterController.center = new Vector3(0, 1.7f, 0);
                    isAirborn = false;
                }
                
                if (!isAirborn && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping Down"))
                {
                    gameStartScript.fallcam.SetActive(false);
                    runCam.SetActive(true);
                }
            }
        }
        MoveBetweenLanes();
    }
    void MoveBetweenLanes()
    {       
        if (gameStartScript.gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                lane++;
                if (lane == 3)
                {
                    lane = 2;
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                lane--;
                if (lane == -1)
                {
                    lane = 0;
                }
            }
            Vector3 targetPos = transform.position.z * transform.forward + transform.position.y * transform.up;
            if (lane == 0)
            {
                targetPos += Vector3.left * laneDistance;
            }
            else if (lane == 2)
            {
                targetPos += Vector3.right * laneDistance;
            }
            if (transform.position == targetPos)
            {
                return;
            }
            Vector3 diff = targetPos - transform.position;
            Vector3 moveDir = diff.normalized * interPolation * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            {
                characterController.Move(moveDir);
            }
            else
            {
                characterController.Move(diff);
            }
        }
    }   
}
