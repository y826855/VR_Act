using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CSVWriter : MonoBehaviour
{
    //저장할 데이터 리스트 받자
    static public void WriteCSV(List<ActionData> data) 
    {
        //파일 이름
        var writer = new CSVFileWriter("Assets/Resources/ActionData.csv");

        List<string> columns;
        columns = new List<string>() { "AnimName", "CheckerList", "ActCount", "ActTime" };    //첫줄 헤더
        writer.WriteRow(columns);   //csv 파일 수정
        columns.Clear();            //열 데이터 삭제

        //여기서 부터 데이터 저장
        foreach (var it in data)
        {
            //AnimName
            columns.Add(it._clipName);

            //백터 리스트 병합
            string c = "";
            foreach (var jt in it._checkerList)
                c += jt.ToString();
            //CheckerList
            columns.Add(c);


            //ActCount
            columns.Add(it._actCount.ToString());
            //ActTime
            columns.Add(it._actTime.ToString());

            writer.WriteRow(columns);
            columns.Clear();
        }
        

        writer.Flush();
        writer.Close();
    }
}