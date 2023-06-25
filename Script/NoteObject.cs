using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    [SerializeField] private bool canBePressed;
    [SerializeField] private KeyCode keyToPress;

    public GameObject hiteffect, goodeffect, perfecteffect, misseffect;

    public float initialPositionY = 5f;
    public float fallSpeed = 5f;

    private Vector3 defaultScale;
    private bool isPulsing;
    private float timer;

    void Start()
    {
        defaultScale = transform.localScale;
        isPulsing = false;
        timer = 0f;

        // Set initial position
        transform.position = new Vector3(transform.position.x, initialPositionY, transform.position.z);
    }

    void Update()
    {
        timer += Time.deltaTime;
        float fallDistance = fallSpeed * Time.deltaTime;
        transform.position -= new Vector3(0f, fallDistance, 0f);

        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);

                if (Mathf.Abs(transform.position.y) > 0.25)
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(hiteffect, transform.position, hiteffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) > 0.05f)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodeffect, transform.position, goodeffect.transform.rotation);

                }
                else
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfecteffect, transform.position, perfecteffect.transform.rotation);

                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
            if (!isPulsing)
            {
                StartCoroutine(Pulse());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && gameObject.activeSelf)
        {
            canBePressed = false;
            StopCoroutine(Pulse());
            transform.localScale = defaultScale;

            Instantiate(misseffect, transform.position, misseffect.transform.rotation);

            GameManager.instance.NoteMiss();
        }
    }

    IEnumerator Pulse()
    {
        isPulsing = true;

        while (true)
        {
            float pulseTime = GameManager.instance.beatInterval * 0.75f;

            float scaleTime = pulseTime / 2f;

            float currScale = transform.localScale.x;

            // Scale up
            float t = 0;
            while (t < scaleTime)
            {
                t += Time.deltaTime;
                float scale = Mathf.Lerp(currScale, defaultScale.x + 0.25f, t / scaleTime);
                transform.localScale = new Vector3(scale, scale, scale);
                yield return null;
            }

            // Scale down
            t = 0;
            while (t < scaleTime)
            {
                t += Time.deltaTime;
                float scale = Mathf.Lerp(transform.localScale.x, defaultScale.x, t / scaleTime);
                transform.localScale = new Vector3(scale, scale, scale);
                yield return null;
            }

            yield return new WaitForSeconds(pulseTime - scaleTime);
        }
    }

    public void SetInitialPosition(float positionY)
    {
        initialPositionY = positionY;
        transform.position = new Vector3(transform.position.x, initialPositionY, transform.position.z);
    }
    public void Initialize(float beatInterval, float fallSpeed)
    {
        this.fallSpeed = fallSpeed;
        GameManager.instance.beatInterval = beatInterval;
    }

    public void SetFallSpeed(float speed)
    {
        fallSpeed = speed;
    }
}
