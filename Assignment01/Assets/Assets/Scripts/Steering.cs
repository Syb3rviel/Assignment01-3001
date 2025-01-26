using Unity.VisualScripting;
using UnityEngine;

public class Steering : MonoBehaviour 
{
    //all references will be made in the gamemanager to both connect the seeker and target together and call the below fucntions when needed

    public static Vector3 SeekBehaviour(Vector3 seeker, Vector3 target, ref Vector3 currentVelocity, float moveSpeed, float acceleration)
    {
        Vector3 desiredVelocity = target - seeker;
        float distance = desiredVelocity.magnitude;
        desiredVelocity = desiredVelocity.normalized * moveSpeed;
        Vector3 steering = desiredVelocity - currentVelocity;

        steering = Vector2.ClampMagnitude(steering, acceleration);
        currentVelocity += steering * Time.deltaTime;
        currentVelocity = Vector3.ClampMagnitude(currentVelocity, moveSpeed);
        seeker += currentVelocity * Time.deltaTime;
        
        return steering;
    }
    public static void RotateTowardsMovePath(Transform seeker, Vector3 velocity, float turnSpeed)
    {
        if(velocity.magnitude > 0.01f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            //we only need rotation on the z axis because it is 2D so this line is unneccesary
            //Quaternion targetRotation = Quaternion.LookRotation(velocity);

            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
            seeker.rotation = Quaternion.RotateTowards(seeker.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    public static Vector3 FleeBehaviour(Vector3 seeker, Vector3 target, ref Vector3 currentVelocity, float moveSpeed, float acceleration)
    {
        Vector3 desiredVelocity = target - seeker;
        float distance = desiredVelocity.magnitude;
        desiredVelocity = desiredVelocity.normalized * moveSpeed;
        Vector3 steering = currentVelocity - desiredVelocity;

        steering = Vector2.ClampMagnitude(steering, acceleration);
        currentVelocity += steering * Time.deltaTime;
        currentVelocity = Vector3.ClampMagnitude(currentVelocity, moveSpeed);
        seeker += currentVelocity * Time.deltaTime;

        return steering;
    }
    public static Vector3 ArriveBehaviour(Vector3 seeker, Vector3 target, ref Vector3 currentVelocity, float moveSpeed, float acceleration, float slowDistance)
    {
        Vector3 desiredVelocity = target - seeker;
        float distance = desiredVelocity.magnitude;

        if (distance < slowDistance)
        {
            desiredVelocity = desiredVelocity.normalized * moveSpeed * (distance / slowDistance);
        }
        else
        {
            desiredVelocity = desiredVelocity.normalized * moveSpeed;
        }
        Vector3 steering = desiredVelocity - currentVelocity;

        steering = Vector2.ClampMagnitude(steering, acceleration);
        currentVelocity += steering * Time.deltaTime;
        currentVelocity = Vector3.ClampMagnitude(currentVelocity, moveSpeed);
        seeker += currentVelocity * Time.deltaTime;

        return steering;
    }

    public static Vector3 AvoidBehaviour(Vector3 seeker, Vector3 target, Vector3 obstacle, ref Vector3 currentVelocity, float moveSpeed, float acceleration,
        float obstacleRadius, float slowDown)
    {
        Vector3 directionFromObstacle = (seeker - obstacle).normalized;
        float distanceToObstacle = Vector3.Distance(seeker, obstacle);
        float slowDownDistance = Mathf.Clamp01(distanceToObstacle / slowDown);

        if (distanceToObstacle < obstacleRadius)
        {
            //Debug.Log("Avoiding obstacle...");

            // Flee force to move away from the obstacle
            Vector3 fleeForce = directionFromObstacle * moveSpeed;

            // Tangential force to move around the obstacle
            Vector3 tangentialForce = Vector3.Cross(directionFromObstacle, Vector3.forward).normalized * moveSpeed * 0.2f;

            // Combine flee and tangential forces
            Vector3 avoidanceForce = fleeForce + tangentialForce;

            // Steering logic
            Vector3 steering = avoidanceForce - currentVelocity;

            // Clamp the steering force and apply it to velocity
            steering = Vector3.ClampMagnitude(steering, acceleration);
            currentVelocity += steering * Time.deltaTime;
            currentVelocity = Vector3.ClampMagnitude(currentVelocity, moveSpeed).normalized;

            // Return the final steering force
            return currentVelocity;
        }
        else
        {
            Vector3 desiredVelocity = target - seeker;
            float distance = desiredVelocity.magnitude;

            if (distance < slowDown)
            {
                desiredVelocity = desiredVelocity.normalized * moveSpeed * (distance / slowDown);
            }
            else
            {
                desiredVelocity = desiredVelocity.normalized * moveSpeed;
            }
            Vector3 steering = desiredVelocity - currentVelocity;

            steering = Vector2.ClampMagnitude(steering, acceleration);
            currentVelocity += steering * Time.deltaTime;
            currentVelocity = Vector3.ClampMagnitude(currentVelocity, moveSpeed);
            seeker += currentVelocity * Time.deltaTime;

            return steering;
        }

        //return Vector3.zero;


        //Debug.Log("we are fleeing");

        //Vector3 desiredVelocity = obstacle - seeker;
        //float distance = desiredVelocity.magnitude;
        //desiredVelocity = desiredVelocity.normalized * moveSpeed;

        ////stands for tangential force so that seeker actually goes around teh target instead of jittering in spot forever
        //Vector3 tanForce = Vector3.Cross(directionFromObstacle, Vector3.up).normalized * moveSpeed * 0.5f;
        //Vector3 combinedForce = desiredVelocity + tanForce;

        //Vector3 steering = currentVelocity - combinedForce;

        //steering = Vector2.ClampMagnitude(steering, acceleration);
        //currentVelocity += steering * Time.deltaTime;
        //currentVelocity = Vector3.ClampMagnitude(currentVelocity, moveSpeed);
        //seeker += currentVelocity * Time.deltaTime;
        //return steering;
    }
}
