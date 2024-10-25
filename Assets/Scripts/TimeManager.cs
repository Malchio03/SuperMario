using UnityEngine;
using System.Collections;
using TMPro;

public class SimpleTimer : MonoBehaviour
{

    public float targetTime = 10.0f;
    public TextMeshProUGUI timeText;
    //public int nextWorld = 1;
    //public int nextStage = 1;

    public void Update()
    {
        if (targetTime > 0.0f)
        {
            targetTime -= Time.deltaTime;
            timeText.text = $"{targetTime}";
        }else 
        {
            timerEnded();
        }

    }

    void timerEnded()
    {
        GameManager.Instance.ResetLevel(1f);    // TODO schermata gameover
    }


}