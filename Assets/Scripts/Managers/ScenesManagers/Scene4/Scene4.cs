using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene4 : MonoBehaviour
{
    [SerializeField]
    GameObject NPC;

    [SerializeField]
    GameObject NPCQuiz;

    [SerializeField]
    List<string> text = new List<string>();

    [SerializeField] GameObject triggerObSpawn;

    bool showed = false;
    // Start is called before the first frame update
    void Start()
    {
        showed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerKnowledge.instance.HasKnowledge("5a") && !showed)
        {
            NPCQuiz.SetActive(true);
            NPC.SetActive(false);

            triggerObSpawn.SetActive(true);
			showed = true;
        }
    }
}
