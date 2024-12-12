using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    [SerializeField]
    List<string> textNoKnowLedge = new List<string>();

    [SerializeField]
    List<string> textHasKnowLedge = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
    }

    // Update is called once per frame
    void Update()
    {
        if (Scene51.instance.turnOffWall)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (!collision.gameObject.GetComponent<PlayerKnowledge>().HasKnowledge("A"))
            {
                DialogueManager.instance.StartDialogue(textNoKnowLedge, false, false);
                return;
            }
            DialogueManager.instance.StartDialogue(textHasKnowLedge, false, false);
            /*CamController.instance.Shake();*/
            Scene51.instance.turnOffWall = true;
            Scene51.instance.ShowUICoin();
            Spawner.instance.SpawnBat();
        }
    }
}
