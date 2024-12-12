using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool _isPause;
    public GAME_STATE _gameState;

    public bool _doneMap1;
    public bool _doneMap2;
    public bool _doneMap3;
    public bool _doneMap4;
    public bool _doneMap5;
    public bool _win;
    public bool _begin;

    public enum GAME_STATE
    {
        MENU = 0,
        MAP1 = 1,
        MAP2 = 2,
        MAP3 = 3,
        MAP4 = 4,
        MAP5 = 5
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        AudioManager.instance.PlaySound(AudioManager.instance.BGMusicClip, 1f, true);
        _doneMap1 = false;
        _doneMap2 = false;
        _doneMap3 = false;
        _doneMap4 = false;
        _doneMap5 = false;
        _win = false;
        _begin = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_gameState != GAME_STATE.MENU && !_isPause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                
                Pause(true);
            }
        }
    }
    public void ChangeState(GAME_STATE gameState)
    {
        if (gameState == _gameState)
            return;

        if(gameState == GAME_STATE.MENU)
        {
            if (_begin)
            {
                _begin = false;
				LoadingManager.instance.AddSceneToLoad("MenuScene", false);
				LoadingManager.instance.LoadSceneInGame();
				_isPause = false;
            }
            else
            {
				LoadingManager.instance.AddSceneToLoad("MenuScene", false);
				LoadingManager.instance.LoadSceneInGame();
                MenuManager.instance.PlayBtn();
			}
            
        }
        if(gameState == GAME_STATE.MAP1)
        {
            PlayerController.instance._live = 3;
            LoadingManager.instance.AddSceneToLoad("Scene1_1", false);
            LoadingManager.instance.AddSceneToLoad("UIScene", true);
            LoadingManager.instance.LoadSceneInGame();
           
        }
        if(gameState == GAME_STATE.MAP2)
        {
            PlayerController.instance._live = 3;
            LoadingManager.instance.AddSceneToLoad("Scene2_1", false);
            LoadingManager.instance.AddSceneToLoad("UIScene", true);
            LoadingManager.instance.LoadSceneInGame();
            
        }
        if (gameState == GAME_STATE.MAP3)
        {
            PlayerController.instance._live = 3;
            LoadingManager.instance.AddSceneToLoad("Scene6_1", false);
			LoadingManager.instance.AddSceneToLoad("UIScene", true);
			LoadingManager.instance.LoadSceneInGame();
            
        }
        if (gameState == GAME_STATE.MAP4)
        {
            PlayerController.instance._live = 3;
            LoadingManager.instance.AddSceneToLoad("Scene5_1", false);
			LoadingManager.instance.AddSceneToLoad("UIScene", true);
			LoadingManager.instance.LoadSceneInGame();
            
        }
        if (gameState == GAME_STATE.MAP5)
        {
            PlayerController.instance._live = 3;
            LoadingManager.instance.AddSceneToLoad("Scene5", false);
			LoadingManager.instance.AddSceneToLoad("UIScene", true);
			LoadingManager.instance.LoadSceneInGame();
            
        }

        _gameState = gameState;
    }

    public void Pause(bool pause)
    {
        _isPause = pause;
        if (pause)
        {
            LoadingManager.instance.AddSceneToLoad("PauseScene", true);
            LoadingManager.instance.LoadSceneInGame();
        }
        else
        {
            LoadingManager.instance.UnLoadSceneInGame("PauseScene");
        }

        
    }

    public int CheckStoneCollect()
    {
        int i = 0;
        if(_doneMap1)
            i++;

        if (_doneMap2)
            i++;

        if (_doneMap3)
            i++;

        if (_doneMap4)
            i++;

        if (_doneMap5)
            i++;

        return i;
    }
}
