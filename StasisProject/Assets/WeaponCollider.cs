using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [SerializeField] Transform testObject;
    [SerializeField] Transform rayPivot;

    [SerializeField] LayerMask sphereLayer;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<StasisCollision>(out StasisCollision collisionScript))
        {
            ProcessHitPoint(collisionScript);
        }
    }

    private void ProcessHitPoint(StasisCollision collisionScript)
    {
        Vector3 rayDirection = collisionScript.gameObject.transform.position - rayPivot.position;
        Vector3 rayStartPoint = rayPivot.position - (rayDirection * 4);

        RaycastHit hit;
        Physics.Raycast(rayStartPoint, rayDirection, out hit, Mathf.Infinity, sphereLayer);

        testObject.position = hit.point;

        collisionScript.HitStasisObject(hit.point);
    }
}
