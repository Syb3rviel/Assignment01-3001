using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //prefabs to attach and instantiate to
    [SerializeField] GameObject targetPrefab;
    [SerializeField] GameObject seekerPrefab;
    [SerializeField] GameObject obstaclePrefab;

    //basic gameobjects that will attach to prefabs once instantiated
    GameObject target;
    GameObject seeker;
    GameObject obstacle;

    //floats needs for certain speed / radius
    float maxSpeed = 5.0f;
    float turnSpeed = 150f;
    float maxAcceleration = 5.0f;
    float slowingDistance = 2f;
    float obstacleRadius = 2f;
    float middleSpawn = 0.5f;

    bool gameStart = false;
    bool avoid = false;
    int gameState = 0;

    private Vector3 currentVelocity = Vector3.zero;

    private void Awake()
    {
        //dont destroy on load to the gamemanager so it stays
        DontDestroyOnLoad(gameObject);

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0) && gameStart)
        {
            //remove everything instantiated in the scene //aka the prefabs!!!
            DestroyCurrent();
            gameState = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1) && gameStart)
        {
            //doo basic seek
            DestroyCurrent();
            RandomInstantiate();
            gameState = 1;
        }
        else if( Input.GetKeyDown(KeyCode.Alpha2) && gameStart)
        {
            //do basic flee
            //add boundaries
            DestroyCurrent();
            RandomInstantiate();
            gameState = 2;
        }
        else if(Input.GetKeyDown (KeyCode.Alpha3) && gameStart)
        {
            //arrive function
            DestroyCurrent();
            RandomInstantiate();
            gameState = 3;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4) && gameStart)
        {
            //avoid
            DestroyCurrent();
            avoid = true;
            RandomInstantiate();
            gameState = 4;
        }



        if (gameState == 1)
        {
            Vector3 steer = Steering.SeekBehaviour(seeker.transform.position, target.transform.position, ref currentVelocity, maxSpeed, maxAcceleration);
            seeker.transform.position += steer * Time.deltaTime;
            seeker.transform.position = new Vector3(
            Mathf.Clamp(seeker.transform.position.x, -5.69f, 2.52f),
            Mathf.Clamp(seeker.transform.position.y, -4.23f, 2.79f), seeker.transform.position.z);
            Steering.RotateTowardsMovePath(seeker.transform, steer, turnSpeed);
        }
        else if (gameState == 2)
        {
            Vector3 steer = Steering.FleeBehaviour(seeker.transform.position, target.transform.position, ref currentVelocity, maxSpeed, maxAcceleration);
            seeker.transform.position += steer * Time.deltaTime;
            seeker.transform.position = new Vector3(
            Mathf.Clamp(seeker.transform.position.x, -5.69f, 2.52f),
            Mathf.Clamp(seeker.transform.position.y, -4.23f, 2.79f), seeker.transform.position.z);
            Steering.RotateTowardsMovePath(seeker.transform, steer, turnSpeed);
        }
        else if (gameState == 3)
        {
            Vector3 steer = Steering.ArriveBehaviour(seeker.transform.position, target.transform.position, ref currentVelocity, maxSpeed, maxAcceleration, slowingDistance);
            seeker.transform.position += steer * Time.deltaTime;
            seeker.transform.position = new Vector3(
            Mathf.Clamp(seeker.transform.position.x, -5.69f, 2.52f),
            Mathf.Clamp(seeker.transform.position.y, -4.23f, 2.79f), seeker.transform.position.z);
            Steering.RotateTowardsMovePath(seeker.transform, steer, turnSpeed);
        }
        else if (gameState == 4)
        {
            Vector3 steer = Steering.AvoidBehaviour(seeker.transform.position, target.transform.position, obstacle.transform.position,
                ref currentVelocity, maxSpeed, maxAcceleration, obstacleRadius, slowingDistance);
            seeker.transform.position += steer * Time.deltaTime;
            seeker.transform.position = new Vector3(
            Mathf.Clamp(seeker.transform.position.x, -5.69f, 2.52f),
            Mathf.Clamp(seeker.transform.position.y, -4.23f, 2.79f), seeker.transform.position.z);
            Steering.RotateTowardsMovePath(seeker.transform, steer, turnSpeed);
        }

    }

    void RandomInstantiate()
    {
        //this is where we will randomly place them on the board
        Vector3 targetPos = new Vector3(Random.Range(-5.69f, 2.53f), Random.Range(-4.23f, 2.8f), 0f);
        Vector3 seekerPos = new Vector3(Random.Range(-5.69f, 2.53f), Random.Range(-4.23f, 2.8f), 0f);
        //Debug.Log(targetPos + seekerPos + obstaclePos);

        while (targetPos == seekerPos)
        {
            //reroll
            targetPos = new Vector3(Random.Range(-5.69f, 2.53f), Random.Range(-4.23f, 2.8f), 0f);
            seekerPos = new Vector3(Random.Range(-5.69f, 2.53f), Random.Range(-4.23f, 2.8f), 0f);
        }

        target = Instantiate(targetPrefab, targetPos, Quaternion.identity);
        seeker = Instantiate(seekerPrefab, seekerPos, Quaternion.identity);

        if (avoid)
        {
            //then make sure to include the obstacle as well
            Vector3 obstaclePos = Vector3.Lerp(targetPos, seekerPos, middleSpawn);
            obstacle = Instantiate(obstaclePrefab, obstaclePos, Quaternion.identity);
        }
    }

    void DestroyCurrent()
    {
        //where we will destroy all current when we change to a different number or press 0
        Destroy(target);
        Destroy(seeker);
        if(avoid)
        {
            Destroy(obstacle);
            avoid = false;
        }
    }

    public void StartScene()
    {
        //press the button and change the scene, make a game manager with dontdestroyonload so it continues
        //music starts playing on start or awake
        gameStart = true;
        SceneManager.LoadScene("PlayScene");
    }
}
