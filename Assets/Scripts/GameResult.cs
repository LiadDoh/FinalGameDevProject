using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResult : MonoBehaviour
{
    public GameObject WinningPanel;
    public GameObject LosingPanel;

    public GameObject game;
    int enemyDeadCount = 0;

    bool isGameOver = false;

    private PlayerUIControl playerUIControl;

    private float timeToAppear = 4f;
    private float timeWhenDone;

    void Start()
    {
        playerUIControl = GameObject.FindObjectOfType<PlayerUIControl>();
        LosingPanel.SetActive(false);
        WinningPanel.SetActive(false);
    }
    // Update is called once per frame

    void Update()
    {
        if ((isGameOver || enemyDeadCount > 1) && (Time.time >= timeWhenDone))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }
    }


    private void setGameOver()
    {
        playerUIControl.enabled = false;

        LosingPanel.SetActive(true);

        game.SetActive(false);

        timeWhenDone = Time.time + timeToAppear;


    }

    private void setWin()
    {
        playerUIControl.enabled = false;

        WinningPanel.SetActive(true);

        game.SetActive(false);

        timeWhenDone = Time.time + timeToAppear;

    }

    public void addEnemyCount()
    {
        enemyDeadCount++;
        Debug.Log("enemy count: " + enemyDeadCount);
        if (enemyDeadCount > 1 && !isGameOver)
        {
            setWin();
        }
    }

    public void setGameOverFlag()
    {
        isGameOver = true;
        if (enemyDeadCount <= 1)
        {
            Debug.Log("Game Over");
            setGameOver();
        }
    }
}
