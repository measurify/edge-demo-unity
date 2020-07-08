Edge Engine With Unity
=================
### Table of contents
- [Overview](https://github.com/measurify/edge-demo-unity#overview)
- [Example](https://github.com/measurify/edge-demo-unity#example)
## Overview
The idea is to test [Edge Engine](https://github.com/measurify/edge) so that it interacts with data from a virtual environment rather than a real sensor.
In order to do that, the first step was to build the DLL(Dinamic Link Library) which relies on the Edge Engine library, implementing wrapper functions for the nedeed purposes and expose them so that Unity could interpret them correctly.
In this repository we are going to focus only on the Unity side, [here](https://github.com/measurify/edge-dll) you can find the aspect related to the implementation of the DLL.
To test the engine we developed a simple scene where the user is in control of a ball locked in a box and collisions with the walls are recorded and sent to Measurify.
## Example 
In order to use the Edge Engine library with Unity the first thing to do is to declare the wrapper functions: 
```
    [DllImport("NATIVECPPLIBRARY", EntryPoint = "Setup")]
    public static extern void Setup(StringBuilder myId, StringBuilder myPw);

    [DllImport("NATIVECPPLIBRARY", EntryPoint = "Action")]
    public static extern void Action(StringBuilder mySample, float data);
``` 
With this kind of signature of the _Setup_ function, we gave to the user the possibility to configure his own credentials to get access to a specific device inside Measurify.
In the _Update_ function we first check the number of collisions occurred and, if it is a multiple of five (zero is excluded), we create a thread that runs the code to communicate with the server. The thread implementation is necessary because otherwise, when the _Action_ method is called (which sends data to Measurify), the scene freezes for a while.
```
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
```
Thread objects are stored in a **queue** in order to manage multiple communication at once while not stopping the execution of the scene.