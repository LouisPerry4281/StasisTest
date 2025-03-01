using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    public void SetDirection(Vector3 position, Vector3 directionToFace)
    {
        transform.position = new Vector3(position.x, position.y + 2, position.z);
        Vector3 positionToFace = transform.position + directionToFace;
        transform.LookAt(positionToFace);
    }
}
