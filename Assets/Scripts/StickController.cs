using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StickController :MonoBehaviour, IInteractable, IUseable
{
	int _itemIndex = 1;
	bool _isPlayerNear = false;
	[SerializeField] GameObject _interactText;

	public void Interact()
	{
		PlayerController.instance.GetItem(this.gameObject, _itemIndex);
		UIManager.instance.ShowTutorialTxt("Ấn chuột phải để đánh");
	}

	public void ShowCanBeInteract()
	{
		_interactText.SetActive(true);
	}
	public bool CanUse()
	{
		throw new System.NotImplementedException();
	}

	public void Use()
	{
		throw new System.NotImplementedException();
	}

	private void Start()
	{
		_interactText.SetActive(false);
	}

	private void Update()
	{
		if (_isPlayerNear && Input.GetKeyDown(KeyCode.E))
		{
			Interact();
		}

	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			_isPlayerNear = true;
			ShowCanBeInteract();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			_isPlayerNear = false;
			_interactText.SetActive(false);
		}
	}

}
