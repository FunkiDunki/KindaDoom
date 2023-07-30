using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public static CameraController camController;

    public float xSens;
    public float ySens;

    [SerializeField] Transform cam;
    [SerializeField] WallRun wr;

    bool locked;

    float mouseX;
    float mouseY;

    float multiplier = 0.1f;
    float extmult = 1.0f;

    float xRotation;
    float yRotation;

    public Transform orientation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        camController = this;
    }

    private void Update()
    {
        GetInputs();
        cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, wr.tilt);
        orientation.transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }

    void GetInputs()
    {
        mouseX = locked ? 0.0f : Input.GetAxisRaw("Mouse X");
        mouseY = locked ? 0.0f : Input.GetAxisRaw("Mouse Y");

        xRotation -= mouseY * multiplier * ySens * extmult;
        yRotation += mouseX * multiplier * xSens * extmult;

        xRotation = Mathf.Clamp(xRotation, -85f, 85f);
    }

    public void LockRotation(bool mode)
    {
        locked = mode;
    }

    public void SetSensMult(float mult)
    {
        extmult = mult;
    }
}
