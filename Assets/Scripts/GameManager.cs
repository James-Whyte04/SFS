using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] checkpoints;
    int checkpointTracker = 0;

    void Start()
    {
        checkpoints[checkpointTracker].SetActive(true);
    }

    void Update()
    {
        checkpoints[checkpointTracker].SetActive(true);
    }

    public void AddPoint()
    {
        checkpointTracker++;
    }
}
