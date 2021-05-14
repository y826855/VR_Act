using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineNode : MonoBehaviour
{
    public LineNode _nextNode = null;
    public LineNode _beforeNode = null;
    Vector3 _tanNext = Vector3.zero;
    Vector3 _tanBefore = Vector3.zero;


    public float weight = 0.0f;
    public Spline _parent = null;

    //생성됨
    public void Spawn()
    {
        ResetTanget();
    }

    //탄젠트 리셋
    public void ResetTanget()
    {
        _tanBefore = this.transform.position - Vector3.forward * 0.3f;
        _tanNext = this.transform.position + Vector3.forward * 0.3f;
    }

    //위치 얻어오기
    public Vector3 GetSplinePoint(float t)
    {
        //다음 노드 없으면 0넘김
        if (_nextNode == null)
            return Vector3.zero;


        Vector3 P0 = this.transform.position;
        Vector3 P1 = _tanNext;
        Vector3 P2 = _nextNode._tanBefore;
        Vector3 P3 = _nextNode.transform.position;

        //one minuse t
        float omt = 1.0f - t;

        //3차 베지어 미분식?
        //B(t) = (1 - t)^3 * P0 + 3 * (1 - t)^2 * t *  P1 + 3 * (1 - t) * t^2 * P2 + t^3 * P3 

        //p0 * (1 - t)^3  +  
        //p1 * 3 * (1 - t)^2 * t  +  
        //P2 * 3 * (1 - t) * t^2  +  
        //P3 * t ^3

        return
            P0 * Mathf.Pow(omt, 3) +
            P1 * Mathf.Pow(omt, 2) * 3 * t +
            P2 * Mathf.Pow(t, 2) * 3 * omt +
            P3 * Mathf.Pow(t, 3);
    }

}
