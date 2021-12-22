using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : MonoBehaviour
{
    public GameObject Obstacle;
    public float obstacleYUp = 3.63f;
    public float obstacleYDown = -2.75f;
    public float obstacleYMid = 0.44f;

    private int obstacleCount = 0;
    private bool initObstacle = false;
    private System.Diagnostics.Stopwatch initObstacleStopWatch;

    

    // Start is called before the first frame update
    void Start()
    {
        initObstacleStopWatch = new System.Diagnostics.Stopwatch();
        ObstacleScript.onKilled += KillObstacle;
    }

    void Update()
    {
        if (initObstacle == false)
        {
            GenerateObstacle();
            initObstacle = true;
        }

        if (initObstacleStopWatch.Elapsed.TotalSeconds > 0.35 && obstacleCount < 10)
        {
            initObstacle = false;
        }
    }

    public void GenerateObstacle()
    {
        float obstacleY = obstacleYUp;
        float randVal = Random.value;

        if (randVal < 0.3)
            obstacleY = obstacleYDown;
        else if (randVal < 0.6)
            obstacleY = obstacleYMid;

        Instantiate(Obstacle, new Vector3(9.0f, obstacleY, 0), Quaternion.identity);
        obstacleCount++;
        initObstacleStopWatch.Restart();
    }

    private void KillObstacle()
    {
        obstacleCount--;
    }
}
