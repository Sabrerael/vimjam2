using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragableTurret : MonoBehaviour {//, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
    [SerializeField] Transform turretParent;
    [SerializeField] Turret turretPrefab;
    
    private bool isDragged;
    private Vector3 mousePositionOffset;
    private Vector3 startingPosition;

    public delegate void DragEndedDelegate(DragableTurret dragableTurret);

    public DragEndedDelegate dragEndedCallback;
    
    /*[SerializeField] Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }

    public void OnPointerDown(PointerEventData eventData) { }*/

    public void SwitchTurretPrefabs() {
        Instantiate(turretPrefab, transform.position, Quaternion.identity, turretParent);
        Destroy(gameObject);
    }

    private Vector3 GetMouseWorldPosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown() {
        Debug.Log("Clicked on object " + gameObject.name);
        isDragged = true;
        mousePositionOffset = transform.position - GetMouseWorldPosition();
        startingPosition = transform.position;
    }

    private void OnMouseDrag() {
        if (isDragged) {
            transform.position = GetMouseWorldPosition() + mousePositionOffset;
        }
    }

    private void OnMouseUp() {
        isDragged = false;
        dragEndedCallback(this);
    }
}
