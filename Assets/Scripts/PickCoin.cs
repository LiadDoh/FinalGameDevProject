using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PickCoin : MonoBehaviour
{
    public AudioSource pickSound;
    public Text coinsTxt;
    public static int numCoins;
    // Start is called before the first frame update
    void Start()
    {
        numCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        numCoins++;
        gameObject.SetActive(false);
        pickSound.Play();
        coinsTxt.text = "Coins : " + numCoins;
    }
}
