using UnityEngine;

public class StasisManager : MonoBehaviour
{
    public bool stasisMode = false;

    private Camera cam;

    [SerializeField] LayerMask stasisLayer;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (stasisMode)
        {
            CastRay();
        }
    }

    private void CastRay()
    {
        Vector3 rayDirection = cam.transform.forward;

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, rayDirection, out hit, Mathf.Infinity, stasisLayer))
        {
            if ()
        }
    }
}
