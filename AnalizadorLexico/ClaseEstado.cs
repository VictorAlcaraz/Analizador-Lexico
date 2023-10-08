using System;
using System.Collections.Generic;
public class Estado
{
    static int ContadorEstado = 0; 
    private int idEstado1;
    private bool EdoAcept1;
    private int Token1;
    private HashSet<Transicion> Trans1 = new HashSet<Transicion>();

    public Estado ()
    {
        EdoAcept = false;
        Token1 = -1;

        IdEstado = ContadorEstado++;
        Trans1.Clear();
    }

    public int IdEstado {get => idEstado1; set => idEstado1 = value;}
    public bool EdoAcept {get => EdoAcept; set => EdoAcept = value;}
    public int Token {get => Token1; set => Token1 = value;}
    public HashSet<Transicion> Trans {get => Trans1; set => Trans1 = value;}
}