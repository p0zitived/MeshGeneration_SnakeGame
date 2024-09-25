using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class BaseGrabbable : MonoBehaviour
{
    public bool IsGrabbed { private set; get; }
    public bool IsHovered { private set; get; }

    public Plane Plane { private set; get; }

    private void Reset()
    {
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<SphereCollider>().radius = 1;
        }
        gameObject.layer = LayerMask.NameToLayer("Grabbable");
    }

    protected virtual void Update()
    {

    }

    public virtual void OnHoverEnter()
    {
        IsHovered = true;
    }
    public virtual void OnHoverExit()
    {
        IsHovered = false;
    }
    public virtual void OnGrabbed()
    {
        IsGrabbed = true;
    }
    public virtual void OnDropped()
    {
        IsGrabbed = false;
    }
    public virtual void SetMovePlane(Vector3 planeNormal)
    {
        Plane = new Plane(planeNormal,0);
    }
}
