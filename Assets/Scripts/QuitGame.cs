using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void OnQuitPressed()
    {
        FindObjectOfType<SoundController>().Play("Play");
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
