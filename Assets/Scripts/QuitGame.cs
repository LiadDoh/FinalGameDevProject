using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void OnQuitPressed()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
