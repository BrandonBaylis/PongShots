using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public Camera mainCamera;
    private int playerNum;

    private readonly float speedX = 0.1f;    //x-axis movement "speed" amount
    private readonly float speedY = 0.04f;    //y-axis movement "speed" amount

    public float movementX;
    public float movementY;

    private float minY;     //Min y-axis position (closer to net)
    private float maxY;     //Max y-axis position (further from net)

    private void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        //Assigns player number primarily for control schemes
        if (gameObject.name == "Player1")
        {
            playerNum = 1;
            //Move Player2 to starting position, bottom of screen
            transform.position = new Vector2(0f, mainCamera.ScreenToWorldPoint(new Vector2(0f, 75f)).y);
        }
        else if (gameObject.name == "Player2")
        {
            playerNum = 2;
            //Move Player2 to starting position, top of screen
            transform.position = new Vector2(0f, mainCamera.ScreenToWorldPoint(new Vector2(0f, Screen.height - 75f)).y);
        }
        else
        {
            playerNum = 0;
        }
    }

    //Clamp y-axis movement between two points
    private void ClampPositionY(ref float yPosition)
    {
        //Min & max dependant on Player 1 (bottom of screen) and Player 2 (top of screen)
        if (gameObject.name == "Player1")
        {
            //Player 1 clamps
            minY = mainCamera.ScreenToWorldPoint(new Vector2(0f, 75f)).y;
            maxY = minY * 1.12f;
            //Clamp reference float between max/min
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
        //Movement speed equals the Player's input value multiplied by speed
        movementX = Input.GetAxisRaw("Horizontal" + playerNum) * speedX;
        if (Input.GetAxisRaw("Vertical" + playerNum) != 0f)
        {
            movementY = Input.GetAxisRaw("Vertical" + playerNum) * speedY;
            //Attempt at exponential pullback
            //movementY = Input.GetAxisRaw("Vertical" + playerNum) * Mathf.Lerp(speedY, speedY + 0.05f, 0.001f);
        }
        else
        {
            movementY = rb2D.position.y * (-speedY * 0.8f);
        }

        //Target position equals current position + movement amount
        float targetXPosition = rb2D.position.x + movementX;
        float targetYPosition = rb2D.position.y + movementY;

        //Clamp x-axis movement between screen width
        ClampPositionX(ref targetXPosition);
        //Clamp y-axis movement between starting y and pull-back stop
        ClampPositionY(ref targetYPosition);

        rb2D.MovePosition(new Vector2(targetXPosition, targetYPosition));
    }
}