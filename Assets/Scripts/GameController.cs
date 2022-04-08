using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] obstaclePrefabs = new GameObject[2];
    public int countOfObstacles, rangeOfObstacles;

    void Start()
    {
        for (int i = 0; i < countOfObstacles; i++)
        {
            Instantiate(obstaclePrefabs[Random.Range(0,2)], new Vector3(Random.Range(-rangeOfObstacles, rangeOfObstacles), 0.5f, Random.Range(-rangeOfObstacles, rangeOfObstacles)), Quaternion.identity);
        }
    }
}
