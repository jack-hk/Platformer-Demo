using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//JackHK

public class GameManager : MonoBehaviour
{
    // --------------Data-------------- 
    private float timer;

    // --------------In-Built-------------- 
    private void Update()
    {
        timer += Time.deltaTime;
    }

}