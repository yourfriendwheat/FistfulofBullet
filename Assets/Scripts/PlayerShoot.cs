using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerShoot : MonoBehaviour {

    [Header("Initialize Setup")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject bulletSprite;

    [Header("Bullet Settings")]
    [SerializeField] private float bulletSpeed;

    [Header("Projection Settings")]
    [SerializeField] private Projection projection;


    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Space)) {
            Shoot();
            disableControls();
        }

        // Simulate the trajectory of the bullet
        projection.SimulateTrajectory(bulletSprite, spawnPoint, spawnPoint.right * bulletSpeed);

    }

    // Shoots bulllet
    void Shoot() {
        GameObject bullet = Instantiate(bulletSprite, spawnPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(spawnPoint.right * bulletSpeed, ForceMode2D.Impulse);
    }
    //Disables player controls when shot is fired
    void disableControls() {
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerShoot>().enabled = false;
        GetComponent<LineRenderer>().enabled = false;
    }
}
