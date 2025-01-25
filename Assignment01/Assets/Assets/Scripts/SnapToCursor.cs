using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this script is for testing purposes --- after i finalize the rest of my scripts i shall be finished with this and work on the random drop in instantiation
//during this they will also probably not have limits of where they cant go so the flee option he will just run and disapear for now
public class SnapToCursor : MonoBehaviour
{
    void Update()
    {
        Vector2 mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        //Debug.Log(mouse);

        transform.position = mouse;
    }
}
