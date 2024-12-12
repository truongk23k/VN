using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] Text dialogueText;
    Queue<string> texts;
    public bool _isShow;
    public bool hasQuiz;
    [SerializeField] List<Quizz> quizzes;
    [SerializeField] string _nameKnowledge;
    [SerializeField] int _scorePass;
    [SerializeField] List<string> _textListSuccess;
    [SerializeField] List<string> _textListFail;
    [SerializeField] bool _showScreenStone;

    void Start()
    {
		Console.OutputEncoding = Encoding.UTF8;
		texts = new Queue<string>();
        dialoguePanel.SetActive(false);
        _isShow = false;
    }

    public void StartDialogue(List<string> textList, bool quiz, bool showScreenStone)
    {
        PlayerController.instance.isBusy = true;
        hasQuiz = quiz;
        _isShow = true;
        _showScreenStone = showScreenStone;
        dialoguePanel.SetActive(true);
        texts.Clear();

        foreach (string text in textList)
        {
            texts.Enqueue(text);
        }

        DisplayNextSentence();

    }

    public void StartDialogue(List<string> textList, bool quiz, List<Quizz> lsFromNPC, string nameKnowledgeFromNPC, int scorePassFromNPC, List<string> textListSuccess, List<string> textListFail)
    {
		PlayerController.instance.isBusy = true;
		hasQuiz = quiz;
        quizzes = lsFromNPC;
        _nameKnowledge = nameKnowledgeFromNPC;
        _scorePass = scorePassFromNPC;
        _textListSuccess = textListSuccess;
        _textListFail = textListFail;
        _isShow = true;
        dialoguePanel.SetActive(true);
        texts.Clear();

        foreach (string text in textList)
        {
            texts.Enqueue(text);
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if (texts.Count == 0)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = texts.Dequeue();
    }

    void EndDialogue()
    {
		PlayerController.instance.isBusy = false;
		_isShow = false;
        dialoguePanel.SetActive(false);
        if (hasQuiz)
            QuizzManager.instance.InitQuiz(quizzes, _nameKnowledge, _scorePass, _textListSuccess, _textListFail);

        if (_showScreenStone)
            UIManager.instance.ShowStoneCollected();

	}
}