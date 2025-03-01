using UnityEngine;

public class StasisManager : MonoBehaviour
{
    public bool stasisMode = false;

    private Camera cam;

    [SerializeField] LayerMask stasisLayer;

    StasisCollision highlightedObject = null;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            highlightedObject = null;
            stasisMode = !stasisMode;
        }

        if (stasisMode)
        {
            CastRay();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && highlightedObject != null && !highlightedObject.isInStasis)
        {
            StartCoroutine(highlightedObject.FreezeObject());
        }
    }

    private void CastRay()
    {
        Vector3 rayDirection = cam.transform.forward;

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, rayDirection, out hit, Mathf.Infinity, stasisLayer))
        {
            highlightedObject = hit.collider.gameObject.GetComponent<StasisCollision>();
        }
        else
        {
            print("Not hit");
            highlightedObject = null;
        }
    }
}
