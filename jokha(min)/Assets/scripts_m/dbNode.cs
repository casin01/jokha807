using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(menuName = "Dialogue/dbmanager")]
    public class dbNode : ScriptableObject
    {

        [System.Serializable]
        public class Choicelist
        {
            public bool[] ch;
        }

        public static int day=0;

        [SerializeField] Choicelist [] c;
        
    }
}
