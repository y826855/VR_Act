using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //현재 속도
    public float _currSpeed = 0.0f;

    //목표 속도
    public float _moveSpeed = 0.0f;

    //뎀퍼
    public float _damping = 1.0f;

    
    public float _senser = 5.0f;





    //출발도 서서히?
    public void AddSpeed(float speed)
    {
        _moveSpeed += speed; 
    }

    private void Update()
    {

        //속도 점점 감소
        _moveSpeed = Mathf.Lerp(0, _moveSpeed, 1 - (Time.deltaTime * _damping));
        if (_moveSpeed < 0.001) _moveSpeed = 0;

        //목표 속도로 보간
        _currSpeed = Mathf.Lerp(_currSpeed, _moveSpeed, Time.deltaTime * _senser);

        //이동
        this.transform.position += this.transform.forward * _currSpeed * Time.deltaTime;
    }




}
