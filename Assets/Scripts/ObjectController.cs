using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class ObjectController : MonoBehaviour {

    private Vector3 offset;
    private Vector3 extents;
    private Transform dragging = null;
    [SerializeField] private LayerMask moveableLayers;
    [SerializeField] private GameObject dragBounds;

    // Update is called once per frame
    void Update() {
        // Check if left click has been pressed
        if (Input.GetMouseButtonDown(0)) {
            // Cast a Ray out into the screen and see if it hits an object
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.PositiveInfinity, moveableLayers);

            // If object hit, set dragging var to that object and get the object
            if (hit) {
                dragging = hit.transform;
                Vector3 scale = dragging.localScale;
                offset = dragging.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 unscaledExtents = dragging.GetComponent<SpriteRenderer>().sprite.bounds.extents;
                extents = new Vector3(
                    unscaledExtents.x * scale.x,
                    unscaledExtents.y * scale.y,
                    unscaledExtents.z * scale.z
                );
            }
        }
        // If left click has been released then set dragging to null again
        else if (Input.GetMouseButtonUp(0)) {
            dragging = null;
        }

        // Move object that is being dragged
        if (dragging != null) {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;

            Bounds bounds = dragBounds.GetComponent<BoxCollider2D>().bounds;

            // Find corners of bound box
            Vector3 topRight = new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y + bounds.extents.y, dragBounds.transform.position.z);
            Vector3 bottomLeft = new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y, dragBounds.transform.position.z);

            // Set the mins and max of the positions
            position.x = Mathf.Clamp(position.x, bottomLeft.x + extents.x, topRight.x - extents.x);
            position.y = Mathf.Clamp(position.y, bottomLeft.y + extents.y, topRight.y - extents.y);

            dragging.position = position;
        }
        // Disables player input once bullet has been fired
        if (Input.GetKeyDown(KeyCode.Space)) {
            // GetComponent<ObjectController>().enabled = false;
        }
    }
}
