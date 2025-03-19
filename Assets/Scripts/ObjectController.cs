using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    private Vector3 offset;
    private Transform dragging = null;
    [SerializeField] private LayerMask moveableLayers;

    // Update is called once per frame
    void Update() {
        // Check if left click has been pressed
        if (Input.GetMouseButtonDown(0)) {
            // Cast a Ray out into the screen and see if it hits an object
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, moveableLayers);

            // If object hit, set dragging var to that object and get the object
            if (hit) {
                dragging = hit.transform;

                offset = hit.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        // If left click has been released then set dragging to null again
        else if (Input.GetMouseButtonUp(0)) {
            dragging = null;
        }

        // Move object that is being dragged
        if (dragging != null) {
            dragging.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }
}
