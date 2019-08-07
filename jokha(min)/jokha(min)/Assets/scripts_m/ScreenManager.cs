using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] Image fade = null;
    public Text daytext;
    public static bool isfinished = false;
    public static bool isfinished2 = false;
    [SerializeField] GameObject back =null;
    [SerializeField] float fadespeed = 0.02f;
    private Color t_color=Color.black;

    public static ScreenManager instance;
    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton

    public void dayupdate()
    {
        daytext.text = "Day " + Dialogue.dbplayer.Day_;  
    }

    public IEnumerator Fadeout() {
        fade.gameObject.SetActive(true);
        t_color.a = 0;
        fade.color = t_color;
        while (t_color.a < 1) {
            t_color.a += fadespeed;
            fade.color = t_color;
            yield return null;
        }
        isfinished = true;
    }

    public IEnumerator Fadein()  {
        t_color.a = 1;
        fade.color = t_color;
        while (t_color.a > 0) {
            t_color.a -= fadespeed;
            fade.color = t_color;
            yield return null;
        }
        isfinished = true;
        fade.gameObject.SetActive(false);
    }

    bool Checksamesprite(SpriteRenderer s_spriterenderer, Sprite s_sprite)
    {
        if (s_spriterenderer.sprite == s_sprite)
            return true;
        else
            return false;
    }

    public IEnumerator SpritechangeCoroutine(string spritename) //배경에 따라 question 글씨 색깔을 바꾸기
    {
        isfinished = false;
        fade.gameObject.SetActive(true);
        StartCoroutine(Fadeout());
        yield return new WaitUntil(() => isfinished);

        SpriteRenderer t_spriteRenderer = back.GetComponent<SpriteRenderer>();
        Sprite t_sprite = Resources.Load("backgrounds/" + spritename, typeof(Sprite)) as Sprite;

        if (!Checksamesprite(t_spriteRenderer, t_sprite))
        {
            t_spriteRenderer.sprite = t_sprite;
        }

        isfinished = false;
        StartCoroutine(Fadein());
        yield return new WaitUntil(() => isfinished);
        fade.gameObject.SetActive(false);
        isfinished2 = true;
    }
}