using UnityEngine;

public class StasisCollision : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float launchForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void HitStasisObject(Vector3 positionHit)
    {
        Vector3 directionToLaunch = transform.position - positionHit;

        LaunchObject(directionToLaunch, positionHit);
    }

    private void LaunchObject(Vector3 directionToLaunch, Vector3 positionToHit)
    {
        rb.AddForceAtPosition(directionToLaunch * launchForce, positionToHit);
    }
}
