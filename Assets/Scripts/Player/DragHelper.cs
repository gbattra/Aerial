using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHelper : MonoBehaviour
{
    public float dragFactor;
    public float angularDragFactor;
    
    private float startDrag;
    private float startAngularDrag;

    public void Init(float drag, float angularDrag)
    {
        startDrag = drag;
        startAngularDrag = angularDrag;
    }

    public float ComputeDrag(float forwardSpeed)
    {
        var speedDrag = forwardSpeed * dragFactor;
        return speedDrag + startDrag;
    }
    
    public float ComputeAngularDrag(float forwardSpeed)
    {
        var speedDrag = forwardSpeed * angularDragFactor;
        return speedDrag + startAngularDrag;
    }
}
