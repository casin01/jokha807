using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dialogue {
    [CreateAssetMenu(menuName = "Dialogue/player")]
    public class dbplayer : ScriptableObject
    {
        [SerializeField] int[] stat;
        [SerializeField] int[] countdb;
        public int[] Stat { get => stat; set => stat = value; }
        public int[] Con { get => countdb; set => countdb = value; }    //횟수들 저장값

        static int day = 1; //진행날짜
        public static int Day_ { get => day; set => day = value; }
        [SerializeField] static int endnum;

        [SerializeField] bool[] jok_p = null;
        [SerializeField] bool[] status_p=null;
        [SerializeField] bool[] ending_p = null;

        public void initializ() //초기화
        {
            jok_p.Initialize();
            status_p.Initialize();

        }

        public bool checking(int ind)
        {
            if (ind == 1) return true;
            else return false;
        }

        public void setzero(int ind)
        {
            Con[ind] = 0;
        }

        public bool statGreater(int ind, int val)
        {
            if (stat[ind] >= val) return true;
            else return false;
        }

        public bool conGreater(int ind, int val)
        {
            if (Con[ind] >= val) return true;
            else return false;
        }

        public bool jokchecking(int ind)
        {
            if (!jok_p[ind]) return true;
            else return false;
        }

        public bool statuschecking(int ind)
        {
            if (!status_p[ind]) return true;
            else return false;
        }

        public bool endchecking(int ind)
        {
            if (!ending_p[ind]) return true;
            else return false;
        }

        public void jokpop(int ind)
        {
            dbmanager db = FindObjectOfType<dbmanager>();
            db.popjok();
            jok_p[ind] = true;
        }

        public void statuspop(int ind)
        {
            dbmanager db = FindObjectOfType<dbmanager>();
            db.popstatus(ind);
            status_p[ind] = true;
            Time.timeScale = 0;
        }

        public void startending(int endn, string endname="mainend" )
        {
            if (!ending_p[endn]) ending_p[endn] = true;
            endnum = endn;  //엔딩번호 (씬 바뀌면서 넘겨주는 값
            if (endname == "") endname = "mainend";
            SceneManager.LoadScene(endname);
        }
    }
}
