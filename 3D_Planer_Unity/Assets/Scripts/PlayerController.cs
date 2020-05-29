using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
    }

    // Update is called once per frame
    private void Update()
    {
        // Falls Bewegung verhindert wird, soll sich Player nicht bewegen
        if (!canMove) return;

        // Steuerung für Bewegung und Sicht
        KeyboardMove();
        MouseMove();
    }


    private void KeyboardMove()
    {
        if (characterController.isGrounded)
        {
            var forward = transform.TransformDirection(Vector3.forward);
            var right = transform.TransformDirection(Vector3.right);
            var curSpeedX = speed * Input.GetAxis("Vertical");
            var curSpeedY = speed * Input.GetAxis("Horizontal");
            moveDirection = forward * curSpeedX + right * curSpeedY;
        }

        // Gravitation
        moveDirection.y -= 10 * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void MouseMove()
    {
        rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
        rotation.x -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        transform.eulerAngles = new Vector2(0, rotation.y);
    }

    public void PlaceOnSecondFloor()
    {
        //@Todo in zweitem Geschoss platzieren
        Debug.Log("Player in zweitem Geschoss platzieren");
    }

    #region Variables

    public bool canMove = true;

    private CharacterController characterController;

    // Geschwindigkeit der Maus, je höher, desto stärker ändert sich die Blickrichtung bei Mausbewegung
    public float lookSpeed = 2.0f;

    // Sichtfeld des Players
    public float lookXLimit = 45.0f;
    private Vector3 moveDirection = Vector3.zero;
    public Camera playerCamera;
    private Vector2 rotation = Vector2.zero;

    // Laufgeschwindigkeit des Player
    public float speed = 7.5f;

    #endregion
}