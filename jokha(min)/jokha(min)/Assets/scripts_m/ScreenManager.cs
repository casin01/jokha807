using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] Image image=null;

    [SerializeField] Color c_white=Color.white;
    [SerializeField] Color c_black=Color.black;

    [SerializeField] float fadespeed=0.01f;

    public static bool isfinished = false;
    public static bool isfinished2 = false;
    public GameObject back;

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

    public IEnumerator Fadeout (bool iswhite)
    {
        Color t_color = (iswhite == true) ? c_white : c_black;
        t_color.a = 0;
        image.color = t_color;

        while (t_color.a < 1)
        {
            t_color.a += fadespeed;
            
            image.color = t_color;
            yield return null;
        }
        isfinished = true;
    }

    public IEnumerator Fadein(bool iswhite)
    {
        Color t_color = (iswhite == true) ? c_white : c_black;
        t_color.a = 1;

        image.color = t_color;

        while (t_color.a > 0)
        {
            t_color.a -= fadespeed;
            image.color = t_color;
            yield return null;
        }
        isfinished = true;
    }

    bool Checksamesprite(SpriteRenderer s_spriterenderer, Sprite s_sprite)
    {
        if (s_spriterenderer.sprite == s_sprite)
            return true;
        else
            return false;
    }

    public void SpritechangeCoroutine(string spritename)
    {
        SpriteRenderer t_spriteRenderer = back.GetComponent<SpriteRenderer>();
        Sprite t_sprite = Resources.Load("backgrounds/" + spritename, typeof(Sprite)) as Sprite;

        if (!Checksamesprite(t_spriteRenderer, t_sprite))
        {
            t_spriteRenderer.sprite = t_sprite;
        }
        isfinished2 = true;
    }
}