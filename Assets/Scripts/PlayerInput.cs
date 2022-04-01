using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof (PlayerMovement), typeof(PlayerShoot))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private UnityEvent testAction;

    private PlayerMovement playerMovement;
    private PlayerShoot playerShoot;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();
    }

    private void Update()
    {
        //��������
        if (Input.GetMouseButton(0))
        {
            playerShoot.Fire();
        }

        //�����������
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerShoot.ReloadWeapon();
        }

        //����� ������
        float mouseWheel = Input.GetAxis(GameData.MOUSE_SCROLL);
        if (mouseWheel >= 0.1f || mouseWheel <= -0.1f)
            playerShoot.ChangeWeapon();

        //����������� ����������� ���������
        float x = Input.GetAxis(GameData.HORIZONTAL_AXIS);
        float y = Input.GetAxis(GameData.VERTICAL_AXIS);

        //����������� �������
        Vector3 mousePosition = Input.mousePosition;

        //�����������
        playerMovement.Move(x, y, mousePosition);

        //�������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerMovement.Roll(x, y);
        }

    }
}
