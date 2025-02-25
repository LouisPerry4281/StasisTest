using System.Collections;
using UnityEngine;

public class StasisCollision : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float launchForce;

    public bool isInStasis = false;

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

    public IEnumerator FreezeObject()
    {
        rb.isKinematic = true;

        isInStasis = true;

        yield return new WaitForSeconds(3);

        isInStasis = false;

        rb.isKinematic = false;

        //launch object
    }
}
