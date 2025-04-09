using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class WinScript : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject);
        ScoreManager.instance.SaveScore();

    }
}
