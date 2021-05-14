using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[ExecuteInEditMode]
public class Spline : MonoBehaviour
{

    //노드 리스트
    public readonly List<LineNode> _nodes = new List<LineNode>();
    //시작 노드
    public LineNode _startNode = null;
    public GameObject _nodeObj = null;

    void Start()
    {
        _nodeObj = Resources.Load<GameObject>("Spline/DefaultNode");
    }

    void Update()
    {
        
    }


    //노드 생성
    public void AddNode()
    {
        LineNode temp = Instantiate(_nodeObj).GetComponent<LineNode>();
        temp.transform.SetParent(this.transform);

        //노드가 있나?
        if (_startNode == null)
        {
            //없으면 그냥 스플라인 위치에 생성
            temp.transform.position = this.transform.position;
            _startNode = temp;
        }
        else
        {
            //이전, 이후 노드 가르킴
            _nodes[_nodes.Count - 1]._nextNode = temp;
            temp._beforeNode = _nodes[_nodes.Count - 1];

            //있다면 마지막 노드기준으로 위치
            temp.transform.position = 
                _nodes[_nodes.Count - 1].transform.position + Vector3.forward;

        }

        temp.Spawn();
        _nodes.Add(temp);
    }

    public void DeleteNode(LineNode node)
    {
        //삭제하려는게 시작노드인가?
        if (_startNode == node)
        {
            //시작 노드 다음이 없나?
            if (node._nextNode == null)
                _startNode = null;
            else
            {
                //노드 연결
                _startNode = node._nextNode;
                _startNode._beforeNode = node._beforeNode;
            }
        }
        else
        {
            //노드 이어주기
            node._beforeNode._nextNode = node._nextNode;
            node._nextNode._beforeNode = node._beforeNode;
        }

        //삭제
        DestroyImmediate(node);
    }
}
