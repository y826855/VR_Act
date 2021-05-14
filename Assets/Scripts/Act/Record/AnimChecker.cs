using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimChecker : MonoBehaviour
{

    //생성 위치
    public Transform _handL = null;
    public Transform _handR = null;
    public Transform _head = null;


    //데이터
    public List<ActionData> _data = new List<ActionData>();
    List<Vector3> _checklist = new List<Vector3>();


    bool _isChecking = false;
    Coroutine _creator = null;

    Animator _anim = null;

    //RecordChecker _recorder = null;

    private void Start()
    {
        _anim = this.GetComponent<Animator>();

        //_recorder = GameObject.FindGameObjectWithTag("AnimRecorder").GetComponent<RecordChecker>();
    }

    //지정된 시간동안 체크
    IEnumerator CreateCol()
    {
        InstCol();

        yield return new WaitForSecondsRealtime(0.03f);
        if (_isChecking) _creator = StartCoroutine("CreateCol");
    }

    //체커 생성
    public void InstCol()
    {
        _checklist.Add(_handL.transform.position);
        _checklist.Add(_handR.transform.position);

        //Debug.Log(_checklist.);
    }

    //체커 생성 시작
    public void StartCheck()
    {
        _checklist.Clear();
        _isChecking = true;
        _creator = StartCoroutine("CreateCol");
    }

    //체커 제거
    public void StopCheck()
    {
        _isChecking = false;
        Debug.Log(_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);

        ActionData temp = new ActionData();
        temp._clipName = _anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        temp._checkerList = new List<Vector3>(_checklist);

        Debug.Log(_checklist.Count);
        Debug.Log(temp._checkerList.Count);

        temp._actTime = 3;
        temp._actCount = 1;

        _data.Add(temp);
    }

    public void RecordEnd()
    {
        CSVWriter.WriteCSV(_data);

        Debug.Log("record end");
    }
}


/*
 
*/

