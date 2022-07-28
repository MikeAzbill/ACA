
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public Vector3 offsets;
    [Range(0, 10)]
    public float smoothingFactor;

    private void FixedUpdate()
    {
        CamFollow();
    }

    void CamFollow()
    {
        Vector3 targetPos = target.position + offsets;
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothingFactor*Time.fixedDeltaTime);
        transform.position = smoothPos;
    }
}
