using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class Movement : MonoBehaviour
{
    [SerializeField] private float walkingSpeed = 7.5f;
    [SerializeField] private float runningSpeed = 11.5f;
    [SerializeField] private float jumpSpeed = 8.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float lookSpeed = 2.0f;
    [SerializeField] private float lookXLimit = 60.0f;
    [SerializeField] private float height = 1.8f;

    public bool isRunning = false;
    public float fatigue;
    public float stamina;
    public float refStamina = 120.0f;

    private bool canRun = true;
    private float rotationX = 0;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    public BodyPosition bodyPos = BodyPosition.Stay;
    
    [HideInInspector] public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterController.height = height;
        Cursor.lockState = CursorLockMode.Locked;
        fatigue = refStamina;
        stamina = refStamina;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        if (Input.GetKeyDown(KeyCode.C))
        {
            Crawl();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            LyingDown();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canRun && bodyPos == BodyPosition.Stay)
        {
            isRunning = true;
            StopAllCoroutines();
            StartCoroutine(ChangeStamina());
        } 
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }
        var curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        var curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        var movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            if (bodyPos == BodyPosition.Stay)
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                StandUp();
            }
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        characterController.Move(moveDirection * Time.deltaTime);
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    private IEnumerator Up(float difference)
    {
        print(difference);
        var standPos = characterController.center;
        standPos.y += difference;
        Collider[] hits;
        do
        {
            hits = Physics.OverlapCapsule(characterController.center, standPos, 0.2f);
            print(hits.Length);
            yield return new WaitForFixedUpdate();
        }
        while (hits.Length > 0);
        characterController.height = characterController.height + difference;
        bodyPos = characterController.height == height ? BodyPosition.Stay : (characterController.height == height - 0.4 ? BodyPosition.Sit : BodyPosition.Lie);
    }

    private void Crawl()
    {
        if(bodyPos == BodyPosition.Lie)
        {
            StopAllCoroutines();
            StartCoroutine(Up(0.9f));
        }
        else if (bodyPos == BodyPosition.Stay)
        {
            characterController.height -= 0.4f;
        }
    }

    private void StandUp()
    {
        if (bodyPos == BodyPosition.Lie)
        {
            StopAllCoroutines();
            StartCoroutine(Up(1.4f));
        }
        else if (bodyPos == BodyPosition.Sit)
        {
            StopAllCoroutines();
            StartCoroutine(Up(0.4f));
        }
    }

    private void LyingDown()
    {
        if (bodyPos == BodyPosition.Sit)
        {
            characterController.height -= 0.9f;
        }
        else if (bodyPos == BodyPosition.Stay)
        {
            characterController.height -= 1.4f;
        }
    }

    private IEnumerator ChangeStamina()
    {
        stamina --;
        while(stamina<fatigue)
        {
            stamina += isRunning ? -1 : 1;
            fatigue -= isRunning ? 0.1f : 0;
            canRun = stamina <= 0 ? false : true;
            if (fatigue < 0)
            {
                fatigue = 0;
            }
            if (stamina <= 0)
            {
                isRunning = false;
            }
            yield return new WaitForSeconds(1);
        }
    }
}