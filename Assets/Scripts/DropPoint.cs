using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPoint : MonoBehaviour
{
	[SerializeField] GameObject _rock;
	[SerializeField] float _timeCount;
	[SerializeField] float _timeBtw;
	[SerializeField] float x;
	[SerializeField] float y;
	private void Start()
	{
		_timeBtw = Random.Range(x, y);
	}
	private void Update()
	{
		if (!TriggerStartMiniGame2_2.instance._gameStart || TriggerStartMiniGame2_2.instance._gameEnd)
		{
			return;
		}

		_timeCount += Time.deltaTime;
		if (_timeCount >= _timeBtw)
		{
			GameObject _obj = ObjectPooling.instance.GetObject(_rock);
			_obj.transform.position = this.transform.position;
			_obj.SetActive(true);
			_timeCount = 0;
			_timeBtw = Random.Range(x, y);
		}
	}
}
