using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField]
    GameObject _chooseMapScreen;
    [SerializeField]
    Text _stoneCollected;
    [SerializeField]
    Text _stoneCollected1;
    [SerializeField]
    Text _stoneCollected2;
    [SerializeField]
    Text _stoneCollected3;
    [SerializeField]
    Text _stoneCollected4;
    [SerializeField]
    Text _stoneCollected5;

    [SerializeField]
    string _stoneCollectedTxt;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance._begin)
        {
            _chooseMapScreen.SetActive(false);
        }
        else
        {
            _chooseMapScreen.SetActive(true);
        }
        PlayerController.instance.ColorOK();
	}

    // Update is called once per frame
    void Update()
    {
        _stoneCollected.text = GameManager.instance.CheckStoneCollect().ToString() + "/5";
        
        if (GameManager.instance._doneMap1)
            _stoneCollected1.text = _stoneCollectedTxt;
        else
            _stoneCollected1.text = "";

        if (GameManager.instance._doneMap2)
            _stoneCollected2.text = _stoneCollectedTxt;
        else
            _stoneCollected2.text = "";

        if (GameManager.instance._doneMap3)
            _stoneCollected3.text = _stoneCollectedTxt;
        else
            _stoneCollected3.text = "";

        if (GameManager.instance._doneMap4)
            _stoneCollected4.text = _stoneCollectedTxt;
        else
            _stoneCollected4.text = "";

        if (GameManager.instance._doneMap5)
            _stoneCollected5.text = _stoneCollectedTxt;
        else
            _stoneCollected5.text = "";
    }

    public void PlayBtn()
    {
        UnityAction actions; 
        actions = () => _chooseMapScreen.SetActive(true);
        LoadingManager.instance.LoadScreenByAction(actions);
        AudioManager.instance.PlaySound(AudioManager.instance.UIClips[1], 0, false);
    }

    public void SettingBtn()
    {
        LoadingManager.instance.AddSceneToLoad("SettingScene",true);
        LoadingManager.instance.LoadSceneInGame();
        AudioManager.instance.PlaySound(AudioManager.instance.UIClips[1], 0, false);
        
    }

    public void BackToMenuBtn()
    {
        UnityAction actions;
        actions = () => _chooseMapScreen.SetActive(false);
        LoadingManager.instance.LoadScreenByAction(actions);
        AudioManager.instance.PlaySound(AudioManager.instance.UIClips[1], 0, false);

    }

    public void ExitBtn()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.UIClips[1], 0, false);
        Application.Quit();
    }

    #region ChooseMapBtn

    public void Map1Btn()
    {
        Debug.Log("anm1");
		PlayerKnowledge.instance.ClearKnowLedge();
		GameManager.instance.ChangeState(GameManager.GAME_STATE.MAP1);
        AudioManager.instance.PlaySound(AudioManager.instance.UIClips[1], 0, false);
		GameManager.instance._isPause = false;
		PlayerKnowledge.instance.ClearKnowLedge();
	}
	public void Map2Btn()
    {
		PlayerKnowledge.instance.ClearKnowLedge();
		GameManager.instance.ChangeState(GameManager.GAME_STATE.MAP2);
        AudioManager.instance.PlaySound(AudioManager.instance.UIClips[1], 0, false);
		GameManager.instance._isPause = false;
		PlayerKnowledge.instance.ClearKnowLedge();
	}
    public void Map3Btn()
    {
		PlayerKnowledge.instance.ClearKnowLedge();
		GameManager.instance.ChangeState(GameManager.GAME_STATE.MAP3);
        AudioManager.instance.PlaySound(AudioManager.instance.UIClips[1], 0, false);
		GameManager.instance._isPause = false;
		PlayerKnowledge.instance.ClearKnowLedge();
	}
    public void Map4Btn()
    {
		PlayerKnowledge.instance.ClearKnowLedge();
		GameManager.instance.ChangeState(GameManager.GAME_STATE.MAP4);
        AudioManager.instance.PlaySound(AudioManager.instance.UIClips[1], 0, false);
		GameManager.instance._isPause = false;
		PlayerKnowledge.instance.ClearKnowLedge();
	}
    public void Map5Btn()
    {
		PlayerKnowledge.instance.ClearKnowLedge();
		GameManager.instance.ChangeState(GameManager.GAME_STATE.MAP5);
		AudioManager.instance.PlaySound(AudioManager.instance.UIClips[1], 0, false);
        GameManager.instance._isPause = false;
		PlayerKnowledge.instance.ClearKnowLedge();
	}

    #endregion
}
