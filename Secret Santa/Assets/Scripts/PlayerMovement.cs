using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public GameObject look;
    public GameObject sword;
    public Vector3 externalForce = Vector3.zero;
    [SerializeField] float cameraSpeed = 70f;
    [SerializeField] float speed = 1f;
    [SerializeField] float clampX = 85f;
    [SerializeField] float jumpForce = 10;

    [SerializeField] float gravity = 1;
    [SerializeField] float maxSpeedChange = 4f;
    [SerializeField] GameObject grunt1;   
    [SerializeField] GameObject grunt2;
    [SerializeField] GameObject grunt3;
    [SerializeField] GameObject footstep;
    [SerializeField] GameObject swoosh1;   
    [SerializeField] GameObject swoosh2;


    private CharacterController cc;
    private Vector3 lookVec;
    private Vector2 walkInputVec = Vector3.zero;
    private Vector2 smoothWalkInputVec = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    

    private bool isNearDoor;
    private string doorName;

    

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.RUNNING)
        {
            Move();
            Rotate();
        }
    }

    void Move()
    {
        footstep.GetComponent<AudioSource>().Play();
        float MSC = maxSpeedChange * Time.deltaTime;

        smoothWalkInputVec.x += Mathf.Clamp(walkInputVec.x - smoothWalkInputVec.x, -MSC, MSC);
        smoothWalkInputVec.y += Mathf.Clamp(walkInputVec.y - smoothWalkInputVec.y, -MSC, MSC);

        smoothWalkInputVec.x = Mathf.Clamp(smoothWalkInputVec.x, -1f, 1f);
        smoothWalkInputVec.y = Mathf.Clamp(smoothWalkInputVec.y, -1f, 1f);

        Vector2 SWIVClampedMag = Vector2.ClampMagnitude(smoothWalkInputVec,1f);

        velocity = 
              transform.forward * SWIVClampedMag.y * speed
            + transform.right * SWIVClampedMag.x * speed
            + Vector3.up * (velocity.y - gravity * Time.deltaTime)
            + Vector3.right * externalForce.x
            + Vector3.forward * externalForce.z;
        
        externalForce *= 0.95f;
        
        cc.Move(velocity * Time.deltaTime);
        if(cc.isGrounded){
            velocity.y = Mathf.Max(-1f,velocity.y);
        }
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0, lookVec.y, 0) * Time.deltaTime * cameraSpeed);

        if (look.transform.localEulerAngles.x + lookVec.x < clampX || look.transform.localEulerAngles.x + lookVec.x > 360 - clampX)
            look.transform.Rotate(new Vector3(lookVec.x, 0, 0) * Time.deltaTime * cameraSpeed);
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
        int rnd = Random.Range(1, 4);

        switch (rnd)
        {
            case 1:
                grunt1.GetComponent<AudioSource>().Play();
                break;
            case 2:
                grunt2.GetComponent<AudioSource>().Play();
                break;
            case 3:
                grunt3.GetComponent<AudioSource>().Play();
                break;
            default:
                break;
        }

        if (cc.isGrounded)
        {   
            velocity.y = jumpForce;
        }
    }

    void OnAttack(InputValue input)
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.RUNNING)
        {
            if (isNearDoor)
            {
                GameManager.Instance.EnterDoor(doorName);
                UIManager.Instance.SeeDoorOption(false);
                return;
            }

            Attack();
        }
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
        int rnd = Random.Range(0, 2);

        switch (randomAttack)
        {
            case 0:
                sword.GetComponent<Animator>().Play("HorizontalSwing");
                break;
            case 1:
                sword.GetComponent<Animator>().Play("VerticalSwing");
                break;
            default:
                break;
        }

        switch (rnd)
        {
            case 0:
                swoosh1.GetComponent<AudioSource>().Play();
                break;
            case 1:
                swoosh2.GetComponent<AudioSource>().Play();
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "door")
        {
            isNearDoor = true;
            UIManager.Instance.SeeDoorOption(true);
            doorName = other.name;
        }

        if (other.tag == "collectable")
        {
            other.gameObject.SetActive(false);
            if (other.name == "KizunePlüsch" && !GameManager.Instance.hasHelmet)
            {
                UIManager.Instance.KizuneTalk(1);
            }

            if (other.name == "Helmet" && !GameManager.Instance.hasHelmet)
            {
                GameManager.Instance.hasHelmet = true;
                UIManager.Instance.KizuneTalk(2);
            }
        }
        if (other.tag == "light" && !GameManager.Instance.hasHelmet)
        {
            UIManager.Instance.StartDeath();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "door")
        {
            isNearDoor = false;
            UIManager.Instance.SeeDoorOption(false);
            UIManager.Instance.SetNeed(false);
        }
        if (other.tag == "light")
        {
            UIManager.Instance.StopDeath();
        }
    }

}
