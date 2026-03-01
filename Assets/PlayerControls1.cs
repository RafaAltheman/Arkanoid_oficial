using UnityEngine;

public class PlayerControls1 : MonoBehaviour
{
    public BoxCollider2D leftWall;
    public BoxCollider2D rightWall;

    private Rigidbody2D rb;
    private float halfWidth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        halfWidth = GetComponent<Collider2D>().bounds.extents.x;
    }

    void FixedUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float minX = leftWall.bounds.max.x + halfWidth;
        float maxX = rightWall.bounds.min.x - halfWidth;

        float targetX = Mathf.Clamp(mousePos.x, minX, maxX);

        // Move usando física (suave e sem travar)
        rb.MovePosition(new Vector2(targetX, rb.position.y));
    }
}