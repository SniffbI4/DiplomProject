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
        #region Movement
        Vector3 moveDirection = new Vector3(x, 0, y);
        moveDirection *= speed;

        //characterController.Move(moveDirection*Time.deltaTime);
        characterController.Move(Vector3.ProjectOnPlane(moveDirection, Vector3.up) * Time.deltaTime - new Vector3(0, 9.8f, 0) * Time.deltaTime);
        #endregion

        //Vector2 dir = new Vector2(lookAt.x - Screen.width / 2, lookAt.y - Screen.height / 2);
        //dir = dir.normalized;

        //Vector3 direction = new Vector3(transform.position.x + dir.x, transform.position.y, transform.position.z + dir.y);

        //transform.LookAt(direction, Vector3.up);

        #region Rotation
        Ray ray = Camera.main.ScreenPointToRay(lookAt);
        RaycastHit hit;
        Vector3 lookDirection;

        if (Physics.Raycast(ray, out hit, 100))
        {
            lookDirection = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(lookDirection, Vector3.up);
        }
        #endregion
    }
}
