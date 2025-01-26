using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject target;
    public GameObject seeker;
    public GameObject obstacle;
    float maxSpeed = 10.0f;
    float turnSpeed = 200f;
    float maxAcceleration = 5.0f;
    float slowingDistance = 2.0f;
    float obstacleRadius = 3.0f;
    float fleeStrength = 5f;

    private Vector3 currentVelocity = Vector3.zero;
    void Update()
    {
        float distanceToObstacle = Vector3.Distance(seeker.transform.position, obstacle.transform.position);

        if (distanceToObstacle < obstacleRadius)
        {

            Vector3 steer = Steering.AvoidBehaviour(seeker.transform.position, obstacle.transform.position, target.transform.position, 
                obstacleRadius, fleeStrength, distanceToObstacle, maxSpeed);
            seeker.transform.position += steer * Time.deltaTime;
            Steering.RotateTowardsMovePath(seeker.transform, steer, turnSpeed);
            Debug.Log("we are fleeing");
        }
        else
        {
            //we call arrive
            Vector3 steer = Steering.ArriveBehaviour(seeker.transform.position, target.transform.position, ref currentVelocity, maxSpeed, maxAcceleration, slowingDistance);
            seeker.transform.position += steer * Time.deltaTime;
            //Steering.RotateTowardsMovePath(seeker.transform, steer, turnSpeed);
            Debug.Log("We are seeking");
        }

    }
}
