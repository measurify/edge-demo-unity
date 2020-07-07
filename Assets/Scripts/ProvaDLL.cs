using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.Text;

public class ProvaDLL : MonoBehaviour

{
    [DllImport("NATIVECPPLIBRARY", EntryPoint = "tryDate")]
    public static extern int tryDate();
    [DllImport("NATIVECPPLIBRARY", EntryPoint = "tryPOCO")]
    public static extern void tryPOCO(StringBuilder myString, int length);
    [DllImport("NATIVECPPLIBRARY", EntryPoint = "tryRead")]
    public static extern int tryRead(StringBuilder myString, int length);

    // Start is called before the first frame update
    void Start()
    {
        print(tryDate());
        //System.IntPtr p = IntPtr.Zero;
        //p = tryPOCO();
        StringBuilder str = new StringBuilder(100);
        //get my string from native code
        tryPOCO(str, str.Capacity);
        string myString = str.ToString();
        print(myString);
        //print(Marshal.PtrToStringAnsi(tryPOCO()));
        //char[] s = new char[4];
        //s[0] = 'C';
        //s[1] = 'i';
        //s[2] = 'a';
        //s[3] = 'o';
        //StringBuilder sb = new StringBuilder();
        //sb.Append("Ciao");

        print(tryRead(str, str.Capacity));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
