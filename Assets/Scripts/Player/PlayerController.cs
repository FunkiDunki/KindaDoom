using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static PlayerController main;
    [Header("Movement")]
    public float moveSpeed;
    public float movementMultiplier;
    public Transform orientation;

    [Header("Jumping")]
    public float jumpForce;
    public float playerHeight;
    public float downGravity;

    [Header("Keybinds")]
    KeyCode jumpKey = KeyCode.Space;

    [Header("Drag")]
    public float groundDrag = 6f;
    public float airDrag = 2f;
    public float airMultiplier = 0.3f;

    [Header("Ground")]
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] public bool isGrounded { get; private set; }

    [Header("Health")]
    [SerializeField] float health;
    [SerializeField] float max_health;
    [SerializeField] PlayerHealthbar healthbar;
    [SerializeField] DamageScreenEffect dmgScreenEffect;
    [SerializeField] float dmgFlashTime;

    [Header("Interaction")]
    [SerializeField] TextMeshProUGUI interactionTextbox;

    [Header("Scoring")]
    [SerializeField] int kills;
    [SerializeField] int winCondition;

    RaycastHit slopeHit;
    Vector3 slopeMoveDirection;
    WallRun wr;

    Rigidbody rb;
    float horizontalMovement;
    float verticalMovement;
    Vector3 moveDirection;
    bool dragSet;
    bool blockMovement;
    bool doubleJump;

    private void Awake()
    {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        wr = GetComponent<WallRun>();
        dragSet = false;

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position + Vector3.down * playerHeight / 2, groundDistance, groundMask);
        isGrounded = isGrounded && rb.useGravity;

        if (isGrounded)
        {
            doubleJump = true;
        }

        HandleInputs();
        ControlDrag();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }
        else if (Input.GetKeyDown(jumpKey) && doubleJump && !wr.isWallRunning)
        {
            doubleJump = false;
            Jump();
        }
        if (!Input.GetKey(jumpKey) && rb.useGravity)
        {
            //add more force on top of gravity
            rb.AddForce(Vector3.down * downGravity, ForceMode.Acceleration);
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        Interactions();

        //display health:
        healthbar.DisplayHealth(health / max_health);
    }

    public void ResetDoubleJump()
    {
        doubleJump = true;
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void ControlDrag()
    {
        if (dragSet)
        {
            return;
        }
        rb.drag = isGrounded ? groundDrag : airDrag;
    }

    public void SetDrag(float drag)
    {
        rb.drag = drag;
        dragSet = true;
    }
    public void UnsetDrag()
    {
        dragSet = false;
    }

    public void SetMoveBlock(bool to)
    {
        blockMovement = to;
    }

    void HandleInputs()
    {
        horizontalMovement = wr.isWallRunning ? 0 : Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
        if ((wr != null) && wr.isWallRunning)
        {
            moveDirection = wr.MovementToWallRun(moveDirection);//convert to tangent movement when wall running
        }
    }

    public void AddScore(int s)
    {
        kills += s;
        if (kills >= winCondition)
        {
            print("We Won!");
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (blockMovement)
        {
            return;//ignore movement if the movement is blocked
        }

        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
        }
        return false;
    }

    public void Damage(float dmg)
    {
        health -= dmg;
        dmgScreenEffect.GetHit(dmgFlashTime);
        if (health <= 0.0f)
        {
            PlayerDealth();
        }
    }

    void PlayerDealth()
    {
        print("This player has died.");
        health = max_health;
    }

    //interacting with objects:
    void Interactions()
    {
        //first clear the textbox
        interactionTextbox.text = "";

        //check if we should display an interaction
        //shoot a raycast forward:
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10.0f))
        {
            IInteractable inter = hit.transform.GetComponentInParent<IInteractable>();
            if (inter != null)
            {
                //we found an interactable object
                interactionTextbox.text = inter.GetInteractionName() + ": E";
                //check if the interact button is pressed:
                if (Input.GetButtonDown("Interact"))
                {
                    inter.Interact();
                }
            }
        }
    }

   }
