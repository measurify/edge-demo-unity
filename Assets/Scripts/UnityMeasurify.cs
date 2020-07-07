﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

public class UnityMeasurify : MonoBehaviour
{
    public string id, pw, sampleName;
    StringBuilder idSB, pwSB, sampleSB;
    int oldCollisionCount;
    Queue<Thread> threads;

    [DllImport("NATIVECPPLIBRARY", EntryPoint = "Setup")]
    public static extern void Setup(StringBuilder myId, StringBuilder myPw);

    [DllImport("NATIVECPPLIBRARY", EntryPoint = "Action")]
    public static extern void Action(StringBuilder mySample, float data);



    // Start is called before the first frame update
    void Start()
    {
        idSB = new StringBuilder(id);
        pwSB = new StringBuilder(pw);
        sampleSB = new StringBuilder(sampleName);
        Setup(idSB, pwSB);
        threads = new Queue<Thread>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (MoveBall.collisionCount % 5 == 0 && MoveBall.collisionCount != 0 && oldCollisionCount != MoveBall.collisionCount)
        {
            Thread t = new Thread(Play);
            threads.Enqueue(t);
            if(!threads.Peek().IsAlive)
                threads.Peek().Start();
        }
        oldCollisionCount = MoveBall.collisionCount;
    }

    void Play()
    {
        Action(sampleSB, MoveBall.collisionCount);
        threads.Dequeue().Abort();
    }

    void OnApplicationQuit()
    {
        foreach (Thread t in threads) {
            if (!t.IsAlive)
                t.Start();
        }
        print("Application ended");
    }
}