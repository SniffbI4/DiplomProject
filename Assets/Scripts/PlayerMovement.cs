using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void Move(float x, float y, Vector3 lookAt)
    {
        Vector3 moveDirection = new Vector3(x, 0, y);
        moveDirection *= speed;
        
        characterController.Move(moveDirection*Time.deltaTime);

        Vector2 dir = new Vector2(lookAt.x - Screen.width / 2, lookAt.y - Screen.height / 2);
        dir = dir.normalized;

        Vector3 direction = new Vector3(transform.position.x + dir.x, 0, transform.position.z + dir.y);

        transform.LookAt(direction, Vector3.up);
    }
}
