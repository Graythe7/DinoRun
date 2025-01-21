using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //we gonna implement a single spawner script to spawn different object all at once

    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] objects;

    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate,maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        float spawnChance = Random.value; // random between 0 amd 1

        foreach (var obj in objects)
        {
            // it's going to loop around different objects and spawn based on their difiend chance 
            if(spawnChance < obj.spawnChance)
            {
                GameObject obstacle = Instantiate(obj.prefab); // creates a copy of prefab
                obstacle.transform.position += transform.position; //offset the position based on the spawner object position
                break;
            }

            //if the spawnchance does not match any of them, then we change the spawnchance's value itself
            spawnChance -= obj.spawnChance;
        }
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
