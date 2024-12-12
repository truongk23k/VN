using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitToMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.InitMoveScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
