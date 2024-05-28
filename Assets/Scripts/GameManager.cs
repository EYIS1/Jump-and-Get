using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] towerArray;
    public GameObject firstTower;
    private CameraFollow cameraFollow;

    private GameObject[] towers;

    private int previousVariable = 0;

    public float spawnDistanceX = 2.5f; // Расстояние от игрока, на котором спавнится объект
    public float spawnDistanceY = 4.5f; // Расстояние от игрока, на котором спавнится объект


    private int random;

    void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        previousVariable = cameraFollow.variable;
    }

    void Update()
    {
        towers = GameObject.FindGameObjectsWithTag("Tower");
        if (previousVariable != cameraFollow.variable)
        {
            Vector3 playerPosition = cameraFollow.target.position;
            Vector3 spawnPosition = playerPosition + new Vector3(spawnDistanceX, spawnDistanceY, 0);

            random = UnityEngine.Random.Range(0, towerArray.Length); // Исправление здесь
            Instantiate(towerArray[random], spawnPosition, Quaternion.identity);
            previousVariable = cameraFollow.variable;

            if (previousVariable > 1) 
            {
                StartCoroutine(enumerator(towers[0].gameObject));
            }
            else
            {
                StartCoroutine(enumerator(firstTower.gameObject));
            }
        }
    }

    private IEnumerator enumerator(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}
