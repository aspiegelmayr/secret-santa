//using System.Diagnostics;
//using UnityEditor.Build.Player;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public GameObject look;
    public GameObject sword;
    [SerializeField] float cameraSpeed = 70f;
    [SerializeField] float groundSpeed = 1f;
    [SerializeField] float airSpeed = 1f;
    [SerializeField] float clampX = 85f;
    [SerializeField] float jumpForce = 1000;

    [SerializeField] float walkFriction = 5;
    [SerializeField] float airFriction = 1;
    [SerializeField] float gravity = 1;

    private CharacterController cc;
    private Vector3 moveVec;
    private Vector3 lookVec;
    private Vector2 walkInputVec;

    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        lastPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move ();
        Rotate ();
    }

    void Move ()
    {
        Vector3 velocity = (transform.position-lastPos)/Time.deltaTime;
        
        velocity += 
            transform.forward * walkInputVec.y * (cc.isGrounded?groundSpeed:airSpeed)
            + transform.right * walkInputVec.x * (cc.isGrounded?groundSpeed:airSpeed)
            -Vector3.up*gravity;
        
        velocity /= 1+velocity.magnitude * (cc.isGrounded?walkFriction:airFriction) * Time.deltaTime;
        
        lastPos = transform.position;

        cc.Move(velocity*Time.deltaTime);
    }

    void Rotate ()
    {
        transform.Rotate(new Vector3 (0, lookVec.y, 0) * Time.deltaTime * cameraSpeed);
        
        if(look.transform.localEulerAngles.x + lookVec.x < clampX || look.transform.localEulerAngles.x + lookVec.x > 360 - clampX)
        look.transform.Rotate(new Vector3 (lookVec.x, 0, 0) * Time.deltaTime * cameraSpeed);
    }

    void OnMove(InputValue input)
    {
        walkInputVec = input.Get<Vector2>();
    }

    void OnLook(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();

        lookVec = new Vector3(-inputVec.y, inputVec.x, 0);
    }

    void OnJump(InputValue input)
    {
        if(cc.isGrounded){
            cc.Move(Vector3.up*jumpForce);
        }
    }

    void OnAttack(InputValue input)
    {
        Attack();
    }   

    void OnMenu()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.PREGAME)
        {
            return;
        }
        else
        {
            GameManager.Instance.TogglePause();
        }
    }

    void Attack()
    {
        int randomAttack = Random.Range(0, 2);

        switch (randomAttack)
        {
            case 0:
                sword.GetComponent<Animator>().Play("HorizontalSwing");
                return;
            case 1:
                sword.GetComponent<Animator>().Play("VerticalSwing");
                return;
            default:
                return;
        }
    }
}
