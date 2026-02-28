using UnityEngine;

// Inspirations
// 60 Seconds Tutorial | Grappling hook | Unity 2D by 1 Minute Unity
// https://youtu.be/P-UscoFwaE4?si=rTahcieaYjodgE-U
// I created the PERFECT grappling hook... and you can too! UNITY 2D by 1 Minute Unity
// https://youtu.be/dnNCVcVS6uw?si=0FHDJceZ4VggDCby
// How To Make Any Game Mechanic - Episode 7 - 2D Grappling Hook by ThatOneUnityDev
// https://youtu.be/Gx46xUgVXrQ?si=_DL_JZDcmDl-ELki
// Swing and Climb On Ropes In Unity 2D by Glimpsy Games
// https://youtu.be/oW_Df62lJDA?si=vhd5GY7c_y0eBABR

public class WebSlinging : MonoBehaviour
{
    Rigidbody2D rb;
    LineRenderer lr;
    DistanceJoint2D dj;
    public LayerMask grappleLayer;
    public bool isGrappling;
    Vector2 grapplePoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        dj = GetComponent<DistanceJoint2D>();
        lr.enabled = false;
        dj.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Physics2D.OverlapCircle(point, 0.1f, grappleLayer))
            {
                grapplePoint = point;
                isGrappling = true;
                lr.enabled = true;
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, grapplePoint);

                dj.enabled = true;
                dj.autoConfigureDistance = false;
                dj.connectedAnchor = grapplePoint;
                dj.distance = Vector2.Distance(transform.position, grapplePoint);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isGrappling = false;
            lr.enabled = false;
            dj.enabled = false;
        }

        if (isGrappling)
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, grapplePoint);
        }
    }
}
