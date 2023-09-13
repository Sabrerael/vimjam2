using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapController : MonoBehaviour {
    [SerializeField] List<Transform> snapPoints;
    [SerializeField] List<DragableTurret> dragableTurrets;
    [SerializeField] float snapRange = 0.5f;

    private void Start() {
        foreach (DragableTurret dragableTurret in dragableTurrets) {
            dragableTurret.dragEndedCallback += OnDragEnded;
        }
    }

    private void OnDragEnded(DragableTurret dragableTurret) {
        float closestDistance = -1;
        Transform closestSnapPoint = null;

        foreach (Transform snapPoint in snapPoints) {
            float currentDistance = Vector2.Distance(dragableTurret.transform.position, snapPoint.position);
            if (closestSnapPoint == null || currentDistance < closestDistance) {
                closestSnapPoint = snapPoint;
                closestDistance = currentDistance;
            }
        }

        if (closestSnapPoint != null && closestDistance <= snapRange) {
            dragableTurret.transform.position = closestSnapPoint.position;
        }
    }
}
