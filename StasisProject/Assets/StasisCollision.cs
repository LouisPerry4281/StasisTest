using System.Collections;
using UnityEngine;

public class StasisCollision : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float baseLaunchForce;
    private float potentialLaunchForce = 0;

    [SerializeField] private float stasisTime;

    Vector3 directionToLaunch;
    Vector3 stasisPositionHit;

    public bool isInStasis = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void HitStasisObject(Vector3 positionHit, float forceOfHit)
    {
        directionToLaunch = transform.position - positionHit;

        if (isInStasis)
        {
            potentialLaunchForce += forceOfHit;
            stasisPositionHit = positionHit;
            return;
        }

        LaunchObject(directionToLaunch, positionHit, baseLaunchForce);
    }

    private void LaunchObject(Vector3 direction, Vector3 positionToHit, float force)
    {
        rb.AddForceAtPosition(direction * force, positionToHit);
    }

    public IEnumerator FreezeObject()
    {
        rb.isKinematic = true;

        isInStasis = true;

        yield return new WaitForSeconds(stasisTime);

        isInStasis = false;

        rb.isKinematic = false;

        float finalLaunchForce = baseLaunchForce * potentialLaunchForce;

        LaunchObject(directionToLaunch, stasisPositionHit, finalLaunchForce);
    }
}
