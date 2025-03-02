using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class StasisCollision : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float baseLaunchForce;
    private float potentialLaunchForce = 0;

    [SerializeField] private float stasisTime;

    [SerializeField] private ParticleSystem chainParticles;

    Vector3 directionToLaunch;
    Vector3 stasisPositionHit;

    public bool isInStasis = false;

    private MeshRenderer meshRenderer;
    [SerializeField] private Material emissiveMaterial;
    [SerializeField] private Material baseMaterial;

    [SerializeField] private GameObject directionIndicatorPrefab;
    private DirectionIndicator directionIndicator;

    void Awake()
    {
        emissiveMaterial.SetFloat("_PowerAmount", 0);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
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
                directionIndicator = Instantiate(directionIndicatorPrefab, transform.position, quaternion.identity).GetComponent<DirectionIndicator>();
            }

            directionIndicator.SetDirection(transform.position, directionToLaunch);

            emissiveMaterial.SetFloat("_PowerAmount", Mathf.Clamp(emissiveMaterial.GetFloat("_PowerAmount") + 0.2f, 0f, 1f));
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

        meshRenderer.material = emissiveMaterial;

        chainParticles.Play();

        yield return new WaitForSeconds(stasisTime);

        isInStasis = false;

        rb.isKinematic = false;

        Destroy(directionIndicator.gameObject);

        meshRenderer.material = baseMaterial;

        emissiveMaterial.SetFloat("_PowerAmount", 0);

        float finalLaunchForce = baseLaunchForce * potentialLaunchForce;

        LaunchObject(directionToLaunch, stasisPositionHit, finalLaunchForce);
    }
}
