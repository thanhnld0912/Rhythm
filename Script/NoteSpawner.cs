using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public bool isTimer;

    public List<GameObject> notesSpawn = new List<GameObject>();

    public float timetoSpawn;
    private float currentTimeToSpawn;

    public bool isRandomized;
    void Start()
    {
        currentTimeToSpawn = timetoSpawn;
    }
    // Update is called once per frame
    void Update()
    {
        if (isTimer)
        {
            UpdateTimer();

        }
    }
    private void UpdateTimer()
    {
        if (currentTimeToSpawn > 0)
        {
            currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnNote();
            currentTimeToSpawn = timetoSpawn;
        }
    }
    public void SpawnNote()
    {
        int index = isRandomized ? Random.Range(0, notesSpawn.Count) : 0;
        if(notesSpawn.Count > 0)
        {
            Instantiate(notesSpawn[index], transform.position, transform.rotation);

        }
    }
}
