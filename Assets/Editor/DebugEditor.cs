using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


//[CustomEditor(typeof(NewBehaviourScript))]
[CustomEditor(typeof(CreateChecker))]
[ExecuteInEditMode]
public class DebugEditor : Editor
{

    //에디터에서 선택한 객체
    //private NewBehaviourScript _selection = null;
    private CreateChecker _selection = null;
    

    //활성화시
    private void OnEnable()
    {
        //_selection = target as NewBehaviourScript;
        _selection = target as CreateChecker;
    }

    //인스팩터 창에 나타날 것들
    public override void OnInspectorGUI()
    {

        //디버그 버튼
        if (GUILayout.Button("Debug"))
        {
            //Debug.Log(_selection.name);
            EditorUtility.SetDirty(_selection);
            _selection.CheckerCreate();
        }
    }


    private void OnSceneGUI()
    {

    }
}
