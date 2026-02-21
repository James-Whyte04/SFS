using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{

    void Start()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "PlayerShip")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
