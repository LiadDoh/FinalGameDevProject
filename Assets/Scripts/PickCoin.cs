using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PickCoin : MonoBehaviour
{
    public GameObject rifle1;
    public GameObject rifle2;

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        rifle1.SetActive(true);
        rifle2.SetActive(true);
    }
}
