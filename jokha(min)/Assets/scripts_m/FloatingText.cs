using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour  {    
    public float destroyTime;   
    public Vector3 RandomizeIntensity = new Vector3(100f, 100f, 0);
    
    void Start() {
        Destroy(gameObject, destroyTime);

        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
            Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y), 0);       
    }
}
