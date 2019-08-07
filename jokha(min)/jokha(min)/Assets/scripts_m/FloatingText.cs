using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour  {    
    public float destroyTime;   
    public Vector3 RandomizeIntensity = new Vector3(100f, 200f, 0);
//    Color textcolor=Color.black;

    void Start() {
        Destroy(gameObject, destroyTime);

        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
        Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y), 0);
    }

    /*object pooling
    public string ChangeText(int ind)
    {
        switch (ind)
        {
            case 0:
                textcolor = Color.red;
                return "지능";
            case 1:
                textcolor = Color.black;
                return "인맥";
            case 2:
                textcolor = Color.green;
                return "돈";
            case 3:
                textcolor = Color.blue;
                return "스트레스";
            case 4:
                textcolor = Color.magenta;
                return "체력";
            default:
                Debug.Log("default");
                textcolor = Color.clear;
                return "";
        }
    }

    public void f (int ind, int val)
    {
        string text = ChangeText(ind);
        if (val > 0) GetComponent<TextMesh>().text = text + " +" + val;
        else GetComponent<TextMesh>().text = text + " " + val;
        GetComponent<TextMesh>().color = textcolor;
        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
        Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y), 0);
        StartCoroutine(FF());
    }

    IEnumerator FF()
    {
        GetComponent<Animator>().SetBool("floating", true);
        yield return new WaitForSeconds(0.6f);
        GetComponent<Animator>().SetBool("floating", false);
        transform.localPosition = Vector3.back;
    }
    */
}
