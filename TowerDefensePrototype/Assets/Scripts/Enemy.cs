using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour {
    [SerializeField] int totalHealthPoints = 10;
    [SerializeField] EnemyPath enemyPath = EnemyPath.Red;

    private int currentHealthPoints;
    private AIDestinationSetter aiDestinationSetter;

    private void Start() {
        currentHealthPoints = totalHealthPoints;
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
    }
    
    public int GetHealth() {
        return currentHealthPoints;
    }

    public EnemyPath GetEnemyPath() {
        return enemyPath;
    }

    public void SetHealth(int health) {
        currentHealthPoints = health;
    }

    public void SetTarget(Target target) {
        if (aiDestinationSetter == null) {
            aiDestinationSetter = GetComponent<AIDestinationSetter>();
        }
        aiDestinationSetter.target = target.gameObject.transform;
    }

    public void TakeDamage(int damage) {
        currentHealthPoints -= damage;
        if (currentHealthPoints <= 0) {
            Destroy(gameObject);
        }
    }

}

public enum EnemyPath {
    Red,
    Blue
}
