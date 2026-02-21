using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    GameManager gameManager;

    void Start()
    {
        gameObject.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "PlayerShip")
        {
            gameManager.AddPoint();
            gameObject.SetActive(false);
        }
    }
}
