using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacles : MonoBehaviour
{
    private float leftWall = -10f;


    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= leftWall)
        {
            Destroy(gameObject);
        }
    }
}
