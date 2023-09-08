using Pathfinding; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] AIDestinationSetter enemyPrefab;
    [SerializeField] Transform target;

    private void Start() {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies() {
        while (true) {
            yield return new WaitForSeconds(.5f);
            AIDestinationSetter aIDestinationSetter = Instantiate(enemyPrefab);
            aIDestinationSetter.target = target;
        }
    }
}
