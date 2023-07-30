using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GrappleScript : Ability
{
    [Header("Grapple")]
    [SerializeField] float grappleForce;
    [SerializeField] float maxGrappleDistance;
    [SerializeField] Camera cam;
    [SerializeField] LayerMask grappleMask;

    [Header("Keybinds")]
    [SerializeField] KeyCode grappleKey;

    GameObject grappleConnection;
    [SerializeField] GameObject grappleobj;
    bool grappling;
    Rigidbody rb;
    LineRenderer lr;
    float distConnected;

    private void Start()
    {
        grappling = false;
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (grappling)
        {
            InGrapple();
        }
    }

    public override void Init(Image abilityDisplay, TextMeshProUGUI cooldownText, Image coverImage, AbilityInfo info)
    {
        base._Init(abilityDisplay, cooldownText, coverImage, info);
    }

    private void LateUpdate()
    {
        if (grappling)
        {
            InGrappleLate();
        }
    }

    public override bool UseUp()
    {
        if (grappling)
        {
            EndGrapple();
        }
        return true;
    }

    public override bool UseDown()
    {
        return (!grappling && base.curCooldown <= 0 && TryGrapple());
    }

    bool TryGrapple()
    {
        RaycastHit hit;
        if (!Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxGrappleDistance, grappleMask))
        {
            return false;
        }
        base.curCooldown = base.cooldown;
        grappleConnection = (GameObject)Instantiate(grappleobj, hit.point, Quaternion.identity);
        grappling = true;
        lr = grappleConnection.GetComponent<LineRenderer>();
        distConnected = Vector3.Distance(transform.position, hit.point);
        rb.useGravity = false;
        return true;

    }

    void InGrapple()
    {
        float dist = Vector3.Distance(transform.position, grappleConnection.transform.position) - distConnected;

        rb.AddForce((grappleConnection.transform.position - transform.position).normalized * (dist > 0 ? 1 + dist : 1) * grappleForce, ForceMode.Acceleration);

    }

    void InGrappleLate()
    {
        lr.SetPosition(1, (transform.position + Vector3.down / 2));
        lr.SetPosition(0, lr.transform.position);
    }

    void EndGrapple()
    {
        Destroy(grappleConnection);
        grappling = false;
        rb.useGravity = true;
    }
}
