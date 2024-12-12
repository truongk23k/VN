using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnowledge : Singleton<PlayerKnowledge>
{
	public List<string> knowledges = new List<string>();

	void Start()
    {
		DontDestroyOnLoad(this);
    }

	public void AddeKnowledge(string knowledge)
	{
		knowledges.Add(knowledge);
	}

	public bool HasKnowledge(string knowledge)
	{
		return knowledges.Contains(knowledge);
	}

	public void ClearKnowLedge()
	{
		knowledges.Clear();
	}
}
