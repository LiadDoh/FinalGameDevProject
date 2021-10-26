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
        GameObject childObject = findChildFromParent(player.name,"Main Camera", "Rifle");
        GameObject childObject1 = findChildFromParent(ally.name, "", "Rifle (1)");
        childObject.SetActive(true);
        childObject1.SetActive(true);
    }

    GameObject findChildFromParent(string parentName, string subChildNameToFind, string childNameToFind)
    {

        string childLocation;
        if (!subChildNameToFind.Equals(""))
            parentName += "/" + subChildNameToFind;
        childLocation = "/" + parentName + "/" + childNameToFind;
        Debug.Log("Location" + childLocation);
        GameObject childObject = GameObject.Find(childLocation);
        return childObject;
    }
}
