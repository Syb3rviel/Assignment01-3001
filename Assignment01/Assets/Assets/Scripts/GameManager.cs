using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject target;
    public GameObject seeker;
    float maxSpeed = 10.0f;
    void Update()
    {
        //this is where all of the calls will happen depending on what happens
        Vector3 steer = Steering.SeekBehaviour(seeker.transform.position, target.transform.position, maxSpeed);
        seeker.transform.position += steer * Time.deltaTime;
    }
}
