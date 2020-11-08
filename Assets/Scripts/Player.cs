using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float speedX = 0.1f;
    private float speedY = 0.1f;

    private int playerNum;

    public Camera mainCamera;

    public float movementX;
    public float movementY;

    private void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        //Assigns player number primarily for control schemes
        if (gameObject.name == "Player1")
        {
            playerNum = 1;
        }
        else if (gameObject.name == "Player2")
        {
            playerNum = 2;
        }
    }

    //Clamp y-axis movement between two points
    private void ClampPositionY(ref float yPosition)
    {
        float minY;
        float maxY;

        //Min & max dependant on Player 1 (bottom of screen) and Player 2 (top of screen)
        if (gameObject.name == "Player1")
        {
            //Player 1 clamps
            minY = mainCamera.ScreenToWorldPoint(new Vector2(0f, 75f)).y;
            maxY = minY * 1.12f;
            yPosition = Mathf.Clamp(yPosition, maxY, minY);
        }
        else
        {
            //Player 2 clamps
            minY = mainCamera.ScreenToWorldPoint(new Vector2(0f, Screen.height - 75f)).y;
            maxY = minY * 1.12f;
            //Clamp reference float between min/max
            yPosition = Mathf.Clamp(yPosition, minY, maxY);
        }
    }

    //Clamp x-axis movement between screen borders
    private void ClampPositionX(ref float xPosition)
    {
        //Min & max between camera borders
        float minX = mainCamera.ScreenToWorldPoint(new Vector2(0 + 50, 0)).x;
        float maxX = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width - 50, 0)).x;

        //Clamp reference float between min/max
        xPosition = Mathf.Clamp(xPosition, minX, maxX);
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        //Movement speed equals the Player's input value * x-axis speed
        movementX = Input.GetAxisRaw("Horizontal" + playerNum) * speedX;

        movementY = Input.GetAxisRaw("Vertical" + playerNum) * speedY;

        //Target position equals current position + movement amount
        float targetXPosition = rb2d.position.x + movementX;

        float targetYPosition = rb2d.position.y + movementY;
        Debug.Log(gameObject.name + " " + targetYPosition);

        //Clamp x-axis movement between screen width
        ClampPositionX(ref targetXPosition);

        ClampPositionY(ref targetYPosition);

        rb2d.MovePosition(new Vector2(targetXPosition, targetYPosition));
    }
}