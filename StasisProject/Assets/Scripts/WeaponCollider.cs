using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    private BoxCollider boxCollider;

    [SerializeField] GameObject hitEffectPrefab;

    [SerializeField] Transform rayPivot;

    [SerializeField] float weaponForce = 1;

    [SerializeField] LayerMask sphereLayer;

    private bool isColliding = false;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(1);
        isColliding = false;
        yield return null;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<StasisCollision>(out StasisCollision collisionScript))
        {
            ProcessHitPoint(collisionScript);
        }
    }

    private void ProcessHitPoint(StasisCollision collisionScript)
    {
        if (isColliding)
            return;

        Vector3 rayDirection = collisionScript.gameObject.transform.position - rayPivot.position;
        Vector3 rayStartPoint = rayPivot.position - (rayDirection * 4);

        RaycastHit hit;
        Physics.Raycast(rayStartPoint, rayDirection, out hit, Mathf.Infinity, sphereLayer);

        GameObject hitEffectInstance = Instantiate(hitEffectPrefab, hit.point, quaternion.identity);
        hitEffectInstance.transform.rotation = Quaternion.LookRotation(-rayDirection);
        hitEffectInstance.GetComponent<ParticleSystem>().Play();

        boxCollider.enabled = false;
        isColliding = true;

        collisionScript.HitStasisObject(hit.point, weaponForce);

        StartCoroutine(ResetHit());
    }
}
