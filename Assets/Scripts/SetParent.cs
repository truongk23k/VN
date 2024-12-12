using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParent : MonoBehaviour
{
    void Start()
    {
        this.gameObject.transform.parent = PlayerController.instance.gameObject.transform;
        this.transform.localPosition = new Vector2(0,-0.02f);
    }
    private void Update()
    {
        if(GameManager.instance._gameState != GameManager.GAME_STATE.MAP3)
        {
            Destroy(this.gameObject);
        }
    }
}
