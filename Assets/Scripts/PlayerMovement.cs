using UnityEngine;

[RequireComponent (typeof(CharacterController), typeof(ActorView))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform weaponDirection;
    [SerializeField] float angleOffset;

    private ActorView actorView;
    private CharacterController characterController;

    private Vector3 animDirection;
    private Vector3 lookDirection;
    private bool canMove = true;
    private bool isRolling = false;

    private Vector3 weaponAim;

    private void Start()
    {
        actorView = GetComponent<ActorView>();
        characterController = GetComponent<CharacterController>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x">Input горизонтальной оси</param>
    /// <param name="y">Input вертикальной оси</param>
    /// <param name="lookAt">Input.mousePosition</param>
    public void Move(float x, float y, Vector3 lookAt)
    {
        if (!canMove)
            return;

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
            transform.Rotate(0, angleOffset, 0);
        }
        #endregion

        #region AnimatorCalculations
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
        #endregion

        #region Test
        ////float distOM = Vector3.Distance(transform.position, lookDirection);
        ////Vector3 weaponPosOnPlane = weaponDirection.position;
        ////weaponPosOnPlane.y = transform.position.y;
        ////float distOW = Vector3.Distance(transform.position, weaponPosOnPlane);

        //////float distWA = Mathf.Sqrt((Mathf.Pow(distOM,2))-(Mathf.Pow(distOW, 2)));
        ////float angleBetweenWeaponAndCharacter = Vector3.SignedAngle(transform.forward - transform.position, weaponDirection.forward - weaponDirection.position, Vector3.up);
        ////Debug.Log(angleBetweenWeaponAndCharacter);
        ////float distOO1 = distOW * Mathf.Sin(angleBetweenWeaponAndCharacter * Mathf.Deg2Rad);

        ////float distWO1 = Mathf.Sqrt((Mathf.Pow(distOW, 2)) - (Mathf.Pow(distOO1, 2)));
        ////float distAO1 = Mathf.Sqrt((Mathf.Pow(distOM, 2)) - (Mathf.Pow(distOO1, 2)));

        ////float distAW = distWO1 + distAO1;
        
        ////weaponAim = weaponPosOnPlane + weaponDirection.forward * distAW;
        ////weaponAim.y = transform.position.y;

        ////Debug.DrawLine(transform.position, lookDirection, Color.red);
        ////Debug.DrawLine(weaponPosOnPlane, weaponAim, Color.black);
        ////Debug.DrawRay(transform.position, transform.forward * distAW, Color.blue);

        ////float ang = Vector3.SignedAngle(lookDirection - transform.position, weaponAim-transform.position, Vector3.up);

        //////Debug.Log($"weapAimPos = {weaponAim}" +
        //////    $"\nmousePos = {lookDirection}");
        //////Debug.Log($"angle(Vector3) = {ang}" +
        //////    $"\nnewAngle = {newAng}");

        //////transform.LookAt(weaponAim);
        //////transform.Rotate(0, newAng, 0);
        //////transform.LookAt(weaponAim, transform.up);

        #endregion
    }

    public void Roll (float x, float y)
    {
        if (isRolling)
            return;

        isRolling = true;
        canMove = false;
        Vector3 direction = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + y);
        transform.LookAt(direction);
        actorView.PlayRoll();
    }

    public void CanMove ()
    {
        canMove = true;
        isRolling = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(lookDirection, 0.2f);
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(weaponAim, 0.1f);
    }
}
