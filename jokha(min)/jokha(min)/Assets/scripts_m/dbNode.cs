using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dialogue
{
    [CreateAssetMenu(menuName = "Dialogue/dbmanager")]
    public class dbNode : ScriptableObject
    {
        [System.Serializable]
        public class Choicelist  { public bool[] ch; }

        [SerializeField] Choicelist [] c;
        [SerializeField] int[] mycon= new int [1];
        [SerializeField] static int endnum;

        public void startending(int ind, int endn)
        {
            endnum = endn;  //엔딩번호
            SceneManager.LoadScene(ind);
        }

    }
}
