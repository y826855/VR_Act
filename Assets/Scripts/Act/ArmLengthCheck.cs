using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmLengthCheck : MonoBehaviour
{

    #region fromEditor
    public Transform _HMD = null; //플레이어 머리
    public Act_Grabber _handL = null; //플레이어 왼손
    public Act_Grabber _handR = null; //플레이어 오른손

    public Transform _NPC_handL = null; //npc 왼손
    public Transform _NPC_handR = null; //npc 오른손

    #endregion

    public float _lenL = 0.8f; //팔길이 왼
    public float _lenR = 0.8f; //팔길이 오
    public float _armDefault = 0.2f; //기본 팔길이
    public bool _checkLengthNow = false; //길이 체크 상태

    public float _Height = 0.0f; //키

    private void Start()
    {
        
    }


    private void Update()
    {
        //팔 길이 측정
        if (_handL.GrabCheck == true && _handR.GrabCheck == true)
            //&& _checkLengthNow == true)
        {

            //2D좌표로 전환
            Vector2 hmd_pos = new Vector2(_HMD.position.x, _HMD.position.z);
            Vector2 handL = new Vector2(_handL.transform.position.x, _handL.transform.position.z);
            Vector2 handR = new Vector2(_handR.transform.position.x, _handR.transform.position.z);
            

            //길이 계산
            _lenL = Vector2.Distance(hmd_pos, handL) + _armDefault;
            _lenR = Vector2.Distance(hmd_pos, handR) + _armDefault;
            
                
            Debug.Log("hand dist : " + _lenL);

            //NPC 팔에 적용
            _NPC_handL.localScale = new Vector3(_lenL, 1, 1);
            _NPC_handR.localScale = new Vector3(_lenR, 1, 1);

            _Height = _HMD.position.y;

            _checkLengthNow = false;
        }
    }

    /*
     y 위치 빼고 머리에서 부터 길이 받을까?
    */
}
