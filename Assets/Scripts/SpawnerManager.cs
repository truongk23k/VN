using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
	[SerializeField] GameObject _rock;
	[SerializeField] int _rockNumber;

	[SerializeField] GameObject _coin;
	[SerializeField] int _coinNumber;
	void Start()
	{
		ObjectPooling.instance.CreatePool(_rock, _rockNumber);
		ObjectPooling.instance.CreatePool(_coin, _coinNumber);
	}
}
