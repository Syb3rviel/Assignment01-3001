using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //prefabs to attach and instantiate to
    [SerializeField] GameObject targetPrefab;
    [SerializeField] GameObject seekerPrefab;
    [SerializeField] GameObject obstaclePrefab;

    //transforms are important to have
    [SerializeField]
    Transform[] instPoints = new Transform[6];
    [SerializeField] GameObject masterTransform;

    //basic gameobjects that will attach to prefabs once instantiated
    GameObject target;
    GameObject seeker;
    GameObject obstacle;

    //floats needs for certain speed / radius
    float maxSpeed = 5.0f;
    float turnSpeed = 150f;
    float maxAcceleration = 5.0f;
    float slowingDistance = 3f;
    float obstacleRadius = 2.4f;
    float middleSpawn = 0.5f;

    bool gameStart = false;
    bool avoid = false;

    private Vector3 currentVelocity = Vector3.zero;

    private void Awake()
    {
        //dont destroy on load to the gamemanager so it stays
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(masterTransform);

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0) && gameStart)
        {
            //remove everything instantiated in the scene //aka the prefabs!!!
            DestroyCurrent();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1) && gameStart)
        {
            //doo basic seek
            DestroyCurrent();
            RandomInstantiate();

        }
        else if( Input.GetKeyDown(KeyCode.Alpha2) && gameStart)
        {
            //do basic flee
            //add boundaries
            DestroyCurrent();
            RandomInstantiate();

        }
        else if(Input.GetKeyDown (KeyCode.Alpha3) && gameStart)
        {
            //arrive function
            DestroyCurrent();
            RandomInstantiate();

        }
        else if(Input.GetKeyDown(KeyCode.Alpha4) && gameStart)
        {
            //avoid
            DestroyCurrent();
            avoid = true;
            RandomInstantiate();

        }
    }

    void RandomInstantiate()
    {
        //this is where we will randomly place them on the board
        int targetPos = Random.Range(0, 6);
        int seekerPos = Random.Range(0, 6);
        //Debug.Log(targetPos + seekerPos + obstaclePos);

        while (targetPos == seekerPos)
        {
            //reroll
            targetPos = Random.Range(0, 6);
            seekerPos = Random.Range(0, 6);
        }

        target = Instantiate(targetPrefab, instPoints[targetPos]);
        seeker = Instantiate(seekerPrefab, instPoints[seekerPos]);

        if (avoid)
        {
            //then make sure to include the obstacle as well
            Vector3 obstaclePos = Vector3.Lerp(instPoints[targetPos].position, instPoints[seekerPos].position, middleSpawn);
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
