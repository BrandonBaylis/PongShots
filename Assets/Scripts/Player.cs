using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 speed;

    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode pullBack;

    public float moveDirection;

    //Min and Max player x movement positions
    private float minX;

    private float maxX;

    private float minY;
    private float maxY;

    public void Start()
    {
        minX = Camera.main.ScreenToWorldPoint(new Vector3(0 + 55, 0)).x;
        maxX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 55, 0)).x;
    }

    public void SetClampY(float passedY)
    {
        minY = passedY;
        maxY = passedY * 1.5f;
        Debug.Log(gameObject.name + " " + maxY);
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        //Movement code
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

        if (Input.GetKey(pullBack))
        {
            rb.position = new Vector2(rb.position.x, Mathf.Clamp(rb.position.y, minY, maxY));
            rb.MovePosition(new Vector2(rb.position.x, rb.position.y * 1.01f));
        }
    }
}