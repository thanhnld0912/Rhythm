using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    [SerializeField] private bool canBePressed;

    [SerializeField] private KeyCode keyToPress;


    public GameObject hiteffect, goodeffect, perfecteffect, misseffect;
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);

                //GameManager.instance.Notehit();

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
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
            if (other.tag == "Activator" && gameObject.activeSelf)
            {
                canBePressed = false;

                Instantiate(misseffect, transform.position, misseffect.transform.rotation);

                GameManager.instance.NoteMiss();
            }
        }
    }
