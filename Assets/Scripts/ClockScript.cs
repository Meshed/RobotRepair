using UnityEngine;
using System.Collections;

public class ClockScript : MonoBehaviour
{
    public bool ClockIsPaused;
    public float StartTime;
    public float TimeRemaining;
    public float Percent;
    public Texture2D ClockBackground;
    public Texture2D ClockForeground;
    public float ClockForgroundMaxWidth;

	void Awake ()
	{
	    StartTime = 120;
	    ClockForgroundMaxWidth = ClockForeground.width;
	}
	
    void Update()
    {
        if (!ClockIsPaused)
        {
            DoCountdown();
        }
    }

    void OnGUI()
    {
        float newBarWidth = (Percent/100)*ClockForgroundMaxWidth;
        int gap = 20;

        GUI.BeginGroup(new Rect(Screen.width - ClockBackground.width - gap, gap, ClockBackground.width, ClockBackground.height));
        GUI.DrawTexture(new Rect(0, 0, ClockBackground.width, ClockBackground.height), ClockBackground);
        GUI.BeginGroup(new Rect(5, 6, newBarWidth, ClockForeground.height));
        GUI.DrawTexture(new Rect(0, 0, ClockForeground.width, ClockForeground.height), ClockForeground);
        GUI.EndGroup();
        GUI.EndGroup();
    }

    public void DoCountdown()
    {
        TimeRemaining = StartTime - Time.time;
        Percent = TimeRemaining/StartTime*100;

        if (TimeRemaining < 0)
        {
            TimeRemaining = 0;
            ClockIsPaused = true;
            TimeIsUp();
        }

        ShowTime();
    }

    public void PauseClock()
    {
        ClockIsPaused = true;
    }

    public void UnpauseClock()
    {
        ClockIsPaused = false;
    }

    public void ShowTime()
    {
        int minutes = (int)TimeRemaining/60;
        int seconds = (int)TimeRemaining%60;
        string timeString = minutes + ":";
        timeString += seconds.ToString("D2");
        guiText.text = timeString;
    }

    public void TimeIsUp()
    {
    }
}
