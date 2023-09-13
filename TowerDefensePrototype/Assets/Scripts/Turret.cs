using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    [SerializeField] protected Bullet bulletPrefab;
    [SerializeField] protected float shotInterval;
    [SerializeField] Transform turretHead;
    [SerializeField] GameObject dragableTurretPrefab;
    
    protected Transform bulletParent;
    private Transform enemyParent;
    private Transform closestEnemy;
    private List<Transform> enemiesInRange = new List<Transform>();

    protected float timer = 0;

    private void Start() {
        bulletParent = GameObject.Find("Bullet Parent").transform;
        enemyParent = GameObject.Find("Enemy Parent").transform;
    }

    private void Update() {
        closestEnemy = FindClosestEnemy();
        if (closestEnemy == null) {
            turretHead.rotation = Quaternion.identity;
        } else {
            Vector2 direction = new Vector2(closestEnemy.position.x - turretHead.position.x, closestEnemy.position.y - turretHead.position.y);
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            turretHead.transform.eulerAngles = new Vector3(0, 0, rotation-90);
        }
        timer += Time.deltaTime;
        if (timer >= shotInterval && closestEnemy) {
            Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletParent);
            bullet.SetTargetedEnemy(closestEnemy);
        }
        if (timer >= shotInterval) {
            timer -= shotInterval;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            enemiesInRange.Add(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Enemy") && enemiesInRange.Contains(other.transform)) {
            enemiesInRange.Remove(other.transform);
        }
    }

    public float GetTimer() {
        return timer;
    }

    public void SwitchTurrets() {
        Instantiate(dragableTurretPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private Transform FindClosestEnemy() {
        if (enemiesInRange.Count == 0) {
            return null;
        }
        foreach (Transform enemyTransform in enemiesInRange) {
            if (closestEnemy == null || ((transform.position - enemyTransform.position).magnitude < (transform.position - closestEnemy.position).magnitude)) {
                closestEnemy = enemyTransform;
            }
        }
        return closestEnemy;
    }
}
