using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TestMover : MonoBehaviour
{

    public Vector3 _lastPos = Vector3.zero;
    public Vector3 _moveDelta = Vector3.zero;

    //민감도
    [Header("클수록 반응 민감해짐")]
    public float _senser = 1.0f;

    //파워
    public float _currentPower = 0;

    public float _delta = 0;

    [Header("낮을수록 복구 느려짐")]
    public float _damping = 1.0f;

    
    public Movement _target = null;







    public bool _toggleUpdate = true;
    Coroutine _updater = null;

    //반복적으로 힘 넘겨주는 코루틴
    IEnumerator UpdatePower(float time)
    {
        
        if (_target != null)
            _target.AddSpeed(_currentPower * 10);
        
        yield return new WaitForSecondsRealtime(time);

        if (_toggleUpdate)
            _updater = StartCoroutine("UpdatePower", time);
    }

    void Start()
    {
        _lastPos = this.transform.position;
        _updater = StartCoroutine("UpdatePower", 0.3333f);
    }

    void ResetLastPos()
    {
        _lastPos = this.transform.position;
    }


    void Update()
    {

        //움직인 벡터
        _moveDelta = this.transform.position - _lastPos;

        //갑자기 이상한 곳으로 가는 것을 체크 할수 있을까?


        //파워 계산
        float deltaPower = Time.deltaTime * Vector3.Distance(Vector3.zero, _moveDelta) * _senser;
        _currentPower += deltaPower;

        _delta = Vector3.Distance(Vector3.zero, _moveDelta);
        //Debug.Log(_delta);

        //
        if (_delta > 1) return;

        //서서히 힘을 잃음
        //_currentPower = Mathf.Lerp(_currentPower, 0, 1 - (Time.deltaTime * 0.1f));
        _currentPower = _currentPower - Mathf.Lerp(0, _currentPower, Time.deltaTime * _damping);
        if (_currentPower < 0.005f) _currentPower = 0;

        //최고최소치 제어
        Mathf.Clamp(_currentPower, 0, 5);

    }




    private void LateUpdate()
    {
        ResetLastPos();
    }

}
