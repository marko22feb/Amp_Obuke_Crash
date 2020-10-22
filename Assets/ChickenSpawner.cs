using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawner : MonoBehaviour
{
    public GameObject Spawned;
    public List<GameObject> CurrentlySpawned;
    public GameObject locationSpawn;

    private void Start()
    {
        CheckIfNeedToSpawn();
    }

    void CheckIfNeedToSpawn()
    {
        if (CurrentlySpawned.Count < 5)
        {
            int amount = 5 - CurrentlySpawned.Count;
            for (int i = 0; i < amount; i++)
            {
                Spawn();
            }
        }
    }

    void Spawn()
    {
       GameObject spawn = Instantiate(Spawned);
        spawn.transform.position = locationSpawn.transform.position;
        spawn.GetComponent<CritterController>().ChickenHut = this.gameObject;
        CurrentlySpawned.Add(spawn);
    }

    public void Despawn(GameObject gob)
    {
        CurrentlySpawned.Remove(gob);
        CheckIfNeedToSpawn();
    }
}
