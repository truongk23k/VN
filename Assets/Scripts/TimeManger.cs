using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManger : MonoBehaviour
{
	[SerializeField] float _time = 100;
	[SerializeField] Text _text;
	[SerializeField] List<string> _textListSucccess = new List<string>();

	private void Update()
	{
		if (GameManager.instance._isPause)
			return;
		if (!TriggerStartMiniGame.instance._gameStart || TriggerStartMiniGame.instance._gameEnd)
		{
			return;
		}

		_time -= Time.deltaTime;
		if (_time <= 0)
		{
			TriggerStartMiniGame.instance._gameEnd = true;
			_text.gameObject.SetActive(false);
			DialogueManager.instance.StartDialogue(_textListSucccess, false, false);
			StartCoroutine(WaitSetPlayer());
			_time = 60;
		}
		_text.text = ((int)_time).ToString();
	}

	

	IEnumerator WaitSetPlayer()
	{
		yield return new WaitForSeconds(2f);
        PlayerController.instance.transform.position = new Vector2(-15, 0);
        PlayerController.instance.SetPosPlayer();
	}
}
