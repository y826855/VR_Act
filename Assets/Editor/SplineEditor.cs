using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Spline))]
[ExecuteInEditMode]
public class SplineEditor : Editor
{
    //에디터에서 선택한 객체
    private Spline _selection = null;


    //활성화시
    private void OnEnable()
    {
        _selection = target as Spline;
    }


    //인스팩터 창에 나타날 것들
    public override void OnInspectorGUI()
    {

        #region //
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_nodes"));


        #endregion



        //노드 생성
        if (GUILayout.Button("Add Point"))
        {
            //Debug.Log(_selection.name);
            _selection.AddNode();           
        }



        //모든 노드 그리기
        if (_selection != null)
        {
            if (_selection._startNode != null)
            {
                foreach (var it in _selection._nodes)
                {
                    if (Handles.Button(it.transform.position, Quaternion.identity, 0.02f, 0.02f, Handles.DotHandleCap))
                    {

                    }

                }
            }


            

        }





        //if (GUILayout.Button("Add Point"))
        //{
        //
        //}




    }
}


/*
    스플라인이 노드들 관리하게 하자

    노드는 스플라인 오브젝트 아래에만 존제

    스플라인은 그 노드들 생성, 삭제 등의 역할

    현재 거리를 스플라인 포인트 위치로 받아올 수 있음

    루프 클릭시 자동으로 루프됨.

    갈림길은..일단 없음..
*/