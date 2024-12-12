using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ContinueBtn()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.UIClips[1], 0, false);
        GameManager.instance.Pause(false);

    }
    public void SettingBtn()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.UIClips[1], 0, false);
        LoadingManager.instance.AddSceneToLoad("SettingScene", true);
        LoadingManager.instance.LoadSceneInGame();
    }
    public void ToMenuBtn()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.UIClips[1], 0, false);
        GameManager.instance.ChangeState(GameManager.GAME_STATE.MENU);
    }
    public void Exit()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.UIClips[1], 0, false);
        Application.Quit();
    }
}
