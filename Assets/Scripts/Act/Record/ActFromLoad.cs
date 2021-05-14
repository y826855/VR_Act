using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActData
{
    public string _clipName = "";
    public int _count = 0;
    public float _actTime = 0.0f;
    public float _Accuracy = 0.0f;
}


public class ActFromLoad : MonoBehaviour
{

    #region from editor 
    //생성 위치
    public Transform _handL = null;
    public Transform _handR = null;
    public Transform _head = null;

    //팔길이 측정
    public ArmLengthCheck _Arm = null;

    //동작 인식기
    public CreateChecker _checker = null;

    //파티클 트레일
    public TrailRenderer _pTrailL = null;
    public TrailRenderer _pTrailR = null;

    //플레이어 손 위치
    public PlayerTrail _playerHandL = null;
    public PlayerTrail _playerHandR = null;
    #endregion

    //현재 정답 갯수
    int _currCorrect = 0;
    //동작 정답률
    public float _accuracy = 0.0f;

    //애니메이터
    Animator _anim = null;

    //체커 초기화 가능여부
    public bool _canRefreshChecker = false;
    
    //동작시간 끝났는지 확인
    [Header("동작 끝났는지 확인. 시작할때 한번 켜줘야함")]
    public bool _isEndAction = false;

    float _actTime = 0.0f;

    //현재 동작 인덱스
    public int indexAct = 0;

    bool _isRoutinEnd = false;

    //플레이어 정보 담기
    List<PlayerActData> _playerData = new List<PlayerActData>();

    private void Start()
    {
        _anim = this.GetComponent<Animator>();
        //_anim.SetTrigger("Batter On Deck");

        _pTrailL.emitting = false;
        _pTrailR.emitting = false;

        
        _playerHandL._actFromLoad = this;
        _playerHandR._actFromLoad = this;
    }

    //지정된 시간동안 소환
    IEnumerator SpawnCols(float time = 0)
    {
        float waitTime = 1.0f / (float)_currCorrect;

        int min = (int)(time * _currCorrect);
        time += 1.0f / (float)_currCorrect;
        int max = (int)(time * _currCorrect);

        ActionData currAct = LoadCSV._AllAction[indexAct];
        for (int i = min; i < max; i++)
        {
            Vector3 L = new Vector3(currAct._checkerList[i*2].x * _Arm._lenL,
                currAct._checkerList[i*2].y,
                currAct._checkerList[i*2].z * _Arm._lenL);
            Vector3 R = new Vector3(currAct._checkerList[i*2 + 1].x * _Arm._lenR,
                currAct._checkerList[i*2 + 1].y,
                currAct._checkerList[i*2 + 1].z * _Arm._lenR);

            //npc 위치 더해줌
            L += this.transform.position;
            R += this.transform.position;

            _checker.SpawnChecker(L, false);     //left
            _checker.SpawnChecker(R, true);      //right

            testCount+= 2;
        }

        yield return new WaitForSeconds(waitTime);

        if (time < 1) { StartCoroutine(SpawnCols(time)); }
        else { Debug.Log(testCount + "test"); }
    }

    public int testCount = 0;
    //체커 생성
    public void SpawnCol()
    {
        ActionData currAct = LoadCSV._AllAction[indexAct];

        //정답 갯수 저장
        _currCorrect = currAct._checkerList.Count >> 1;

        //현재 정답 초기화
        _playerHandL.ResetCount();
        _playerHandR.ResetCount();

        testCount = 0;
        StartCoroutine(SpawnCols());

        //TODO :지정된 시간동안 만들어지게 해보자

        /*
        for (int i = 0; i < currAct._checkerList.Count; i+= 2)
        {
            //체커 생성 좌표 지정
            Vector3 L = new Vector3(currAct._checkerList[i].x * _Arm._lenL, 
                currAct._checkerList[i].y, 
                currAct._checkerList[i].z * _Arm._lenL);
            Vector3 R = new Vector3(currAct._checkerList[i + 1].x * _Arm._lenR, 
                currAct._checkerList[i + 1].y, 
                currAct._checkerList[i + 1].z * _Arm._lenR);
        
            //npc 위치 더해줌
            L += this.transform.position;
            R += this.transform.position;
        
            _checker.SpawnChecker(L, false);     //left
            _checker.SpawnChecker(R, true);      //right
        }
        Debug.Log(currAct._checkerList.Count + "counts");
        */
    }

