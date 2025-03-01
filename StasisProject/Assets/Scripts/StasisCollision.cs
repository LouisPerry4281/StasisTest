using System.Collections;
using Unity.Mathematics;
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

    [SerializeField] private Material material;

    [SerializeField] private GameObject directionIndicatorPrefab;
    private DirectionIndicator directionIndicator;

    void Awake()
    {
        material.SetFloat("_PowerAmount", 0);
    }

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

            if (directionIndicator == null)
            {
                print("spawn");
                directionIndicator = Instantiate(directionIndicatorPrefab, transform.position, quaternion.identity).GetComponent<DirectionIndicator>();
                print(directionIndicator);
            }

            directionIndicator.SetDirection(transform.position, directionToLaunch);

            material.SetFloat("_PowerAmount", Mathf.Clamp(material.GetFloat("_PowerAmount") + 0.2f, 0f, 1f));
            return;
        }

        LaunchObject(directionToLaunch, positionHit, baseLaunchForce);
    }

    private void LaunchObject(Vector3 direction, Vector3 positionToHit, float force)
    {
        print(force);
        rb.AddForceAtPosition(direction * force, positionToHit);
    }

    public IEnumerator FreezeObject()
    {
        rb.isKinematic = true;

        isInStasis = true;

        yield return new WaitForSeconds(stasisTime);

        isInStasis = false;

        rb.isKinematic = false;

        material.SetFloat("_PowerAmount", 0);

        float finalLaunchForce = baseLaunchForce * potentialLaunchForce;

        LaunchObject(directionToLaunch, stasisPositionHit, finalLaunchForce);
    }
}
