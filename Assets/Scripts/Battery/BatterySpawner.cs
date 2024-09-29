using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterySpawner : MonoBehaviour
{
    [SerializeField] private GameObject batteryPrefab;
    [SerializeField] private Transform spawnerBase;
    [SerializeField] private float spawnRadius = 2f;
    [SerializeField] private string batteryTag = "Battery";

    private List<GameObject> batteries = new List<GameObject>();


     void Start()
    {
        GameObject[] existingBatteries = GameObject.FindGameObjectsWithTag(batteryTag);
        foreach (GameObject battery in existingBatteries)
        {
            batteries.Add(battery);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            DestroyOneBattery();
        }
    }

    public void SpawnBattery()
    {
        if (batteryPrefab != null && spawnerBase != null)
        {
            Vector3 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = spawnerBase.position + randomOffset;

            GameObject newBattery = Instantiate(batteryPrefab, spawnPosition, Quaternion.identity);
            newBattery.tag = batteryTag; 
            batteries.Add(newBattery);
            Debug.Log("Battery Spawned at: " + spawnPosition);
        }
    }

    public void DestroyOneBattery()
    {
        if (batteries.Count > 0)
        {
            GameObject batteryToDestroy = batteries[0];
            if (batteryToDestroy != null)
            {
                Destroy(batteryToDestroy);
                batteries.RemoveAt(0);
                Debug.Log("Battery Destroyed!");
            }

            SpawnBattery();
        }
        else
        {
            Debug.Log("No batteries to destroy!");
        }
    }
}
