using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 speed;

    public KeyCode moveLeft;
    public KeyCode moveRight;

    public float moveDirection;

    // Update is called once per frame
    private void FixedUpdate()
    {
        float minX = Camera.main.ScreenToWorldPoint(new Vector3(0 + 55, 0)).x;
        float maxX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 55, 0)).x;

        if (Input.GetKey(moveRight))
        {
            rb.position = new Vector2(Mathf.Clamp(rb.position.x, minX, maxX), rb.position.y);
            rb.MovePosition(rb.position + speed);
            moveDirection = 5f;
        }
        else if (Input.GetKey(moveLeft))
        {
            rb.position = new Vector2(Mathf.Clamp(rb.position.x, minX, maxX), rb.position.y);
            rb.MovePosition(rb.position - speed);
            moveDirection = -5f;
        }
        else
        {
            moveDirection = 0f;
        }
    }
}