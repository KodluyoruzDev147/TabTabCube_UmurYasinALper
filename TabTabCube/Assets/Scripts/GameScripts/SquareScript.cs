using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquareScript : MonoBehaviour
{
    public Text ScoreText;
    public float Vel = 3500;
    private Rigidbody2D rigidbody;

    public float m_Thrust = 20f;

    private bool playerIsGrounded = true;
    private bool playerFast = true;

    private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
    private double tabHoldTime = 100;

    public delegate void PointAction();
    public static event PointAction onPoint;

    public delegate void SpeedAction();
    public static event SpeedAction onSpeedUp;
    public static event SpeedAction onSpeedDown;

    private int PointCount = 0;
    private float horizantalVel;
    private float absoluteHorizantalVel = 10.8f;
    private float gravity = -25.8f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        //Physics2D.gravity = (new Vector2(0, 5f * gravity));
        Physics2D.gravity = (new Vector2(0, 0));
        horizantalVel = absoluteHorizantalVel;
        rigidbody.velocity = new Vector2(horizantalVel, gravity);
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(playerIsGrounded && playerFast && stopwatch.Elapsed.TotalMilliseconds >= tabHoldTime)
        {
            Debug.Log("Fastest Boii");
            onSpeedUp?.Invoke();
        }
        else
        {
            //Debug.Log("Fuck Boii");
            onSpeedDown?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReverseGravity();
        }

        rigidbody.velocity = new Vector2(horizantalVel, gravity);
    }

    public void ReverseGravity()
    {
        gravity = -gravity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            playerIsGrounded = true;
        else if (collision.gameObject.tag == "Obstacle")
            SpeedDown();
        else if (collision.gameObject.tag == "UpDownCollider")
            SpeedUp();
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
        PointCount++;
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
        ScoreText.text = PointCount.ToString();
    }

}
