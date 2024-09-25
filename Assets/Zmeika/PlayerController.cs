using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _rayDistance = 1000;
    [SerializeField] private LayerMask _targetLayers;

    private BaseGrabbable _currentGrabbable;

    private void Update()
    {
        if (_currentGrabbable == null || !_currentGrabbable.IsGrabbed)
        {
            Ray ray = Camera.main.ScreenPointToRay(TargetScreenPosition);
            if (Physics.Raycast(ray, out RaycastHit hitResult, _rayDistance, _targetLayers))
            {
                if (_currentGrabbable == null || _currentGrabbable.gameObject != hitResult.collider.gameObject)
                {
                    _currentGrabbable?.OnHoverExit();

                    _currentGrabbable = hitResult.collider.gameObject.GetComponent<BaseGrabbable>();

                    _currentGrabbable?.OnHoverEnter();
                    _currentGrabbable?.SetMovePlane(Vector3.up);
                }
            }
            else
            {
                _currentGrabbable?.OnHoverExit();
                _currentGrabbable = null;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (_currentGrabbable != null)
            {
                _currentGrabbable.OnGrabbed();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (_currentGrabbable != null)
            {
                _currentGrabbable.OnDropped();
            }
        }
    }

    private Vector2 TargetScreenPosition
    {
        get
        {
            return Input.mousePosition;
        }
    }
}
