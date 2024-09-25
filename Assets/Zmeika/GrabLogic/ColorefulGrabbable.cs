using System;
using UnityEngine;

public class ColorefulGrabbable : BaseGrabbable
{
    [SerializeField] private Renderer _targetRenderer;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _onHoverMaterial;
    [SerializeField] private Material _onGrabbedMaterial;

    public override void OnDropped()
    {
        base.OnDropped();
        UpdateMaterial();
    }
    public override void OnGrabbed()
    {
        base.OnGrabbed();
        UpdateMaterial();
    }
    public override void OnHoverEnter()
    {
        base.OnHoverEnter();
        UpdateMaterial();
    }
    public override void OnHoverExit()
    {
        base.OnHoverExit();
        UpdateMaterial();
    }

    protected override void Update()
    {
        base.Update();

        if (IsGrabbed)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Plane.Raycast(ray,out float distance))
            {
                transform.position = ray.GetPoint(distance);
            }
        }
    }

    private void UpdateMaterial()
    {
        if (IsGrabbed)
        {
            _targetRenderer.material = _onGrabbedMaterial;
        } else if (IsHovered)
        {
            _targetRenderer.material = _onHoverMaterial;
        } else
        {
            _targetRenderer.material = _defaultMaterial;
        }
    }
}