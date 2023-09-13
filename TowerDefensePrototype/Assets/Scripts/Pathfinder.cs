using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {
    [SerializeField] float movementSpeed;

    private List<Transform> waypoints;
    private int waypointIndex = 0;

    private void Start() {
        transform.position = waypoints[waypointIndex].position;
    }

    private void Update() {
        FollowPath();
    }

    private void FollowPath() {
        if (waypointIndex < waypoints.Count) {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            Quaternion targetRotation = waypoints[waypointIndex].rotation;
            float delta = movementSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, .75f);
            if (transform.position == targetPosition) {
                waypointIndex++;
            }
        }
    }

    public void SetWaypoints(Transform waypointParent) { 
        waypoints = new List<Transform>();
        foreach (Transform child in waypointParent) {
            waypoints.Add(child);
        }
    }
}
