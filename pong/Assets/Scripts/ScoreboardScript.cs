using TMPro;
using UnityEngine;

public class ScoreboardScript : MonoBehaviour
{
    public GameObject leftText;
    public GameObject rightText;

    public GameObject infoText;

    public void DisplayScore(int leftScore, int rightScore)
    {
        this.rightText.GetComponent<TextMeshProUGUI>().text = leftScore.ToString();
        this.leftText.GetComponent<TextMeshProUGUI>().text = rightScore.ToString();
    }

    public void DisplayText(string text)
    {
        this.infoText.GetComponent<TextMeshProUGUI>().text = text;
    }
}
