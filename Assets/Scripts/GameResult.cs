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

    void Start()
    {
        playerUIControl = GameObject.FindObjectOfType<PlayerUIControl>();
        LosingPanel.SetActive(false);
        WinningPanel.SetActive(false);
    }
    // Update is called once per frame

    private void setGameOver()
    {
        playerUIControl.enabled = false;

        // yield return new WaitForSeconds(3);

        LosingPanel.SetActive(true);

        game.SetActive(false);

    }

    private void setWin()
    {
        playerUIControl.enabled = false;

        // yield return new WaitForSeconds(3);

        WinningPanel.SetActive(true);

        game.SetActive(false);

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
