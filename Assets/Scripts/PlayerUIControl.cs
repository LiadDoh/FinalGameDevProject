using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class PlayerUIControl : MonoBehaviour
{
    public Text objective;
    public Text state;

    public Text health;

    private float timeToAppear = 2f;
    private float timeWhenDisappear;
    private bool isStateTextActive = false;

    private string maxHealth = " / 100";

    private void Update()
    {
        if (isStateTextActive && (Time.time >= timeWhenDisappear))
        {
            isStateTextActive = false;
            state.text = "";
        }
    }

    public void setObjectiveText(string objText)
    {
        objective.text = objText;
    }

    public string getObjectiveText()
    {
        return objective.text;
    }

    public void setStateText(string sttText)
    {
        Debug.Log("setStateText" + sttText);
        state.text = sttText;
        timeWhenDisappear = Time.time + timeToAppear;
        isStateTextActive = true;
    }

    public void setHealthText(float damage)
    {
        float prevHealth = float.Parse(health.text.Split('/')[0], CultureInfo.InvariantCulture.NumberFormat);
        health.text = (prevHealth - damage).ToString() + maxHealth;
    }
}