using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<StasisCollision>(out StasisCollision collisionScript))
        {
            ProcessHitPoint(collisionScript);
        }
    }

    private void ProcessHitPoint(StasisCollision collisionScript)
    {
        collisionScript.HitStasisObject(Vector3.zero); //Placeholder Vector for now
    }
}
