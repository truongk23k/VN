using System.Collections.Generic;
using UnityEngine;

public class TriggerEventSpawnEnemy : MonoBehaviour
{
	[SerializeField] string _nameKnowledge;
	[SerializeField] GameObject _enemy;
	[SerializeField] bool _beginActive;

	[SerializeField] Vector2 posSpawn;

	// Danh sách các câu thoại
	[SerializeField]
	List<string> textList = new List<string>();

	void Start()
	{
		_beginActive = false;
	}

	void Update()
	{
		if (_beginActive)
		{
			if (!DialogueManager.instance._isShow)
			{
				_beginActive = false;
				//spawn 
				_enemy.GetComponent<EnemyController>().Init(posSpawn, _nameKnowledge);
				this.gameObject.SetActive(false);
			}
		}

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		_beginActive = true;
		DialogueManager.instance.StartDialogue(textList, false, false);

	}

}
