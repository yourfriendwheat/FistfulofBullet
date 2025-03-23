using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] private LayerMask simLayer;
    public GameObject Init(GameObject bulletPrefab, Vector3 spawnPoint, bool ghost) {
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint, Quaternion.identity);
        return bullet;
    }
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision) {
        if (gameObject.layer != 8) {GetComponent<AudioSource>().Play();}
        
        
    }
}
