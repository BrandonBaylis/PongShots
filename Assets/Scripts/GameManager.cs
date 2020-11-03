using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;

    public BoxCollider2D wallLeft;
    public BoxCollider2D wallRight;
    public BoxCollider2D goalBottom;
    public BoxCollider2D goalTop;

    public Transform Player1;
    public Transform Player2;

    public GUISkin scoreSkin;

    //Declare score variables and set them to 0
    public static int player1Score = 0;

    public static int player2Score = 0;

    // Update is called every frame
    private void Start()
    {
        ScreenSize();
    }

    private void ScreenSize()
    {
        //Matches the wall's colliders to the camera's view and moves them just outside view
        wallLeft.size = new Vector2(1f, mainCamera.ScreenToWorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
        wallLeft.offset = new Vector2(mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x - 0.5f, 0f);

        wallRight.size = new Vector2(1f, mainCamera.ScreenToWorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
        wallRight.offset = new Vector2(mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x + 0.5f, 0f);

        goalTop.size = new Vector2(mainCamera.ScreenToWorldPoint(new Vector3(Screen.width * 2f, 0f, 0f)).x, 1f);
        goalTop.offset = new Vector2(0f, mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).y - 0.5f);

        goalBottom.size = new Vector2(mainCamera.ScreenToWorldPoint(new Vector3(Screen.width * 2f, 0f, 0f)).x, 1f);
        goalBottom.offset = new Vector2(0f, mainCamera.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y + 0.5f);

        //Moves the players to starting positions
        Player1.transform.position = new Vector3(Player1.transform.position.x, mainCamera.ScreenToWorldPoint(new Vector3(0f, 75f, 0f)).y, 0f);
        Player2.transform.position = new Vector3(Player2.transform.position.x, mainCamera.ScreenToWorldPoint(new Vector3(0f, Screen.height - 75f, 0f)).y, 0f);
    }

    public static void Score(string goalName)
    {
        //If top goal Player 1 has scored
        if (goalName == "GoalTop")
        {
            player1Score++;
        }
        //Else if bottom goal Player 2 has scored
        else if (goalName == "GoalBottom")
        {
            player2Score++;
        }
    }

    private void OnGUI()
    {
        GUI.skin = scoreSkin;

        GUI.Label(new Rect(25, Screen.height / 2 - 100 - 12, 100, 100), "" + player1Score);
        GUI.Label(new Rect(25, Screen.height / 2 + 100 - 12, 100, 100), "" + player2Score);
    }
}