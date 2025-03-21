using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    [Header("Initialize Setup")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject bulletSprite;
    
    [Header("Bullet Settings")]
    [SerializeField] private float bulletSpeed;

     
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Shoot();
        }
    }

    void Shoot(){
        GameObject bullet = Instantiate(bulletSprite, spawnPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(spawnPoint.right * bulletSpeed, ForceMode2D.Impulse);
    }
}
