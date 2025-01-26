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
    float avoidDistance = 2.0f;

    private Vector3 currentVelocity = Vector3.zero;
    void Update()
    {
        //we have properly checked that seek, flee, and arrive work

        Vector3 steer = Steering.AvoidBehaviour(seeker.transform.position, obstacle.transform.position, ref currentVelocity, maxSpeed, maxAcceleration, avoidDistance);
        seeker.transform.position += steer * Time.deltaTime;
        //Steering.RotateTowardsMovePath(seeker.transform, steer, turnSpeed);
    }
}
