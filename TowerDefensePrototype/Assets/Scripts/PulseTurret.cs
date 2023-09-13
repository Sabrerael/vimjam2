using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseTurret : Turret {

    private void Update() {
        timer += Time.deltaTime;
        if (timer >= shotInterval) {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletParent);
        }
        if (timer >= shotInterval) {
            timer -= shotInterval;
        }
    }
}
