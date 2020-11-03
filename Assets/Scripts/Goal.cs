using UnityEngine;

public class Goal : MonoBehaviour
{
    private AudioSource audioData;

    private void OnTriggerEnter2D(Collider2D goalCollision)
    {
        //On collision if the item is "Ball" pass on the collided item's name to GameManager's Score function
        if (goalCollision.name == "Ball")
        {
            GameManager.Score(transform.name);

            goalCollision.gameObject.SendMessage("ResetBall");

            audioData = GetComponent<AudioSource>();
            audioData.Play();
        }
    }
}