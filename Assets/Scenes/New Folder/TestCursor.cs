using UnityEngine;

public class TestCursor : MonoBehaviour
{
    //public Sprite cursorTexture;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        Cursor.visible = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }
    }
}
