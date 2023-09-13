using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] protected int damage = 5;
    [SerializeField] float movementSpeed = 10;
    [SerializeField] BulletType bulletType = BulletType.Basic;

    private Transform targetedEnemy;
    private Vector2 direction;

    private void Update() {
        if (targetedEnemy != null) {
            direction = (targetedEnemy.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, targetedEnemy.position, Time.deltaTime * movementSpeed);
        } else {
            transform.position += (Vector3)direction * Time.deltaTime * movementSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public BulletType GetBulletType() {
        return bulletType;
    }

    public Transform GetTargetedEnemy() {
        return targetedEnemy;
    }

    public void SetTargetedEnemy(Transform _targetedEnemy) {
        targetedEnemy = _targetedEnemy;
    }
}

public enum BulletType {
    Basic,
    Pulse,
    Lazer
}