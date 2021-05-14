using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act_Grabber : OVRGrabber
{

    //public bool 

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    protected override void GrabBegin()
    {
        base.GrabBegin();

        Debug.Log("grap now");
    }
    
    //그랩 체크
    public bool GrabCheck { get { return !m_grabVolumeEnabled; }  }
}
