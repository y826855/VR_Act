using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//동작 정보
public class ActionData
{
    public string _clipName = "";       //애님 이름
    public List<Vector3> _checkerList = new List<Vector3>(); //체커 리스트
    public int _actCount = 0;           //플레이어 동작 횟수
    public float _actTime = 0.0f;       //체크 지속 시간
}

//플레이어 정보 저장
/*
public class PlayerData
{
    public string _clipName = "";       //애님 이름
    public int _actCount = 0;              //플레이어 동작 횟수
    public float _checkPersent = 0.0f;  //플레이어 체커 정확도
    public float _actTime = 0.0f;    //한 동작 얼마나 걸렸나
}*/


public class CreateChecker : MonoBehaviour
{
    //체커 풀링
    public List<GameObject> _pull = new List<GameObject>();
    //생성될 체커
    public GameObject checker = null;

    int _currActive = 0;
   


    //체커 생성
    public void CheckerCreate()
    {
        foreach (var it in _pull) DestroyImmediate(it);
        _pull.Clear();
        for (int i = 0; i < 300; i++)
        {
            _pull.Add(Instantiate(checker, this.transform));
        }

        _currActive = 0;
    }

    //하나 활성화
    public void SpawnChecker(Vector3 pos, bool isLeft = false)
    {
        //풀에서 가져옴
        _pull[_currActive].transform.position = pos;
        _pull[_currActive].gameObject.SetActive(true);


        //왼손 레이어
        if (isLeft)
            _pull[_currActive].gameObject.layer = LayerMask.NameToLayer("LeftChecker");
        //오른손 레이어
        else
            _pull[_currActive].gameObject.layer = LayerMask.NameToLayer("RightChecker");

        //풀링
        _currActive++;
        if (_currActive > _pull.Count - 1)
            _currActive = 0;   
    }


    //모두 비활성화
    public void RemoveAll()
    {
        foreach (var it in _pull)
        {
            if (it.gameObject.activeSelf) it.gameObject.SetActive(false);
        }
        _currActive = 0;
    }


}
