using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    [SerializeField] Image _LostHeart1, _LostHeart2, _LostHeart3, _stoneImg;
    [SerializeField] Text _tutorialTxt, _stoneTxt;
    [SerializeField] GameObject _screenStone;
	[SerializeField] GameObject _screenWin;

	[SerializeField] List<Sprite> _stoneSprites;
    // Start is called before the first frame update
    void Start()
    {
		Console.OutputEncoding = Encoding.UTF8;
		ShowTutorialTxt("Sử dụng  các phím W,A,S,D \r\nđể di chuyển nhân vật");
    }

    // Update is called once per frame
    private void Update()
    {
		if (GameManager.instance.CheckStoneCollect() == 5 && !GameManager.instance._win)
		{
            GameManager.instance._win = true;
            ShowScreenWin();
		}

		if (PlayerController.instance._live == 3)
        {
            _LostHeart1.gameObject.SetActive(false);
            _LostHeart2.gameObject.SetActive(false);
            _LostHeart3.gameObject.SetActive(false);
        }
        if (PlayerController.instance._live == 2)
        {
            _LostHeart1.gameObject.SetActive(false);
            _LostHeart2.gameObject.SetActive(false);
            _LostHeart3.gameObject.SetActive(true);
        }
        if (PlayerController.instance._live == 1)
        {
            _LostHeart1.gameObject.SetActive(false);
            _LostHeart2.gameObject.SetActive(true);
            _LostHeart3.gameObject.SetActive(true);
        }
        if (PlayerController.instance._live <= 0)
        {
            _LostHeart1.gameObject.SetActive(true);
            _LostHeart2.gameObject.SetActive(true);
            _LostHeart3.gameObject.SetActive(true);
        }
    }

    IEnumerator ShowTutorialTxtIE(string text)
    {
		_tutorialTxt.text = text;
		_tutorialTxt.color = new Color(_tutorialTxt.color.r, _tutorialTxt.color.g, _tutorialTxt.color.b, 1f);
		_tutorialTxt.gameObject.SetActive(true);
		
        yield return new WaitForSeconds(5f);
        while (_tutorialTxt.color.a >= 0f)
        {
            yield return new WaitForSeconds(0.05f);
            _tutorialTxt.color = new Color(_tutorialTxt.color.r, _tutorialTxt.color.g, _tutorialTxt.color.b, _tutorialTxt.color.a - 0.05f);
        }
        _tutorialTxt.gameObject.SetActive(false);
    }

    public void ShowTutorialTxt(string text)
    {
        StartCoroutine(ShowTutorialTxtIE(text));
    }

    public void ShowStoneCollected()
    {
        if(GameManager.instance._gameState == GameManager.GAME_STATE.MAP1)
        {
            _stoneImg.sprite = _stoneSprites[0];
            _stoneTxt.text = "Chúc mừng bạn đã hoàn thành bản đồ\r\nĐây là viên đá cổ vật\r\nThánh Địa Mỹ Sơn";
            GameManager.instance._doneMap1 = true;
        }
        if (GameManager.instance._gameState == GameManager.GAME_STATE.MAP2)
        {
            _stoneImg.sprite = _stoneSprites[1];
            _stoneTxt.text = "Chúc mừng bạn đã hoàn thành bản đồ\r\nĐây là viên đá cổ vật\r\nThành nhà Hồ";
            GameManager.instance._doneMap2 = true;
        }
        if (GameManager.instance._gameState == GameManager.GAME_STATE.MAP3)
        {
            _stoneImg.sprite = _stoneSprites[2];
            _stoneTxt.text = "Chúc mừng bạn đã hoàn thành bản đồ\r\nĐây là viên đá cổ vật\r\nPhố cổ Hội An";
            GameManager.instance._doneMap3 = true;
        }
        if (GameManager.instance._gameState == GameManager.GAME_STATE.MAP4)
        {
            _stoneImg.sprite = _stoneSprites[3];
            _stoneTxt.text = "Chúc mừng bạn đã hoàn thành bản đồ\r\nĐây là viên đá cổ vật\r\nPhong Nha Kẻ Bàng";
            GameManager.instance._doneMap4 = true;
        }
        if (GameManager.instance._gameState == GameManager.GAME_STATE.MAP5)
        {
            _stoneImg.sprite = _stoneSprites[4];
            _stoneTxt.text = "Chúc mừng bạn đã hoàn thành bản đồ\r\nĐây là viên đá cổ vật\r\nCố đô Huế";
            GameManager.instance._doneMap5 = true;
        }
        _screenStone.SetActive(true);
    }

    public void HideScreenStone()
    {
		_screenStone.SetActive(false);
        GameManager.instance.ChangeState(GameManager.GAME_STATE.MENU);
	}

    public void ShowScreenWin()
    {
        _screenWin.SetActive(true);
		_screenStone.SetActive(false);
	}

	public void HideScreenWin()
	{
		_screenWin.SetActive(false);
		GameManager.instance.ChangeState(GameManager.GAME_STATE.MENU);
	}
}
