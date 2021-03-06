using UnityEngine;

[RequireComponent (typeof(CharacterController), typeof(ActorView))]
public class NewPlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;

    private ActorView actorView;
    private CharacterController characterController;

    Vector3 animDirection;
    Vector3 lookDirection;

    private void Start()
    {
        actorView = GetComponent<ActorView>();
        characterController = GetComponent<CharacterController>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x">Input ?????????????? ???</param>
    /// <param name="y">Input ???????????? ???</param>
    /// <param name="lookAt">Input.mousePosition</param>
    public void Move(float x, float y, Vector3 lookAt)
    {
        #region Movement
        Vector3 moveDirection = new Vector3(x, transform.position.y, y);
        moveDirection *= speed;

        characterController.Move(Vector3.ProjectOnPlane(moveDirection, Vector3.up) * Time.deltaTime - new Vector3(0, 9.8f, 0) * Time.deltaTime);
        #endregion

        #region Rotation
        Ray ray = Camera.main.ScreenPointToRay(lookAt);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            animDirection = (hit.point - transform.position).normalized;
            lookDirection = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(lookDirection, Vector3.up);
        }
        #endregion

        Vector3 dirToCurcor = lookDirection - transform.position;
        Vector3 allignedDir = new Vector3(dirToCurcor.x, 0, dirToCurcor.z);

        float angle = Vector3.Angle(Vector3.forward, allignedDir);
        Vector3 cross = Vector3.Cross(Vector3.forward, allignedDir);

        if (cross.y < 0)
        {
            angle = -angle;
        }

        float cos = Mathf.Cos(angle * Mathf.Deg2Rad);
        float sin = Mathf.Sin(angle * Mathf.Deg2Rad);
        float Ax = cos * x - sin * y;
        float Ay = cos * y + sin * x;

        actorView.PlayWalk(Ax, Ay);
    }
}
