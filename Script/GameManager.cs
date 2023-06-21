using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource theMusic;

    [SerializeField] private bool startPlaying;

    [SerializeField] private BeatScroller theBS;

    public static GameManager instance;

    [SerializeField] private int currentScore;
    [SerializeField] private int ScorePerNote = 100;
    [SerializeField] private int ScorePerGoodNote = 125;
    [SerializeField] private int ScorePerPerfectNote = 150;


    [SerializeField] private int currentMultiplier;
    [SerializeField] private int multiplierTracker;
    [SerializeField] private int[] multiplierThresholds;

    public Text scoreText;
    public Text multiText;

    [SerializeField] private float totalNotes;
    [SerializeField] private float normalHits;
    [SerializeField] private float goodHits;
    [SerializeField] private float perfectHits;
    [SerializeField] private float missedHits;

    public GameObject resultScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;

    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }


    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;

                theMusic.Play();
            }
        }
        else
        {
            if(!theMusic.isPlaying && !resultScreen.activeInHierarchy){
                resultScreen.SetActive(true);

                normalsText.text = "" + normalHits;
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missesText.text = "" + missedHits;

                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100f;

                percentHitText.text = percentHit.ToString("F1") + "%";

                string rankVal = "F";

                if(percentHit > 40)
                {
                    rankVal = "D";
                    if(percentHit > 55)
                    {
                        rankVal = "C";
                        if(percentHit > 70)
                        {
                            rankVal = "B";
                            if(percentHit > 85)
                            {
                                rankVal = "A";
                                if(percentHit > 95)
                                {
                                    rankVal = "SS";
                                }
                            }
                        }
                    }
                }
                rankText.text = rankVal;

                finalScoreText.text = currentScore.ToString();
            }
        }
    }

    public void Notehit()
    {
        Debug.Log("Hit On Time");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        multiText.text = "Multiplier: x" + currentMultiplier;


        currentScore += ScorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += ScorePerNote * currentMultiplier;
        Notehit();

        normalHits++;
    }

    public void GoodHit()
    {
        currentScore += ScorePerGoodNote * currentMultiplier;

        Notehit();
        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore = ScorePerPerfectNote * currentMultiplier;

        Notehit();

        perfectHits++;
    }
    public void NoteMiss()
    {
        Debug.Log("Missed Note");

        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "Multiplier: x" + currentMultiplier;

        missedHits++;
    }
}
