using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnAnswer : MonoBehaviour, IPointerDownHandler
{
	[SerializeField] Button _button;
	[SerializeField] Text _text;

	public void SetAnswer(string txt)
	{
		_text.text = txt;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (QuizzManager.instance.isChecking)
			return;

		bool check = QuizzManager.instance.CheckAnswer(_text.text);
		if (check) 
			_button.image.color = Color.green;
		else
			_button.image.color = Color.red;

	}
}
