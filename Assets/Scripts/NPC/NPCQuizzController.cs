using System.Collections.Generic;
using UnityEngine;

public class NPCQuizzController : MonoBehaviour, IInteractable
{
    [SerializeField] List<string> _nameKnowledgeChecks;
    [SerializeField] string _nameKnowledge;
    [SerializeField] GameObject _interactText;
    bool _isPlayerNear = false;
    [SerializeField] bool _beginActive;

    // Danh sách các câu thoại
    [SerializeField] List<string> _textList = new List<string>();
    [SerializeField] List<string> _textListSpecial = new List<string>();

    [SerializeField] List<string> _textListSuccess = new List<string>();
    [SerializeField] List<string> _textListFail = new List<string>();

    [SerializeField] List<Quizz> quizzes;
    [SerializeField] int _scorePass;

    void Start()
    {
        _interactText.SetActive(false);
        _beginActive = false;
    }

    void Update()
    {
        if (_isPlayerNear && Input.GetKeyDown(KeyCode.E) && !_beginActive)
        {
            Interact();
        }

        if (_beginActive)
        {
            if (!DialogueManager.instance._isShow)
            {
                _beginActive = false;

            }
        }
    }

    private bool CheckKnowledge()
    {
        bool hasAllKnowledge = true;

        foreach (string knowledge in _nameKnowledgeChecks)
        {
            if (!PlayerKnowledge.instance.HasKnowledge(knowledge))
            {
                hasAllKnowledge = false;
                break;
            }
        }

        return hasAllKnowledge;
    }

    public void Interact()
    {
        bool hasAllKnowledge = CheckKnowledge();
        if (hasAllKnowledge)
        {
            _beginActive = true;
            DialogueManager.instance.StartDialogue(_textList, true, quizzes, _nameKnowledge, _scorePass, _textListSuccess, _textListFail);
        }
        else
        {
            DialogueManager.instance.StartDialogue(_textListSpecial, false, false);
        }
    }

    public void ShowCanBeInteract()
    {
        _interactText.SetActive(true);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isPlayerNear = true;
            ShowCanBeInteract();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isPlayerNear = false;
            _interactText.SetActive(false);
        }
    }
}