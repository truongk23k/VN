using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene51 : Singleton<Scene51>
{
    public bool turnOffWall = false;
    public int coin = 0;
    [SerializeField]
    Image UICoin;
    [SerializeField]
    Text _coinTxt;

    [SerializeField]
    GameObject NPC;

    [SerializeField]
    GameObject NPCQuiz;

    [SerializeField]
    List<string> texts = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(coin == 10)
        {
            HideUICoin();
            coin = 0;
            DialogueManager.instance.StartDialogue(texts, false, false);
            Spawner.instance.DeSpawnBat();
            NPC.SetActive(false);
            NPCQuiz.SetActive(true);
        }
        if (UICoin.gameObject.activeSelf)
        {
            _coinTxt.text = coin.ToString() + " / 10";
        }
    }
    public void ShowUICoin()
    {
        UICoin.gameObject.SetActive(true);
    }
    public void HideUICoin()
    {
        UICoin.gameObject.SetActive(false);
    }
}
