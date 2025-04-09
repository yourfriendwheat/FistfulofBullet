using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceRNG : MonoBehaviour
{

    private Vector3 extents;
    [SerializeField] private GameObject dragBounds;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 unscaledExtents = transform.GetComponent<SpriteRenderer>().sprite.bounds.extents;
        extents = new Vector3(
            unscaledExtents.x * transform.localScale.x,
            unscaledExtents.y * transform.localScale.y,
            unscaledExtents.z * transform.localScale.z
        );

        Bounds bounds = dragBounds.GetComponent<BoxCollider2D>().bounds;

        float x = Random.Range(bounds.center.x - bounds.extents.x + extents.x, bounds.center.x + bounds.extents.x - extents.x);
        float y = Random.Range(bounds.center.y - bounds.extents.y + extents.y, bounds.center.y + bounds.extents.y - extents.y);

        transform.position = new Vector3(x, y, transform.position.z);
    }

   
}
