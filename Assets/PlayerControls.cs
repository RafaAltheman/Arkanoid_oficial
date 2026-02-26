using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public BoxCollider2D leftWall;
    public BoxCollider2D rightWall;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        float halfWidth = GetComponent<Collider2D>().bounds.extents.x;

        float minX = leftWall.bounds.max.x + halfWidth;
        float maxX = rightWall.bounds.min.x - halfWidth;

        var pos = transform.position;
        pos.x = mousePos.x;

        // Limite pelas paredes
        if (pos.x < minX)
            pos.x = minX;

        if (pos.x > maxX)
            pos.x = maxX;

        transform.position = pos;
    }
}