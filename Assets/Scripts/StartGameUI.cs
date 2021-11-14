using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameUI : MonoBehaviour
{

    public void Click()
    {
        FindObjectOfType<SoundController>().Play("Play");   
    }
}
