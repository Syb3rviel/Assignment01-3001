using Unity.VisualScripting;
using UnityEngine;

public class Steering : MonoBehaviour 
{
    //all references will be made in the gamemanager to both connect the seeker and target together and call the below fucntions when needed

    public static Vector3 SeekBehaviour(Vector2 seeker, Vector2 target, float moveSpeed)
    {
        Vector2 desiredVelocity = target - seeker;
        float distance = desiredVelocity.magnitude;
        desiredVelocity = desiredVelocity.normalized * moveSpeed;
        Vector2 steering = desiredVelocity - seeker;
        return steering;
    }

    //public static Vector3 FleeBehaviour(Vector2 seeker, Vector2 target)
    //{
    //    return Vector3.zero;
    //}

    //public static Vector3 ArriveBehaviour()
    //{
    //    return Vector3.zero;
    //}

    //public static Vector3 AvoidBehaviour()
    //{
    //    return Vector3.zero;
    //}
}
