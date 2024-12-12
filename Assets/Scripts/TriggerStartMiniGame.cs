using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerStartMiniGame : Singleton<TriggerStartMiniGame>
{
	[SerializeField] List<string> _nameKnowledgeChecks;
	[SerializeField] public bool _gameStart = false;
	[SerializeField] public bool _gameEnd = false;
	[SerializeField] Vector3 _newCamPos;
	[SerializeField] Text _time;
	private void Update()
	{
		if (_gameStart)
		{
			return;
		}
		if (CheckKnowledge())
		{
			_gameStart = true;
			CamController.instance.GetComponent<CinemachineVirtualCamera>().Follow = null;
			CamController.instance.gameObject.transform.position = _newCamPos;
			_time.gameObject.SetActive(true);
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
