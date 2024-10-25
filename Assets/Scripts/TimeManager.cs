using UnityEngine;
using System.Collections;
using TMPro;

public class SimpleTimer : MonoBehaviour
{
    public float targetTime = 60.0f;
    public TextMeshProUGUI timeText;

    // Flag to check if the flagpole has been reached
    public bool isFlagReached = false;

    public void Update()
    {
        if (targetTime > 0.01f && !isFlagReached)
        {
            targetTime -= Time.deltaTime;
            timeText.text = $"{targetTime:F2}";  // Format to two decimal places
        }
        else if (!isFlagReached)
        {
            timerEnded();
        }
    }

    void timerEnded()
    {
        GameManager.Instance.ResetLevel(0f);    // Trigger level reset only if flag is not reached
    }
}
