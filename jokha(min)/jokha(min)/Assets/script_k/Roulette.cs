using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
{
    public RectTransform wheel;
    public Image finalImg;
    List<Image> contents;

    public Button checkbutton;

    public float initSpeed;
    public float brakeSpeed;
    public float keepSpeedTimeMin, keepSpeedTimeMax;

    float currentTime;
    float currentSpeed;

    bool rolling;
    int result;
    diamanager dm = null;

    void Start()
    {
        dm = FindObjectOfType<diamanager>();
        contents = new List<Image>();
        checkbutton.gameObject.SetActive(false);
        finalImg.gameObject.SetActive(false);
        for (int i = 0; i < wheel.childCount; i++)
        {
            contents.Add(wheel.GetChild(i).GetComponent<Image>());
        }
    }

    void Update()
    {
        if (rolling)
        {
            currentTime -= Time.deltaTime;

            if(currentTime <= 0)
            {
                currentSpeed -= brakeSpeed * Time.deltaTime;
            }

            wheel.Rotate(0, 0, -currentSpeed * Time.deltaTime);

            if(currentSpeed <= 0)
            {
                float halfAng = 360 / contents.Count * 0.5f;
                float minAng = 360;
                Image targeting = null;

                for(int i = 0; i < contents.Count; i++)
                {
                    Vector3 localDir = Quaternion.Euler(0, 0, halfAng + (i * 360 / contents.Count)) * Vector3.down;
                    float ang = Vector3.Angle(wheel.TransformDirection(localDir), Vector3.down);

                    if (ang <= minAng)
                    {
                        minAng = ang;
                        targeting = contents[i];
                        result = i;
                    }
                }
                
                finalImg.sprite = targeting.sprite;
                finalImg.gameObject.SetActive(true);
                rolling = false;
                checkbutton.gameObject.SetActive(true);
            }
        }
    }

    public void checkresult()
    {
        checkbutton.gameObject.SetActive(false);
        finalImg.gameObject.SetActive(false);
        dm.sendresult(result);
        gameObject.SetActive(false);
        dm.ShowDialogue();
    }

    public IEnumerator Roll()
    {
        yield return new WaitForSeconds(1f);
        rolling = true;
        currentSpeed = initSpeed;
        currentTime = Random.Range(keepSpeedTimeMin, keepSpeedTimeMax);
    }

    public void testroll()
    {
        rolling = true;
        currentSpeed = initSpeed;
        currentTime = Random.Range(keepSpeedTimeMin, keepSpeedTimeMax);
    }
}
