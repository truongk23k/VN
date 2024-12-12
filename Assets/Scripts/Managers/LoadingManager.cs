using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingManager : Singleton<LoadingManager>
{
    [SerializeField]
    Image _sceneSwitchBG;

    List<AsyncOperation> _scenesToLoad = new List<AsyncOperation> ();
    Dictionary<string,bool> _scenesToAdd = new Dictionary<string,bool> ();

    List<AsyncOperation> _scenesToUnLoad = new List<AsyncOperation>();

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScreenByAction(UnityAction actions)
    {
        StartCoroutine(LoadingActions(actions));

    }
    public void LoadSceneInGame()
    {
        StartCoroutine(LoadingScene());
    }
    public void UnLoadSceneInGame(string sceneName)
    {
        StartCoroutine(UnLoadingScene(sceneName));
    }
    IEnumerator FadeInImage(Image image)
    {
        image.gameObject.SetActive(true);
        image.color = new Color(Color.black.r, Color.black.g, Color.black.b, 0f);
        
        while (_sceneSwitchBG.color.a < 1f)
        {
            yield return new WaitForSeconds(0.001f);
            image.color = new Color(Color.black.r, Color.black.g, Color.black.b, image.color.a + 0.025f);
        }
    }
    IEnumerator FadeOutImage(Image image)
    {
        image.color = new Color(Color.black.r, Color.black.g, Color.black.b, 1f);

        while (_sceneSwitchBG.color.a > 0f)
        {
            yield return new WaitForSeconds(0.00001f);
            image.color = new Color(Color.black.r, Color.black.g, Color.black.b, image.color.a - 0.025f);
        }
        image.gameObject.SetActive(false);
    }

    public void AddSceneToLoad(string sceneName, bool isAdditive)
    {
        _scenesToAdd.Add(sceneName, isAdditive);
    }
    IEnumerator LoadingScene()
    {
        StartCoroutine(FadeInImage(_sceneSwitchBG));
        yield return new WaitForSeconds(1f);
        foreach (var s in _scenesToAdd)
        {
            if (s.Value)
                _scenesToLoad.Add(SceneManager.LoadSceneAsync(s.Key, LoadSceneMode.Additive));
            else
                _scenesToLoad.Add(SceneManager.LoadSceneAsync(s.Key));
        }
        float totalProcess = 0;
        for(int i = 0; i < _scenesToLoad.Count; ++i)
        {
            if (_scenesToLoad[i] == null)
                continue;

            while (!_scenesToLoad[i].isDone)
            {
                totalProcess+= _scenesToLoad[i].progress;
                yield return null;
            }
        }
        StartCoroutine(FadeOutImage(_sceneSwitchBG));
        _scenesToLoad.Clear();
        _scenesToAdd.Clear();
    }
    IEnumerator UnLoadingScene(string sceneName)
    {
        StartCoroutine(FadeInImage(_sceneSwitchBG));
        yield return new WaitForSeconds(0.5f);
        _scenesToUnLoad.Add(SceneManager.UnloadSceneAsync(sceneName));
        float totalProcess = 0;
        for (int i = 0; i < _scenesToUnLoad.Count; ++i)
        {
            while (!_scenesToUnLoad[i].isDone)
            {
                totalProcess += _scenesToUnLoad[i].progress;
                yield return _scenesToUnLoad[i];
            }
        }
        StartCoroutine(FadeOutImage(_sceneSwitchBG));
        _scenesToUnLoad.Clear();
    }

    IEnumerator LoadingActions(UnityAction actions)
    {
        StartCoroutine(FadeInImage(_sceneSwitchBG));
        yield return new WaitForSeconds(1f);
        actions();
        StartCoroutine(FadeOutImage(_sceneSwitchBG));
    }
    public void Clear()
    {
        _scenesToAdd.Clear();
    }
}
