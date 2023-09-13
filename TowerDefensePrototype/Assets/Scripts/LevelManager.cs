using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance = null;

    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] Target redTarget;
    [SerializeField] Target blueTarget;
    [SerializeField] int enemiesForLoseState = 10;
    [SerializeField] GameObject enemyParent;
    [SerializeField] GameObject bulletParent;
    [SerializeField] GameObject turretParent;
    [SerializeField] List<DragableTurret> dragableTurrets;
    [SerializeField] Enemy redEnemy;
    [SerializeField] Enemy blueEnemy;

    private int enemiesReachedTargets = 0;
    private bool gamePaused = true;

    // Checkpoint data needs to include the music state, the bullets and their types, the positions of the bullets, the positions of the enemies, the health of the enemies, and the number of enemies that reached the target 
    private float musicTime;
    private List<float> turretTimers;
    private List<BulletCheckpointValues> bullets;
    private List<EnemyCheckpointValues> enemies;
    private int checkpointEnemiesReachedTargets;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Time.timeScale = 0;
    }

    private void OnPause() {
        if (gamePaused) {
            Time.timeScale = 1;
            gamePaused = false;
            SwitchTurrets();
            Debug.Log("Unpaused");
        } else {
            Time.timeScale = 0;
            gamePaused = true;
            Debug.Log("Paused");
            SetCheckpoint();
        }
    }

    private void OnUnpause() {
        ResetToCheckpoint();
    }

    public void AddEnemyReachedTarget() {
        enemiesReachedTargets++;
        if (enemiesReachedTargets >= enemiesForLoseState) {
            Debug.Log("You Lose");
            ResetToCheckpoint();
        }
    }

    public int GetEnemiesReachedTargets() {
        return enemiesReachedTargets;
    } 

    private void SetCheckpoint() {
        enemies = new List<EnemyCheckpointValues>();
        if (enemyParent.transform.childCount != 0) {
            foreach(Enemy enemy in enemyParent.GetComponentsInChildren<Enemy>()) {
                EnemyCheckpointValues enemyCheckpoint = new EnemyCheckpointValues();
                enemyCheckpoint.health = enemy.GetHealth();
                enemyCheckpoint.position = enemy.transform.position;
                enemyCheckpoint.enemyPath = enemy.GetEnemyPath();

                enemies.Add(enemyCheckpoint);
            }
        }

        bullets = new List<BulletCheckpointValues>();
        if (bulletParent.transform.childCount != 0) {
            foreach(Bullet bullet in bulletParent.GetComponentsInChildren<Bullet>()) {
                BulletCheckpointValues bulletCheckpoint = new BulletCheckpointValues();
                bulletCheckpoint.position = bullet.transform.position;
                bulletCheckpoint.bulletType = bullet.GetBulletType();
                int index = 0;
                foreach(Enemy enemy in enemyParent.GetComponentsInChildren<Enemy>()) {
                    if (enemy.transform == bullet.GetTargetedEnemy()) {
                        break;
                    } else {
                        index++;
                    }
                }
                bulletCheckpoint.targetedEnemyIndex = index;

                bullets.Add(bulletCheckpoint);
            }
        }

        turretTimers = new List<float>();
        foreach(Turret turret in turretParent.GetComponentsInChildren<Turret>()) {
            turretTimers.Add(turret.GetTimer());
        }
        
    }

    private void SwitchTurrets() {
        foreach (DragableTurret dragableTurret in dragableTurrets) {
            dragableTurret.SwitchTurretPrefabs();
        }
    }

    private void ResetToCheckpoint() {
        Debug.Log("Resetting");
        Time.timeScale = 0;
        gamePaused = true;
        enemiesReachedTargets = checkpointEnemiesReachedTargets;
        foreach (Enemy enemy in enemyParent.GetComponentsInChildren<Enemy>()) {
            Destroy(enemy.gameObject);
        }
        foreach (Bullet bullet in bulletParent.GetComponentsInChildren<Bullet>()) {
            Destroy(bullet.gameObject);
        }

        if (enemies.Count != 0) {
            foreach (EnemyCheckpointValues item in enemies) {
                if (item.enemyPath == EnemyPath.Red) {
                    Enemy enemy = Instantiate(redEnemy, item.position, Quaternion.identity, enemyParent.transform);
                    enemy.SetHealth(item.health);
                    enemy.SetTarget(redTarget);
                } else {
                    Enemy enemy = Instantiate(blueEnemy, item.position, Quaternion.identity, enemyParent.transform);
                    enemy.SetHealth(item.health);
                    enemy.SetTarget(blueTarget);
                }
            }
        }
        if (bullets.Count != 0) {
            foreach (BulletCheckpointValues item in bullets) {
                
            }
        }
    }

    private struct EnemyCheckpointValues {
        public int health;
        public Vector3 position;
        public EnemyPath enemyPath;
    }

    private struct BulletCheckpointValues {
        public int targetedEnemyIndex;
        public Vector3 position;
        public BulletType bulletType;
    }
}
