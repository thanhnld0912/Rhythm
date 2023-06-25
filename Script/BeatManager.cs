using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatManager : MonoBehaviour
{
    [SerializeField] private float bpm;
    [SerializeField] private AudioSource theMusic;
    [SerializeField] private Intervals[] _intervals;
    [SerializeField] private NoteObject noteObject;
    [SerializeField] private NoteObject noteObjectPrefab;

    private void Start()
    {
        noteObject = Instantiate(noteObjectPrefab);
    }

    private void Update()
    {
        foreach (Intervals interval in _intervals)
        {
            float sampledTime = (theMusic.timeSamples / (theMusic.clip.frequency * interval.GetIntervalLength(bpm)));
            interval.CheckForNewInterval(sampledTime, noteObject, bpm);
        }
    }
}

[System.Serializable]
public class Intervals
{
    [SerializeField] private float _steps;
    [SerializeField] private UnityEvent _trigger;
    private int _lastInterval;

    public float GetIntervalLength(float bpm)
    {
        return 60f / (bpm * _steps);
    }

    public void CheckForNewInterval(float interval, NoteObject noteObject, float bpm)
    {
        int currentInterval = Mathf.FloorToInt(interval);

        if (currentInterval != _lastInterval)
        {
            _lastInterval = currentInterval;
            float beatTime = interval - _lastInterval;
            float fallTime = beatTime / GetIntervalLength(bpm);
            float fallDistance = noteObject.fallSpeed * fallTime;

            noteObject.transform.position = new Vector3(noteObject.transform.position.x, noteObject.initialPositionY - fallDistance, noteObject.transform.position.z);
        }
    }
}