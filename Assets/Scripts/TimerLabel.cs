using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimerLabel : MonoBehaviour
{
    private TextMeshProUGUI label = null;

    private void Awake()
    {
        label = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        string text = $"Time <3: {Timer.ElapsedSeconds:F3} s";
        int stepsCount = Timer.StepsCount;

        for (int index = 0; index < stepsCount; index++)
        {
            text += $"\n{(index + 1)}. {Timer.GetStepElapsedSeconds(index):F3}";
        }

        label.SetText(text);
    }
}
