using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    [SerializeField] private float lifetime;
    void Start()
    {
        
    }

   
    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
