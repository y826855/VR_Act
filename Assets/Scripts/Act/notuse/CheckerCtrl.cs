using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerCtrl : MonoBehaviour
{

    //생성 위치
    public Transform _handL = null;
    public Transform _handR = null;
    public Transform _head = null;

    public CreateChecker _checker = null;


    int _numCorrect = 0;

    bool _isChecking = false;
    Coroutine _creator = null;

    Animator _anim = null;


    private void Start()
    {
        _anim = this.GetComponent<Animator>();
    }

    //지정된 시간동안 콜리전 생성
    IEnumerator CreateCol()
    {
        InstCol();

        yield return new WaitForSecondsRealtime(0.03f);
        if (_isChecking) _creator = StartCoroutine("CreateCol");
    }

    //체커 생성
    public void InstCol()
    {
        Debug.Log(_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        
        _checker.SpawnChecker(_handL.transform.position);
        _checker.SpawnChecker(_handR.transform.position);
        _numCorrect++;
    }

    //체커 생성 시작
    public void StartCheck()
    {
        _numCorrect = 0;
        _isChecking = true;
        _creator = StartCoroutine("CreateCol");
    }

    //체커 제거
    public void StopCheck()
    {
        _isChecking = false;
        _checker.RemoveAll();
    }
}


/*
 
    TODO :
     정답 갯수 저장하기
     
     저장한 정답 리스트로 가지고 있자

     점수로 환산해보자

     */