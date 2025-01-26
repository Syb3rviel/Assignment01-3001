using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject target;
    public GameObject seeker;
    public GameObject obstacle;
    float maxSpeed = 5.0f;
    float turnSpeed = 150f;
    float maxAcceleration = 5.0f;
    float slowingDistance = 3f;
    float obstacleRadius = 2.4f;
    //float fleeStrength = 5f;

    private Vector3 currentVelocity = Vector3.zero;
    void Update()
    {
        //when we try to do avoid we end up in a cycle where we just jitter on the spot because we are calling flee and arrive over and over again
        //aka - we are not going around the radius but instead just going back and forth


        Vector3 steer = Steering.AvoidBehaviour(seeker.transform.position, target.transform.position, obstacle.transform.position, ref currentVelocity, maxSpeed,
            maxAcceleration, obstacleRadius, slowingDistance);
        seeker.transform.position += currentVelocity * Time.deltaTime;
        //Debug.Log(currentVelocity);
        Steering.RotateTowardsMovePath(seeker.transform, steer, turnSpeed);
    }
}
