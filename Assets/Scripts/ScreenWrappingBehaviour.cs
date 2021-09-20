using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrappingBehaviour : MonoBehaviour
{
    private void Awake()
    {
    }


    private void FixedUpdate()
    {
        //Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector3 viewPortPoint = Camera.main.WorldToViewportPoint(this.transform.position);

        if (viewPortPoint.x >= 1.0f)
        {
            var p = this.transform.position;
            var q = new Vector3(0.0f, viewPortPoint.y, viewPortPoint.z);
            var r = Camera.main.ViewportToWorldPoint(q);
            var s = new Vector3(r.x, p.y, p.z);
            this.transform.position = s;
        }
    }
}
