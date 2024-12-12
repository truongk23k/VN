using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coins : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Coin")
		{
			this.gameObject.SetActive(false);
			GameSpawn.instance._coinNow--;
		}
		else if (collision.gameObject.name == "Player")
		{
			this.gameObject.SetActive(false);
			GameSpawn.instance._point++;
			GameSpawn.instance._coinNow--;
		}

	}
}
