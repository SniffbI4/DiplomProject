using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof (PlayerMovement), typeof(PlayerShoot))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private UnityEvent testAction;

    private PlayerMovement playerMovement;
    private NewPlayerMovement newPlayerMovement;
    private PlayerShoot playerShoot;
    private bool isAiming = false;

    private void Awake()
    {
        newPlayerMovement = GetComponent<NewPlayerMovement>();
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

        //������
        if (Input.GetMouseButtonDown(1))
            isAiming = true;
        if (Input.GetMouseButtonUp(1))
            isAiming = false;

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
        //newPlayerMovement.Move(x, y, mousePosition);
        playerMovement.Move(x, y, mousePosition);

        //�������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerMovement.Roll(x, y);
        }

        //������������
        if (Input.GetKeyDown(KeyCode.N))
        {
            EnemySpawner.instance.SpawnEnemy();
        }
    }
}
