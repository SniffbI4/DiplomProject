using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private void Update()
    {
        float x = Input.mousePosition.x - Screen.width / 2;
        float y = Input.mousePosition.y - Screen.height / 2;

        Vector2 dir = new Vector2(x,y);
        dir = dir.normalized;

        Vector3 direction = new Vector3(transform.position.x+dir.x, 0, transform.position.z+dir.y);

        transform.LookAt(direction, Vector3.up);
    }
}
