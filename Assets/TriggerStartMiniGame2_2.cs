using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerStartMiniGame2_2 : Singleton<TriggerStartMiniGame2_2>
{
	[SerializeField] List<string> _nameKnowledgeChecks;
	[SerializeField] List<string> text = new List<string>();
	[SerializeField] public bool _gameStart = false;
	[SerializeField] public bool _gameEnd = false;
	
	private void Update()
	{
		if (_gameStart)
		{
			return;
		}
		if (CheckKnowledge())
		{
			DialogueManager.instance.StartDialogue(text, false, false);
			_gameStart = true;		
		}

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
