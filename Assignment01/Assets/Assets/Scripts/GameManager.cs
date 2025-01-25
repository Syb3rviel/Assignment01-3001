using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject target;
    public GameObject seeker;
    float maxSpeed = 10.0f;
    float turnSpeed = 300f;
    float maxAcceleration = 5.0f;

    private Vector3 currentVelocity = Vector3.zero;
    void Update()
    {
        //this is where all of the calls will happen depending on what happens
        Vector3 steer = Steering.SeekBehaviour(seeker.transform.position, target.transform.position, ref currentVelocity, maxSpeed, maxAcceleration);
        seeker.transform.position += steer * Time.deltaTime;

        //making sure we turn the right way
        //fix this 
        Steering.RotateTowardsMovePath(seeker.transform, steer, turnSpeed);
    }
}
