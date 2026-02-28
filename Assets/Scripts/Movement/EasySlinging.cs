using UnityEngine;
using System.Collections.Generic;

// Majority of this was based on the web slinging script for hard mode. Refer to that for references.

public class EasySlinging : MonoBehaviour
{
    Rigidbody2D rb;
    LineRenderer lr;
    DistanceJoint2D dj;
    public LayerMask grappleLayer;
    public bool isGrappling;
    Vector2 grapplePoint;

    private List<float> aimAngles = new List<float> {30f, 45f, 60f, 120f, 135f, 150f};
    private int currentAngleIndex = 0;
    public float maxGrappleDistance = 20f;
    public float aimLineLength = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        dj = GetComponent<DistanceJoint2D>();
        lr.enabled = true;
        dj.enabled = false;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f || Input.GetKeyDown(KeyCode.LeftControl))
        {
            currentAngleIndex = (currentAngleIndex + 1) % aimAngles.Count;
        }
        else if (scroll < 0f || Input.GetKeyDown(KeyCode.LeftAlt))
        {
            currentAngleIndex--;
            if (currentAngleIndex < 0)
                currentAngleIndex = aimAngles.Count - 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isGrappling || Input.GetKeyDown(KeyCode.LeftShift) && !isGrappling)
        {
            FireGrapple();
        }

        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            isGrappling = false;
            dj.enabled = false;
        }

        if (isGrappling)
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, grapplePoint);
        }
        else
        {
            // Turns the angle from the list into a vector for the upcoming raycast.

            Vector2 direction = GetDirectionFromAngle(aimAngles[currentAngleIndex]);
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, (Vector2)transform.position + direction * aimLineLength);
        }
    }

    void FireGrapple()
    {
        Vector2 direction = GetDirectionFromAngle(aimAngles[currentAngleIndex]);

        // The reason for this is because unlike the normal slinging, this needs to hit a surface, not a volume.
        // Reference: https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Physics2D.Raycast.html
        // I used it to learn the parameters mostly.

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxGrappleDistance, grappleLayer);

        if (hit.collider != null)
        {
            grapplePoint = hit.point;
            isGrappling = true;
            dj.enabled = true;
            dj.autoConfigureDistance = false;
            dj.connectedAnchor = grapplePoint;
            dj.distance = Vector2.Distance(transform.position, grapplePoint);
        }
    }

    Vector2 GetDirectionFromAngle(float angle)
    {
        float radians = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
    }
}