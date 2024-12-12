using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoins : MonoBehaviour
{
	[SerializeField] float _timeCount = 0;
	[SerializeField] float _timeBtw;
	[SerializeField] GameObject _bullet;
	void Update()
	{
		if (GameManager.instance._isPause)
			return;
		if (!TriggerStartMiniGame.instance._gameStart || TriggerStartMiniGame.instance._gameEnd)
		{
			return;
		}
		_timeCount += Time.deltaTime;
		if (_timeCount >= _timeBtw)
		{
			Vector2 pos = new Vector2(this.transform.position.x, Random.Range(-3.4f, 3.4f));
			GameObject _obj = ObjectPooling.instance.GetObject(_bullet);
			_obj.transform.position = pos;
			_obj.SetActive(true);
			_timeCount = 0;

		}
	}
}
