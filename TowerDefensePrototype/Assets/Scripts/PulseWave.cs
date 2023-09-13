using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseWave : Bullet {
    private void Start() {
        Destroy(gameObject, 0.75f);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
