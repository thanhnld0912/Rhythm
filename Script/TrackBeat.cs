using UnityEngine;
using UnityEngine.Events;

public class TrackBeat : MonoBehaviour
{
    [SerializeField] private bool startPlaying;
    [SerializeField] private float _bpm;
    [SerializeField] private AudioSource _audiosource;
    [SerializeField] private Intervalsed[] _intervals;

    private void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;

                _audiosource.Play();
            }
        }
        foreach (Intervalsed interval in _intervals)
        {
            float sampledTime = (_audiosource.timeSamples / (_audiosource.clip.frequency * interval.GetIntervalLength(_bpm)));
            interval.CheckForNewInterval(sampledTime);
        }
    }

}
    [System.Serializable]
    public class Intervalsed
    {
        [SerializeField] private float _steps;
        [SerializeField] private UnityEvent _trigger;
        private int _lastInterval;

        public float GetIntervalLength(float bpm)
        {
            return 60f / (bpm * _steps);
        }
        public void CheckForNewInterval(float interval)
        {
            if(Mathf.FloorToInt(interval) != _lastInterval)
            {
                _lastInterval = Mathf.FloorToInt(interval);
                _trigger.Invoke();
            }
        }
    }

