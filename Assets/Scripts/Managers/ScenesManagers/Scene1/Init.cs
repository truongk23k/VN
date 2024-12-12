using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    [SerializeField]
    Vector2 _spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerKnowledge.instance.knowledges == null || PlayerKnowledge.instance.knowledges.Count == 0)
        {
		    PlayerController.instance.Init(_spawnPos);
        }
        else
        {
            PlayerController.instance.InitMoveScene();
        }
        GameManager.instance._isPause = false;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
