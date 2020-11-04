using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour

{
    public Rigidbody2D rb;
    public float ballSpeed;
    private AudioSource audioData;

    //Start is called once on game start
    private void Start()
    {
        StartCoroutine(LaunchWait());
    }

    private IEnumerator LaunchWait()
    {
        //Wait two seconds before Launch function
        yield return new WaitForSecondsRealtime(2);
        Launch();
    }

    private void Launch()
    {
        int randomNumber = Random.Range(0, 2);

        if (randomNumber <= 0.5)
        {
            rb.AddForce(new Vector2(130, ballSpeed));
        }
        else
        {
            rb.AddForce(new Vector2(-130, -ballSpeed));
        }
    }

    private void OnCollisionEnter2D(Collision2D colInfo)
    {
        if (colInfo.collider.tag == "Player")
        {
            Vector3 rbVelocity = rb.velocity;
            rbVelocity.x = rb.velocity.x * (Random.Range(0.7f, 1.1f)) + colInfo.collider.GetComponent<Player>().moveDirection / 2;
            rb.velocity = rbVelocity;
            audioData = GetComponent<AudioSource>();
            audioData.pitch = Random.Range(0.85f, 1.15f);
            audioData.Play();
        }
    }

    public void ResetBall()
    {
        //Reset ball position and velocity to 0
        rb.velocity = new Vector2(0, 0);
        rb.position = new Vector2(0, 0);

        //Start ball yeet wait function
        StartCoroutine(LaunchWait());
    }

    private void FixedUpdate()
    {
        //Debug.Log(rb.velocity);
    }
}