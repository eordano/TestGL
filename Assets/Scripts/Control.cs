using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

public class Control : MonoBehaviour
{

    public static Control singleton;

    public GameObject cube;
    public GameObject cyllinder;
    public GameObject sphere;

    public float cummTicks = 0;
    public float totalTicks = 0;
    public int lastReceivedMessages = 0;
    public int receivedMessages = 0;

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
        _UnityJS_HandleAwake(Activate);
    }

    // Update is called once per frame
    void Update()
    {
        var dt = Time.deltaTime;
        cummTicks += dt;
        if (cummTicks > 2)
        {
            print(string.Format("{0}: {1}", totalTicks, (lastReceivedMessages - receivedMessages)));

            totalTicks += cummTicks;
            cummTicks = 0;
            receivedMessages = lastReceivedMessages;
        }
    }

    public void Ctrl(string msg)
    {
        lastReceivedMessages++;
        int command = int.Parse(msg);

        switch (command)
        {
            case 1:
                cube.SetActive(!cube.activeSelf);
                break;
            case 2:
                cyllinder.SetActive(!cyllinder.activeSelf);
                break;
            case 3:
                sphere.SetActive(!sphere.activeSelf);
                break;
        }
    }

    public void CtrlFaster(int which, int what)
    {
        lastReceivedMessages++;

        switch (which)
        {
            case 1:
                cube.SetActive(what == 0);
                break;
            case 2:
                cyllinder.SetActive(what == 0);
                break;
            case 3:
                sphere.SetActive(what == 0);
                break;
        }
    }

    public delegate int ActivateDelegate(int which, int what);

    [DllImport("__Internal")]
    public static extern void _UnityJS_HandleAwake(ActivateDelegate activateCallback);

    [MonoPInvokeCallback(typeof(ActivateDelegate))]
    public static int Activate(int which, int what)
    {
        singleton.CtrlFaster(which, what);
        return 0;
    }
}
