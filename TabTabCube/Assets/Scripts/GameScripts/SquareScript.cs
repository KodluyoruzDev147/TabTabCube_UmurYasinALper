using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquareScript : MonoBehaviour
{
    public Text ScoreText;
    public Animator animator;
    public float Vel = 3500;

    public float m_Thrust = 20f;

    public delegate void PointAction();
    public static event PointAction onPoint;

    public delegate void SpeedAction();
    public static event SpeedAction onSpeedUp;
    public static event SpeedAction onSpeedDown;

    private Rigidbody2D rigidBody;

    private bool playerIsGrounded = true;
    private bool playerFast = true;

    private System.Diagnostics.Stopwatch TabTimeWatch;
    private System.Diagnostics.Stopwatch reverseGravityWatch;
    private double tabHoldTime = 100;

    private int pointCount = 0;
    private float horizantalVel;
    private float absoluteHorizantalVel = 10.8f;
    private float gravity = -25.8f;
    private bool reverseGravity = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        //Physics2D.gravity = (new Vector2(0, 5f * gravity));
        Physics2D.gravity = (new Vector2(0, 0));
        horizantalVel = absoluteHorizantalVel;
        rigidBody.velocity = new Vector2(horizantalVel, gravity);
        TabTimeWatch = new System.Diagnostics.Stopwatch();
        reverseGravityWatch = new System.Diagnostics.Stopwatch();
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        if (reverseGravity)
        {
            rigidBody.transform.Rotate(new Vector3(1, 0, 0), 180);
            reverseGravity = false;
        }

        if (playerIsGrounded && playerFast && TabTimeWatch.Elapsed.TotalMilliseconds >= tabHoldTime)
        {
            Debug.Log("Faster Boii");
            onSpeedUp?.Invoke();
        }
        else
        {
            onSpeedDown?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReverseGravity();
        }

        rigidBody.velocity = new Vector2(horizantalVel, gravity);

     

    }

    public void ReverseGravity()
    {
        animator.SetBool("isGround", false);
        gravity = -gravity;
        reverseGravity = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            playerIsGrounded = true;
            animator.SetBool("isGround", true);
        }
        else if (collision.gameObject.tag == "Obstacle")
        {
            SpeedDown();
        }
        else if (collision.gameObject.tag == "UpDownCollider")
        {
            SpeedUp();
            animator.SetBool("isGround", true);
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            playerIsGrounded = false;
        else if (collision.gameObject.tag == "Obstacle")
            SpeedUp();
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PointLimit")
            PointLimit();
        else if (collider.gameObject.tag == "SpeedUpLimit")
            SpeedUp();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "SpeedUpLimit")
            SpeedDown();

    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "SpeedUpLimit")
            SpeedDown();
    }

    private void PointLimit()
    {
        onPoint?.Invoke();
        pointCount++;
        UpdateScoreText();
        //Debug.Log(" PointCount = " + PointCount);
    }

    private void SpeedUp()
    {
        horizantalVel = absoluteHorizantalVel;
    }

    private void SpeedDown()
    {
        horizantalVel = 0;
    }

    private void UpdateScoreText()
    {
        ScoreText.text = pointCount.ToString();
    }

}
