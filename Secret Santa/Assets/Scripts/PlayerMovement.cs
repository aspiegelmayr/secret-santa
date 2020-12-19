using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerMovement : MonoBehaviour
{
    public GameObject look;
    public GameObject sword;
    [SerializeField] float cameraSpeed = 100f;
    [SerializeField] float speed = 20f;
    [SerializeField] float clampX = 85f;
    [SerializeField] float jumpForce = 1000;

    private Rigidbody playerRb;
    private Vector3 moveVec;
    private Vector3 lookVec;
    private bool isNearDoor = false;
    private string doorName = "";

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveVec * Time.deltaTime * speed);
        transform.Rotate(new Vector3(0, lookVec.y, 0) * Time.deltaTime * cameraSpeed);

        if (look.transform.localEulerAngles.x + lookVec.x < clampX || look.transform.localEulerAngles.x + lookVec.x > 360 - clampX)
            look.transform.Rotate(new Vector3(lookVec.x, 0, 0) * Time.deltaTime * cameraSpeed);
    }

    void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();

        moveVec = new Vector3(inputVec.x, 0, inputVec.y);
    }

    void OnLook(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();

        lookVec = new Vector3(-inputVec.y, inputVec.x, 0);
    }

    void OnJump(InputValue input)
    {
        playerRb.AddForce(Vector3.up * jumpForce);
    }

    void OnAttack(InputValue input)
    {
        
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.RUNNING)
            Attack();

        if (isNearDoor)
        {
            GameManager.Instance.EnterDoor(doorName);
            UIManager.Instance.SeeDoorOption(false);
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "door")
        {
            doorName = other.gameObject.name;
            isNearDoor = true;
            UIManager.Instance.SeeDoorOption(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "door")
        {
            isNearDoor = false;
            UIManager.Instance.SeeDoorOption(false);
        }
    }
}
