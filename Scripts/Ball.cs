//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.Mathematics;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // config parameters
    [SerializeField] Paddle paddle;

    [SerializeField] float xPush = 2f;

    [SerializeField] float yPush = 15f;

    [SerializeField] AudioClip[] ballSounds;

    [SerializeField] float randomFactor = 0.2f;

    // state
    Vector2 paddleToBallVector;

    bool hasStarted = false;

    // Cached component references
    AudioSource myAudioSource;

    Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    void Start()
    {

        paddleToBallVector = transform.position - paddle.transform.position;

        myAudioSource = GetComponent<AudioSource>();

        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();

            LaunchOnMouseClick();
        }

    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0)) // 0 represents the left mouse button
        {
            hasStarted = true;

            //GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);

            myRigidBody2D.velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);

        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));

        if (hasStarted)
        {
            //GetComponent<AudioSource>().Play();

            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];

            //GetComponent<AudioSource>().PlayOneShot(clip);    //Play it whole and dont let it be interrupted

            myAudioSource.PlayOneShot(clip);

            myRigidBody2D.velocity += velocityTweak;
        }
    }
}
