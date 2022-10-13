using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Transform movePoint;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private bool movingTowards = true;

    private void Awake()
    {
        movePoint = GetComponentInChildren<Transform>();
    }

    private void Start()
    {
        targetPosition = GameObject.Find("A").transform.position;
        startPosition = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        if (!transform.position.Equals(targetPosition) && movingTowards)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.01f);
            movingTowards = false;

        }
        else if (!transform.position.Equals(startPosition) && !movingTowards)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, 0.01f);
            movingTowards = true;
        }
        else { Debug.Log("Not working");  }
    }
}
