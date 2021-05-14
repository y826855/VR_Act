using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTrail : MonoBehaviour
{

    public bool DebugTrigger = false;

    public Transform _char = null;
    public Transform _hmd = null;
    public Transform _target = null;
    public ArmLengthCheck _player = null;

    public Image _img = null;
    public int _countCorrect = 0;
    public int CountCorrect {get{ return _countCorrect; }}
    public float _interval = 0.4f;

    public ActFromLoad _actFromLoad = null;

    bool _nowCorrect = false;
    Coroutine _changeColor = null;
    IEnumerator ChangeColor()
    {
        SetColor(_nowCorrect);

        yield return new WaitForSecondsRealtime(0.3f);

        _nowCorrect = false;
        _changeColor = null;
        SetColor(_nowCorrect);
    }


    //색상 변경
    void SetColor(bool correct)
    {
        //_img.color = correct ? new Color(193, 255, 150) : new Color(255,150,176);
        _img.color = correct ? new Color(0.7568f, 1, 0.5882f) : new Color(1, 0.5882f, 0.6901f);
    }

    void Update()
    {
        if (DebugTrigger) return;

        //손의 위치 npc 쪽으로 돌리기
        Vector3 pos = new Vector3(-_target.position.x, -_target.position.y, _target.position.z);
        //this.transform.position = _hmd.position - pos + new Vector3(0,_char.position.y - 1.3f,0);
        this.transform.position = _hmd.position - pos + new Vector3(0,_char.position.y - _player._Height - _interval, 0);
    }

    //카운트 제거
    public void ResetCount()
    {
        _countCorrect = 0;
    }

    //체커와 충돌
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
        //Debug.Log("triggered");
        _countCorrect++;

        if (_changeColor != null)
            StopCoroutine(_changeColor);
        
        _changeColor = StartCoroutine("ChangeColor");
        _nowCorrect = true;

        _actFromLoad.CheckCorrect();
    }

}