    //에님 시작 체크
    public void StartCheck()
    {
        if (_isRoutinEnd) return;

        //트레일 활성화
        _pTrailL.emitting = true;
        _pTrailR.emitting = true;

        //이전 동작 끝났는지 체크
        if (_isEndAction == true)
        {
            _isEndAction = false;
            _checker.RemoveAll();
            SpawnCol();

            _canRefreshChecker = false;
        }

        //초기화 해도 되나?
        else if(_canRefreshChecker)
        {
            _canRefreshChecker = false;
            _checker.RemoveAll();
            SpawnCol();
        }
    }

    //애님 정지 체크
    public void StopCheck()
    {
        _pTrailL.emitting = false;
        _pTrailR.emitting = false;


        ActionData currAct = LoadCSV._AllAction[indexAct];

        //플레이어 데이터 저장
        {
            PlayerActData data = new PlayerActData();

            data._clipName = currAct._clipName;
            data._Accuracy = _accuracy * 50.0f;

            _playerData.Add(data);

            _playerHandL.ResetCount();
            _playerHandR.ResetCount();
        }

        //코루틴 끝나면 다음 애님 넘어갈 수 있음
        if (_checkActionTime != null) return;
        _isEndAction = true;



        //현재 애님 비활성화
        _anim.SetBool(currAct._clipName, false);

        //모션이 더 있나 체크하자
        //indexAct = (indexAct < 2) ? indexAct + 1 : indexAct;
        if (indexAct < LoadCSV._AllAction.Count - 1) { indexAct++; }
        
        //모든 모션 끝나면 데이터 저장
        else
        {
            Debug.Log("Save Data");
            CSV_WritePlayerData.WriteCSV(_playerData);

            _isRoutinEnd = true;
            _anim.SetBool(currAct._clipName, false);
            _checker.RemoveAll();
            return;
        }


        //다음 애님으로 변경
        currAct = LoadCSV._AllAction[indexAct];
        _anim.SetBool(currAct._clipName, true);
    }


    //시간 받아 다음 애니메이션 넘어갈 수 있게 해줌
    Coroutine _checkActionTime = null;
    IEnumerator CheckActionTime(float time)
    {
        ActionData currAct = LoadCSV._AllAction[indexAct];

        //애님 실행
        _anim.SetBool(currAct._clipName, true);

        yield return new WaitForSecondsRealtime(time);

        _checkActionTime = null;
    }


    //동작 정확성 체크
    public void CheckCorrect()
    {
        //왼손 정답
        float leftC = (float)_playerHandL.CountCorrect / (float)_currCorrect;
        
        //오른손 정답
        float rightC = (float)_playerHandL.CountCorrect / (float)_currCorrect;

        _accuracy = leftC + rightC;
        //60% 성공하면 했다 취급
        if (leftC + rightC > 1.2f)
        {
            _canRefreshChecker = true;
        }
    }
    
    public void Update()
    {
        //코루틴 끝났는지 체크
        if (_checkActionTime == null && _isEndAction == true
            && _isRoutinEnd == false)
            _checkActionTime = StartCoroutine(
                CheckActionTime(LoadCSV._AllAction[indexAct]._actTime));


    }

}


/*
 
    TODO : 정답 갯수 컨트롤러에 알려주기
    정답률 60% 넘어가면 다음 애님 실행때 콜라이더 생성하자
     
*/
