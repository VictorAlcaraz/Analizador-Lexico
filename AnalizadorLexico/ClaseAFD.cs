using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class AFD
{   
    public int[,] TablaAFD;

    // Constructor
    public AFD()
    {
        TablaAFD = new int[0, 0];
    }
    public AFD(HashSet<Estado> Estados)
    {
        int[,] TablaAFD = new int[Estados.Count, 257];

    }
    // Metodos 
    public void LeerAFDdeArchivo (string File, int Id)
    {

    }
}