using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameObjects : MonoBehaviour
{
    public List<GameObject> positions;
    public GameObject player;

    public GameObject friendlyNPC;

    public GameObject firstEnemyNPC;

    public GameObject secondEnemyNPC;
    public GameObject rifle;

    private int amountOfRifles;
    private List<GameObject> riflePositions;

    // Start is called before the first frame update
    void Start()
    {
        spawnRiflePositions();
        setPositionOfPlayerAndNPCs();


    }

    private void spawnRiflePositions()
    {
        riflePositions = new List<GameObject>();
        amountOfRifles = positions.Count / 2 + 1;
        while (positions.Count >= amountOfRifles && positions.Count > 0)
        {
            int index = Random.Range(0, positions.Count - 1);
            Debug.Log("Instantiating rifle in :" + positions[index].name);
            GameObject newRifle = Instantiate(rifle);
            newRifle.transform.position = positions[index].transform.position;
            riflePositions.Add(positions[index]);
            positions.RemoveAt(index);
        }

        firstEnemyNPC.GetComponent<EnemiesFollow>().getClosestRiflePosition(riflePositions);
    }

    private void setPositionOfPlayerAndNPCs()
    {
        int randomIndex = Random.Range(0, positions.Count - 1);
        player.transform.position = new Vector3(positions[randomIndex].transform.position.x, positions[randomIndex].transform.position.y, positions[randomIndex].transform.position.z);
        friendlyNPC.transform.position = new Vector3(player.transform.position.x + 1f, player.transform.position.y + 1.5f, player.transform.position.z);
        Debug.Log("Player and friendlyNPC spawned at: " + positions[randomIndex].name);

        positions.RemoveAt(0);

        randomIndex = Random.Range(0, positions.Count - 1);
        firstEnemyNPC.transform.position = new Vector3(positions[randomIndex].transform.position.x, positions[randomIndex].transform.position.y + 1.5f, positions[randomIndex].transform.position.z);
        secondEnemyNPC.transform.position = new Vector3(firstEnemyNPC.transform.position.x + 1f, firstEnemyNPC.transform.position.y, firstEnemyNPC.transform.position.z);
        Debug.Log("First and second enemyNPC spawned at: " + positions[randomIndex].name);
        positions.RemoveAt(0);
    }
}
