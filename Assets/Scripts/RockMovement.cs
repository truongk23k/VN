using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RockMovement : MonoBehaviour
{
	[SerializeField] float _speed;
	void Update()
	{
		if (GameManager.instance._isPause)
			return;

		if (!TriggerStartMiniGame2_2.instance._gameStart || TriggerStartMiniGame2_2.instance._gameEnd)
		{
			return;
		}
		this.transform.Translate(-Vector2.up * _speed * Time.deltaTime);
		if (this.transform.position.y <= -23f)
		{
			this.gameObject.SetActive(false);
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name == "Player")
		{
			LoadingManager.instance.AddSceneToLoad("Scene2_2", false);
			LoadingManager.instance.AddSceneToLoad("UIScene", true);
			LoadingManager.instance.LoadSceneInGame();
			GameSpawn.instance._point = 0;
			PlayerController.instance._live = 3;
		}
	}
}
