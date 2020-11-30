using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GameObject look;
    public float cameraSpeed = 100f;
    public float speed = 20f;
    public float clampX = 85f;
    private Vector3 moveVec;
    private Vector3 lookVec;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveVec * Time.deltaTime * speed);
        transform.Rotate(new Vector3 (0, lookVec.y, 0) * Time.deltaTime * cameraSpeed);
        
        if (look.transform.localEulerAngles.x + lookVec.x < clampX || look.transform.localEulerAngles.x + lookVec.x > 360 - clampX)
        look.transform.Rotate(new Vector3 (lookVec.x, 0, 0) * Time.deltaTime * cameraSpeed);
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
}
