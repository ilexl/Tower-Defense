using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Path path;
    public PathSpawn[] levels;
    
    PathSpawn currentWave;
    int waveInternal = 0;
    float lastSpawnTime = 0;

    [CreateAssetMenu(menuName = "Game/Wave")]
    public class PathSpawn : ScriptableObject
    {
        public GameObject[] enemy;
        public float[] timeBetweenEnemy;

        public PathSpawn(GameObject[] _enemy, float[] _timeBetweenEnemy)
        {
            enemy = _enemy;
            timeBetweenEnemy = _timeBetweenEnemy;
        }
    }

    public void Wave(PathSpawn spawn)
    {
        waveInternal = 0;
        lastSpawnTime = 0;
        currentWave = spawn;
    }

    public PathSpawn NextWave()
    {
        Debug.Log("Next Wave!");
        return null;
    }

    public void SpawnOnPath(GameObject toSpawn, Path path)
    {
        Debug.Log("Spawned @");
        GameObject spawned = Instantiate(toSpawn, this.transform);
        spawned.GetComponent<FollowPath>().SetPath(path);
    }

    public void Update()
    {
        if(currentWave != null)
        {
            if(waveInternal < currentWave.enemy.Length)
            {
                if(lastSpawnTime > currentWave.timeBetweenEnemy[waveInternal])
                {
                    // Spawn next enemy on this path
                    SpawnOnPath(currentWave.enemy[waveInternal], path);
                    waveInternal++;
                }
                else
                {
                    // Add time
                    lastSpawnTime += Time.deltaTime;
                }
            }
            else
            {
                Wave(NextWave());
            }
        }
        foreach (Transform t in transform)
        {
            if (!t.gameObject.activeSelf)
            {
                // delete inactive enemies
                GameObject.Destroy(t.gameObject);
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SpawnManager))]
public class EDITOR_SpawnManager : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SpawnManager manager = (SpawnManager)target;
        
        GUILayout.Space(10);
        
        if(GUILayout.Button("TEST SPAWN"))
        {
            manager.Wave(manager.levels[0]);
        }
    }
}
#endif