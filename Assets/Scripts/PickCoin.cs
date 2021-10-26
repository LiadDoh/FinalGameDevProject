using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PickCoin : MonoBehaviour
{
    public GameObject player;
    public GameObject ally;

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        GameObject child1 = player.transform.GetChild(0).transform.GetChild(0).gameObject;
        GameObject child2 = ally.transform.GetChild(1).gameObject;
        child1.SetActive(true);
        child2.SetActive(true);
    }
}
