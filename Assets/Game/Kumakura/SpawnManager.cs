using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> animalPrefabs;

    private float spawnRangeX = 20;
    private float spawnPosZ = 20;
    private float spawnInterval = 1.5f;
    private float passedTime = -2f;

    void Update()
    {
        passedTime += Time.deltaTime;
        if(passedTime > spawnInterval)
        {
            SpawnRandomAnimalBox();
            passedTime = 0;
        }
    }

    private void SpawnRandomAnimalBox()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
        int animalIndex = Random.Range(0, animalPrefabs.Count);
        GameObject animal = animalPrefabs[animalIndex];
        Instantiate(animal, spawnPos, animal.transform.rotation);
    }
}