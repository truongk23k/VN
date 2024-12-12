using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
	[SerializeField] Text _text;
	void Update()
	{
		_text.text = GameSpawn.instance._point.ToString();
	}
}
