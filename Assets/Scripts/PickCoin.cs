using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PickCoin : MonoBehaviour
{
    public GameObject rifle1;
    public GameObject rifle2;
    public GameObject rifle3;
    public GameObject rifle4;

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        Debug.Log(other.tag);
        if (other.tag == "FirstEnemy" || other.tag == "SecondEnemy")
        {
            rifle3.SetActive(true);
            rifle4.SetActive(true);
        } 

    }
}
