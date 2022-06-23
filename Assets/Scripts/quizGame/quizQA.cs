using TMPro;
[System.Serializable]
public class quizQA
{
    public string Question; //Question
    public string[] Answers; //list of answers
    public int correct; //index of the correct answer.
}
//Mose of these are just for the Unity Inspector as it is easier to manage all these Answers\Question from there than
// in a script.