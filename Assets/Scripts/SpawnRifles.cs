using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRifles : MonoBehaviour
{
    public List<GameObject> positions;
    public GameObject originalRifle;

    private int amountOfRifles;
    private List<GameObject> spawnedPositions;

    // Start is called before the first frame update
    void Start()
    {
        spawnedPositions = new List<GameObject>();
        amountOfRifles = positions.Count/2 + 1;
        while (positions.Count >= amountOfRifles && positions.Count > 0)
        {
            int index = Random.Range(0, positions.Count - 1);
            Debug.Log("Instantiating rifle in :" + positions[index].name);
            GameObject newRifle = Instantiate(originalRifle);
            newRifle.transform.position = positions[index].transform.position;
            spawnedPositions.Add(positions[index]);
            positions.RemoveAt(index);
        }

        GetComponent<EnemiesFollow>().getClosestRiflePosition(spawnedPositions);

    }


}
