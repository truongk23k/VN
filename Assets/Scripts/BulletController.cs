using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletController : MonoBehaviour
{
	[SerializeField] float _speed;
	[SerializeField] float _factor;

	bool collided = false;
	private void Update()
	{
		if (GameManager.instance._isPause)
			return;

		if (TriggerStartMiniGame.instance._gameEnd)
        {
			this.gameObject.SetActive(false);
			return;
        }
        this.transform.Translate(-Vector2.right * _speed * 0.5f * _factor * Time.deltaTime);
		_factor += 0.07f;
		if (this.transform.position.x <= -48f)
		{
			_factor = 0;
			this.gameObject.SetActive(false);
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<PlayerController>() && collided == false)
		{
			collided = true;
			LoadingManager.instance.Clear();
            LoadingManager.instance.AddSceneToLoad("Scene6_1", false);
            LoadingManager.instance.AddSceneToLoad("UIScene", true);
            LoadingManager.instance.LoadSceneInGame();
			
			PlayerController.instance.newPos = new Vector2(-27f, 0);
        }
    }

	IEnumerator CoolDown()
	{
		yield return new WaitForSeconds(2f);
		collided = false;
	}
}
