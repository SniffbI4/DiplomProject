using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionDebug : MonoBehaviour
{
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        Debug.Log(difference);
    }
}
