using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 class PlayerActData
{
    public string _clipName = "";
    public int _count = 0;
    public float _actTime = 0.0f;
    public float _Accuracy = 0.0f;
}
*/

public class CSV_WritePlayerData : MonoBehaviour
{
    static public void WriteCSV(List<PlayerActData> data)
    {
        //파일 이름
        var writer = new CSVFileWriter("Assets/Resources/PlayerData.csv");

        List<string> columns;
        columns = new List<string>() { "AnimName", "ActTime", "Accuracy"};    //첫줄 헤더
        writer.WriteRow(columns);   //csv 파일 수정
        columns.Clear();            //열 데이터 삭제

        foreach (var it in data)
        {
            columns.Add(it._clipName + it._count.ToString());
            columns.Add(it._actTime.ToString());
            columns.Add(it._Accuracy.ToString());

            writer.WriteRow(columns);
            columns.Clear();
        }


        writer.Flush();
        writer.Close();
    }
}
