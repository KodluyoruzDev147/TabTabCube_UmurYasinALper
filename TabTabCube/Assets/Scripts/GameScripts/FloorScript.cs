using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{

    public GameObject Obstacle;
    public float obstacleY; 

    private GameObject CloneObstacle;
    private bool isInitObstacle = false;
    private Rigidbody2D ObstacleRigidBody;

    void Start()
    {
    }

    void Update()
    {
        if (isInitObstacle == false)
        {
            CloneObstacle = Instantiate(Obstacle, new Vector3(9.0f, obstacleY, 0), Quaternion.identity);
            ObstacleRigidBody = CloneObstacle.GetComponent<Rigidbody2D>();
            isInitObstacle = true;
        }
        else
        {
            ObstacleRigidBody.velocity  = new Vector2(-15.8f, 0f);

            if (CloneObstacle.transform.position.x <= -9.0f)
            {
                Destroy(CloneObstacle);
                isInitObstacle = false;
            }

        }
    }

}
