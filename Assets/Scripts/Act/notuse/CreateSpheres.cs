using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSpheres : MonoBehaviour
{

    public GameObject _col;

    //생성 위치
    public Transform _handL = null;
    public Transform _handR = null;
    public Transform _head = null;

    //콜라이더 리스트
    public List<GameObject> _colList = new List<GameObject>();


    public GameObject _parent = null;


    bool _isChecking = false;
    Coroutine _creator = null;


    //콜라이더 생성
    public void InstCol()
    {
        GameObject handL = Instantiate(_col);
        GameObject handR = Instantiate(_col);

        handL.transform.SetParent(_parent.transform);
        handR.transform.SetParent(_parent.transform);

        handL.transform.localPosition = _handL.position - _head.position;
        handR.transform.localPosition = _handR.position - _head.position;

        _colList.Add(handL);
        _colList.Add(handR);
    }


    //지정된 시간동안 콜리전 생성
    IEnumerator CreateCol()
    {
        InstCol();

        yield return new WaitForSecondsRealtime(0.03f);
        if (_isChecking) _creator = StartCoroutine("CreateCol");
    }


    

    //콜리전 생성 시작
    public void StartCheck()
    {
        _isChecking = true;
        _creator = StartCoroutine("CreateCol");
    }


    //콜리전 생성 종료
    public void StopCheck()
    {
        _isChecking = false;
        StopCoroutine(_creator);


        foreach (var it in _colList)
        {
            Destroy(it);
            //DestroyImmediate(it);
        }
        _colList.Clear();
    }

}


/*
 구 콜라이더를 구 모양으로 만들어 
     
     */


    /*
     캐릭터는 머리에서 손까지 선 그림

    플레이어 역시 머리부터 손까지 선 그림

    그 선을 비교하자. 머리에서 손까지의 방향 어느정도 차이나는지,
    거리는 어느정도 차이나는지.
    
    처음에 양팔 좌우로 벌려서 팔 길이 체크하자.
    

    오큘러스 퀘스트 손 인식 범위 생각해보자

     
     */