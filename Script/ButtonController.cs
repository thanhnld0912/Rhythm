using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer theSR;
    [SerializeField] private Sprite defaultImage;
    [SerializeField] private Sprite pressedImage;

    [SerializeField] private KeyCode keyToPress;

   
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            theSR.sprite = pressedImage;
        }

        if (Input.GetKeyUp(keyToPress))
        {
            theSR.sprite = defaultImage;
        }
    }
}
