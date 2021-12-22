using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private float absoluteObstacleSpeed = -10.8f;
    private float obstacleSpeed = -10.8f;

    public delegate void KillAction();
    public static event KillAction onKilled;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        SquareScript.onSpeedUp += SquareScript_onSpeedUp;
        SquareScript.onSpeedDown += SquareScript_onSpeedDown;
        SquareScript.onPoint += SquareScript_onPoint;
    }

    private void SquareScript_onPoint()
    {
        absoluteObstacleSpeed = absoluteObstacleSpeed * 1.1f;
        obstacleSpeed = absoluteObstacleSpeed;
    }

    private void SquareScript_onSpeedDown()
    {
        obstacleSpeed = absoluteObstacleSpeed;
    }

    private void SquareScript_onSpeedUp()
    {
        obstacleSpeed = absoluteObstacleSpeed * 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector2(obstacleSpeed, 0);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "KillLimit")
            KillObstacle();
    }

    private void KillObstacle()
    {
        Destroy(this.gameObject);
        onKilled?.Invoke();
    }

}
