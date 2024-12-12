using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScene : MonoBehaviour
{
	[SerializeField] List<string> _nameKnowledgeChecks; 
	[SerializeField] string _nameSceneToMove;
	[SerializeField] Vector2 _newPos;

	[SerializeField] List<string> _textListSpecial = new List<string>();

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			if (_nameKnowledgeChecks != null && _nameKnowledgeChecks.Count > 0)
			{
				bool hasAllKnowledge = CheckKnowledge();

				if (hasAllKnowledge)
				{
					LoadingManager.instance.AddSceneToLoad(_nameSceneToMove, false);
                    LoadingManager.instance.AddSceneToLoad("UIScene", true);
                    LoadingManager.instance.LoadSceneInGame();
					PlayerController.instance.newPos = _newPos;
				}
				else
				{
					InteractSpecial();
				}
			}
			else
			{
                LoadingManager.instance.AddSceneToLoad(_nameSceneToMove, false);
                LoadingManager.instance.AddSceneToLoad("UIScene", true);
                LoadingManager.instance.LoadSceneInGame();
                PlayerController.instance.newPos = _newPos;
			}
		}
	}

	private void InteractSpecial()
	{
		DialogueManager.instance.StartDialogue(_textListSpecial, false, false);
	}

	private bool CheckKnowledge()
	{
		bool hasAllKnowledge = true;

		foreach (string knowledge in _nameKnowledgeChecks)
		{
			if (!PlayerKnowledge.instance.HasKnowledge(knowledge))
			{
				hasAllKnowledge = false;
				break;
			}
		}

		return hasAllKnowledge;
	}
}
