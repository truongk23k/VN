using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXController : MonoBehaviour
{
	SpriteRenderer sr;

	[Header("FlashFX")]
	[SerializeField] float flashDuration;
	[SerializeField] Material hitMat;
	Material originalMat;

	private void Start()
	{
		sr = GetComponentInChildren<SpriteRenderer>();
		originalMat = sr.material;
	}

	IEnumerator FlashFX()
	{
		sr.material = hitMat;

		yield return new WaitForSeconds(flashDuration);

		sr.material = originalMat;
	}

	public void SetOriginMat()
	{
		sr.material = originalMat;
	}
}
