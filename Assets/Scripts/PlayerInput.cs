using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerMovement), typeof(PlayerShoot))]
public class PlayerInput : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerShoot playerShoot;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            playerShoot.Shot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            playerShoot.ReloadWeapon();
        }

        float x = Input.GetAxis(GameData.HORIZONTAL_AXIS);
        float y = Input.GetAxis(GameData.VERTICAL_AXIS);

        Vector3 mousePosition = Input.mousePosition;
        playerMovement.Move(x, y, mousePosition);
    }
}
