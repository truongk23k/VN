using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSpawn : Singleton<GameSpawn>
{
	[SerializeField] GameObject _coin;
	[SerializeField] float rx;
	[SerializeField] float ry;
	[SerializeField] float dx;
	[SerializeField] float dy;
	[SerializeField] int _maxCoins;
	[SerializeField] public int _coinNow = 0;
	[SerializeField] public int _coinWin = 20;

	[SerializeField] List<string> _textListSuccess;

	public int _point;

	private void Start()
	{
		_point = 0;
		_coinWin = 20;
	}

	void Update()
	{
		if (GameManager.instance._isPause)
			return;
		if (!TriggerStartMiniGame2_2.instance._gameStart || TriggerStartMiniGame2_2.instance._gameEnd)
		{
			return;
		}

		if (_coinNow < _maxCoins)
		{
			float ranger = Random.Range(rx, ry);
			float ranged = Random.Range(dx, dy);

			GameObject _obj = ObjectPooling.instance.GetObject(_coin);
			_obj.transform.position = new Vector2(ranger, ranged);
			_obj.SetActive(true);
			_coinNow++;
		}
		if (_point == _coinWin)
		{
			TriggerStartMiniGame2_2.instance._gameEnd = true;
			/*DialogueManager.instance.StartDialogue(_textListSuccess, false, false);*/
			StartCoroutine(MoveScene());
		}
	}

	IEnumerator MoveScene()
	{
		yield return new WaitForSeconds(1f);
		LoadingManager.instance.AddSceneToLoad("Scene2_3", false);
		LoadingManager.instance.AddSceneToLoad("UIScene", true);
		LoadingManager.instance.LoadSceneInGame();
		PlayerController.instance.newPos = new Vector2(0, 3.5f);
	}
}
