using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrappingBehaviour : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        // TODO: pass camera as argument.
        cam = Camera.main;
    }


    private void FixedUpdate()
    {
        Vector3 worldPosition = this.transform.position;
        Vector3 viewportPosition = cam.WorldToViewportPoint(worldPosition);

        if (!IsWithinViewport(viewportPosition))
        {
            // Wrap position around screen.
            float x = WrapUnitInterval(viewportPosition.x);
            float y = WrapUnitInterval(viewportPosition.y);
            Vector3 wrappedViewportPosition = new Vector3(x, y, viewportPosition.z);
            Vector3 wrappedWorldPosition = cam.ViewportToWorldPoint(wrappedViewportPosition);
            this.transform.position = wrappedWorldPosition;
        }
    }

    private bool IsWithinViewport(Vector3 pos)
    {
        return (pos.x >= 0.0f)
            && (pos.x <= 1.0f)
            && (pos.y >= 0.0f)
            && (pos.y <= 1.0f);
    }

    private float WrapUnitInterval(float f)
    {
        if (f >= 1.0f)
        {
            f = 0.0f;
        }
        else if (f <= 0.0f)
        {
            f = 1.0f;
        }

        return f;
    }
}
