using Pathfinding; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] float spawnTimer = 1f;
    [SerializeField] Pathfinder enemyPrefab;
    [SerializeField] Transform waypointParent;
    [SerializeField] Transform enemyParent;

    private void Start() {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies() {
        while (true) {
            yield return new WaitForSeconds(spawnTimer);
            Pathfinder pathfinder = Instantiate(enemyPrefab, transform.position, Quaternion.identity, enemyParent);
            pathfinder.SetWaypoints(waypointParent);
        }
    }
}
