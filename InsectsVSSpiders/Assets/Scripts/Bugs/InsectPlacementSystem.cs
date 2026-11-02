using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InsectPlacementSystem : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera _camera;

    [Header("Placement settings")]
    [SerializeField] private LayerMask _buildableLayers;
    [SerializeField] private LayerMask _blockedLayers;
    [SerializeField] private float _placementZ = -2f;
    [SerializeField] private float _placementRadius = 0.35f;

    [Header("Preview colors")]
    [SerializeField]
    private Color _validColor =
        new Color(0f, 1f, 0f, 0.5f);

    [SerializeField]
    private Color _invalidColor =
        new Color(1f, 0f, 0f, 0.5f);

    private GameObject _selectedPrefab;
    private GameObject _preview;

    private Action<bool> _onFinished;

    public bool IsPlacing => _preview != null;

    // Start placing an insect.
    public bool StartPlacement(
        GameObject prefab,
        Action<bool> onFinished)
    {
        if (IsPlacing)
            return false;

        if (prefab == null || _camera == null)
        {
            Debug.LogError("Prefab or Camera is missing!");
            return false;
        }

        if (_buildableLayers.value == 0)
        {
            Debug.LogError("Buildable Layers are not assigned!");
            return false;
        }

        _selectedPrefab = prefab;
        _onFinished = onFinished;

        // Create the preview.
        _preview = Instantiate(prefab);
        _preview.name = prefab.name + "_Preview";

        // Disable scripts on the preview.
        foreach (MonoBehaviour script in
                 _preview.GetComponentsInChildren<MonoBehaviour>(true))
        {
            script.enabled = false;
        }

        // Disable colliders.
        foreach (Collider2D col in
                 _preview.GetComponentsInChildren<Collider2D>(true))
        {
            col.enabled = false;
        }

        // Disable physics.
        foreach (Rigidbody2D rb in
                 _preview.GetComponentsInChildren<Rigidbody2D>(true))
        {
            rb.simulated = false;
        }

        // Disable animations.
        foreach (Animator animator in
                 _preview.GetComponentsInChildren<Animator>(true))
        {
            animator.enabled = false;
        }

        SetPreviewColor(_invalidColor);

        return true;
    }

    private void Update()
    {
        if (!IsPlacing)
            return;

        // Cancel placement.
        if (Input.GetMouseButtonDown(1) ||
            Input.GetKeyDown(KeyCode.Escape))
        {
            FinishPlacement(false);
            return;
        }

        if (!TryGetMousePosition(out Vector3 mousePosition))
            return;

        Vector3 position = new Vector3(
            mousePosition.x,
            mousePosition.y,
            _placementZ
        );

        // Move preview with mouse.
        _preview.transform.position = position;

        bool valid = IsValidPosition(position);

        SetPreviewColor(valid ? _validColor : _invalidColor);

        // Place insect with left mouse button.
        if (Input.GetMouseButtonDown(0) &&
            valid &&
            !IsPointerOverUI())
        {
            Instantiate(
                _selectedPrefab,
                position,
                Quaternion.identity
            );

            FinishPlacement(true);
        }
    }

    private bool TryGetMousePosition(out Vector3 position)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        Plane plane = new Plane(
            Vector3.forward,
            new Vector3(0f, 0f, _placementZ)
        );

        if (plane.Raycast(ray, out float distance))
        {
            position = ray.GetPoint(distance);
            return true;
        }

        position = Vector3.zero;
        return false;
    }

    private bool IsValidPosition(Vector3 position)
    {
        Vector2 point = new Vector2(position.x, position.y);

        // Must be on the buildable area.
        Collider2D ground = Physics2D.OverlapPoint(
            point,
            _buildableLayers
        );

        if (ground == null)
            return false;

        // Must not overlap an existing tower.
        Collider2D obstacle = Physics2D.OverlapCircle(
            point,
            _placementRadius,
            _blockedLayers
        );

        return obstacle == null;
    }

    private void SetPreviewColor(Color color)
    {
        if (_preview == null)
            return;

        SpriteRenderer[] renderers =
            _preview.GetComponentsInChildren<SpriteRenderer>(true);

        foreach (SpriteRenderer sprite in renderers)
        {
            sprite.color = color;
        }
    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current != null &&
               EventSystem.current.IsPointerOverGameObject();
    }

    private void FinishPlacement(bool placed)
    {
        GameObject oldPreview = _preview;
        _preview = null;

        _selectedPrefab = null;

        if (oldPreview != null)
            Destroy(oldPreview);

        Action<bool> callback = _onFinished;
        _onFinished = null;

        callback?.Invoke(placed);
    }
}