using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject player;
    private bool selected = false;
    private Vector3 mousePos;
    private float angle;
    private float radius;

    // Start is called before the first frame update
    void Start() {
        // Find radius of the arm
        radius = Mathf.Sqrt(Mathf.Pow(player.transform.position.x - transform.position.x, 2) + Mathf.Pow(player.transform.position.y - transform.position.y, 2));
    }

    // Update is called once per frame
    void Update() {
        if (selected) {

            //Get the angle btw the mouse and the player
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos = mousePos - player.transform.position;
            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            if (angle < -60.0f) angle = -60.0f;
            if (angle > 60.0f) angle = 60.0f;
            transform.localEulerAngles = new Vector3(0, 0, angle);
            float xpos = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            float ypos = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            transform.localPosition = new Vector3(player.transform.position.x + xpos, player.transform.position.y + ypos, 0);
        }
    }

    private void OnMouseDown() {
        selected = true;
    }

    private void OnMouseUp() {
        selected = false;
    }

}
