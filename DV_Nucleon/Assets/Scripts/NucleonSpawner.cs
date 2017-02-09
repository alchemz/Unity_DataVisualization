using UnityEngine;
//Connect each point with a line
public class NucleonSpawner : MonoBehaviour
{
    public float timeBetweenSpawns;

    public float spawnDistance;

    public Nucleon[] nucleonPrefabs;
    float timeSinceLastSpawn;
    public 
    void FixedUpdate()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= timeBetweenSpawns)
        {
            timeSinceLastSpawn -= timeBetweenSpawns;
            SpawnNucleon();
        }
    }

    void SpawnNucleon()
    {
        Nucleon prefab = nucleonPrefabs[Random.Range(0, nucleonPrefabs.Length)];
        Nucleon spawn = Instantiate<Nucleon>(prefab);
        spawn.transform.localPosition = Random.onUnitSphere * spawnDistance;
    }

}

