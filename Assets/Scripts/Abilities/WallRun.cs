using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [SerializeField] Transform orientation;

    [Header("Wall Running")]
    [SerializeField] float wallDistance;
    [SerializeField] float minimumJumpHeight;
    [SerializeField] float wallRunGravity;
    [SerializeField] float wallRunJumpForce;
    [SerializeField] float wallRunClingForce;
    [SerializeField] float wallSpeedReq;
    [SerializeField] float wallRunBoost;
    [SerializeField] float angleThreshold;
    [SerializeField] float noGravTime;
    [SerializeField] LayerMask wallRunMask;
    float noGravCur;

    [Header("Camera")]
    [SerializeField] Camera cam;
    [SerializeField] float fov;
    [SerializeField] float wallRunFov;
    [SerializeField] float wallRunFovTime;
    [SerializeField] float camTilt;
    [SerializeField] float camTiltTime;

    public bool isWallRunning;
    public float tilt { get; private set; }
    float angledTilt => wallLeft ? -camTilt : camTilt;

    bool wallLeft = false;
    bool wallRight = false;

    RaycastHit leftHit;
    RaycastHit rightHit;

    PlayerController pc;

    Rigidbody rb;

    private RaycastHit CurrentHit => wallLeft ? leftHit : rightHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pc = GetComponent<PlayerController>();
    }

    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight) && rb.velocity.magnitude > wallSpeedReq;
    }

    void CheckWall()
    {
        if (wallLeft || wallRight)
        {
            if (Physics.Raycast(transform.position, -CurrentHit.normal, wallDistance, wallRunMask))
                return;
        }
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftHit, wallDistance, wallRunMask);
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightHit, wallDistance, wallRunMask);
    }

    private void Update()
    {
        CheckWall();
        if (CanWallRun())
        {
            if (wallLeft)
            {
                StartWallRun();
            }
            else if (wallRight)
            {
                StartWallRun();
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }
    }

    void StartWallRun()
    {
        if (!isWallRunning)
        {
            //called at the start of the wall-run
        }

        pc.ResetDoubleJump();
        rb.useGravity = false;
        isWallRunning = true;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunFov, wallRunFovTime * Time.deltaTime);
        tilt = Mathf.Lerp(tilt, angledTilt, camTiltTime * Time.deltaTime);

        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Acceleration);//downforce while wall-running
        rb.AddForce(-CurrentHit.normal * wallRunClingForce, ForceMode.Force);//small cling force to stay on the wall
        rb.AddForce(MovementToWallRun(orientation.forward).normalized * wallRunBoost, ForceMode.Acceleration);//add a bit of forward movement

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 wallRunJumpDir = transform.up + orientation.forward * 2 + CurrentHit.normal;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(wallRunJumpDir * wallRunJumpForce, ForceMode.Impulse);
        }
    }
    void StopWallRun()
    {
        rb.useGravity = true;
        isWallRunning = false;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallRunFovTime * Time.deltaTime);
        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
    }

    public Vector3 MovementToWallRun(Vector3 inDir)
    {
        //take input at direction of movement, output the direction tangent to plane if angle is less than threshold
        //else, return inDir

        float theta = 90f - Vector3.Angle(inDir, CurrentHit.normal);//angle from wall to indir

        if (Mathf.Abs(theta) > angleThreshold)
        {
            return inDir;
        }

        inDir -= Vector3.Dot(inDir, CurrentHit.normal) * CurrentHit.normal;
        return inDir;
    }
}
