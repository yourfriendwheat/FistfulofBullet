using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private GameObject player;
    private bool selected = false;
    private Vector3 mousePos;
    private Vector3 center;
    private float angle = -45;
    private float radius;

    // Start is called before the first frame update
    void Start() {
        // Find radius of the arm
        center = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        radius = Mathf.Sqrt(Mathf.Pow(center.x - transform.position.x, 2) + Mathf.Pow(center.y - transform.position.y, 2));
        GetComponent<LineRenderer>().enabled = false;
        calcPosition();
    }

    // Update is called once per frame
    void Update() {
        if (selected) {

            // Get the angle between the mouse and the player
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos = mousePos - center;
            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

            // Restrict the angle to 60 degress up and down
            if (angle < -60.0f) angle = -60.0f;
            if (angle > 60.0f) angle = 60.0f;
            calcPosition();
        }
    }

    // Keep track on if the Arm is selected or not
    private void OnMouseDown() {
        GetComponent<LineRenderer>().enabled = true;
        selected = true;
    }

    private void OnMouseUp() {
        selected = false;
    }

    private void calcPosition() {
        // Set Angle
        transform.localEulerAngles = new Vector3(0, 0, angle);
        // Make sure that the radius is consistent
        float xpos = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
        float ypos = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
        transform.localPosition = new Vector3(center.x + xpos, center.y + ypos, 0);
    }

}
