using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCSV : MonoBehaviour
{
    static public readonly List<ActionData> _AllAction = new List<ActionData>();

    void Awake()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("ActionData");

        //AnimName	CheckerList	ActCount	ActTime

        ActionData temp = null;

        //데이터 불러오기
        foreach (var it in data)
        {
            temp = new ActionData();

            temp._clipName = it["AnimName"].ToString();


            //string to vector
            string all = it["CheckerList"].ToString();
            string[] sVec = all.Split(')');
            foreach (var jt in sVec)
            {
                if (jt.Length <= 0) continue;
                string s = jt.Substring(1, jt.Length - 1);
                string[] sFloat = s.Split(',');

                Vector3 res = new Vector3(
                    float.Parse(sFloat[0]),
                    float.Parse(sFloat[1]),
                    float.Parse(sFloat[2])
                    );
                temp._checkerList.Add(res);
            }

            temp._actCount = (int)it["ActCount"];
            temp._actTime = (int)it["ActTime"];

            _AllAction.Add(temp);
            Debug.Log(temp._clipName);
        }
    }

    
    void Update()
    {
        
    }
}
