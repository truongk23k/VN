using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class QuizzManager : Singleton<QuizzManager>
{
    [SerializeField] GameObject quizz;
    [SerializeField] Text questionText;
    [SerializeField] List<Button> answerButtons;
    public List<Quizz> quizzes;
    [SerializeField] string _nameKnowledge;
    [SerializeField] int _scorePass;
    [SerializeField] int _score;
    List<Quizz> selectedQuizzes;
    [SerializeField] int currentQuestionIndex;
    [SerializeField] List<string> _textListSuccess;
    [SerializeField] List<string> _textListFail;

    public bool isChecking;


    void Start()
    {
		Console.OutputEncoding = Encoding.UTF8;
	}
   

    public void InitQuiz(List<Quizz> quizzesData, string nameKnowledgeData, int scorePassData, List<string> textListSuccess, List<string> textListFail)
    {
		PlayerController.instance.isBusy = true;
		quizzes = quizzesData;
        _nameKnowledge = nameKnowledgeData;
        _scorePass = scorePassData;
        _textListSuccess = textListSuccess;
        _textListFail = textListFail;
        _score = 0;
        selectedQuizzes = SelectRandomQuizzes(10);

        currentQuestionIndex = 0;
        quizz.SetActive(true);
        DisplayQuiz(currentQuestionIndex);
    }

    List<Quizz> SelectRandomQuizzes(int count)
    {
        List<Quizz> randomQuizzes = new List<Quizz>();
        List<int> usedIndex = new List<int>();

        while (randomQuizzes.Count < count)
        {
            int randomIndex = UnityEngine.Random.Range(0, quizzes.Count);

            if (!usedIndex.Contains(randomIndex))
            {
                usedIndex.Add(randomIndex);
                randomQuizzes.Add(quizzes[randomIndex]);
            }
        }

        return randomQuizzes;
    }

    void DisplayQuiz(int index)
    {
		isChecking = false;

		if (index < selectedQuizzes.Count)
        {
            questionText.text = (index + 1).ToString() + ". " + selectedQuizzes[index].question;
            List<string> answers = selectedQuizzes[index].answers;

            for (int i = 0; i < answerButtons.Count; i++)
            {
                answerButtons[i].GetComponent<Button>().image.color = Color.white;
                answerButtons[i].GetComponent<BtnAnswer>().SetAnswer(answers[i]);
            }
        }
        else
        {
            if (_score >= _scorePass)
            {
                PlayerKnowledge.instance.AddeKnowledge(_nameKnowledge);
                DialogueManager.instance.StartDialogue(_textListSuccess, false, true);
            }
            else
                DialogueManager.instance.StartDialogue(_textListFail, false, false);

            quizz.SetActive(false);
			PlayerController.instance.isBusy = false;
			Debug.Log("hết");
        }
    }

    public bool CheckAnswer(string selectedAnswer)
    {
        isChecking = true;

        StartCoroutine(WaitCheckQuestion(1f));

        if (selectedAnswer == selectedQuizzes[currentQuestionIndex].correctAnswer)
        {
            _score++;
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator WaitCheckQuestion(float time)
    {
        yield return new WaitForSeconds(time);
        currentQuestionIndex++;
        DisplayQuiz(currentQuestionIndex);
    }

}


[System.Serializable]
public class Quizz
{
    public string question;
    public List<string> answers;
    public string correctAnswer;
}