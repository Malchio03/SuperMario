using UnityEngine;
using System.Collections;
using TMPro;

public class SimpleTimer : MonoBehaviour
{
    public float targetTime = 60.0f;
    public TextMeshProUGUI timeText;
    public bool isFlagReached = false;

    public void Update()
    {
        if (targetTime > 0.01f && !isFlagReached)
        {
            targetTime -= Time.deltaTime;
            // Converti il tempo in un numero intero e mostra solo i secondi
            int seconds = Mathf.CeilToInt(targetTime);
            timeText.text = seconds.ToString();
        }
        else if (!isFlagReached)
        {
            timerEnded();
        }
    }

    void timerEnded()
    {
        GameManager.Instance.ResetLevel(0f);
    }
}