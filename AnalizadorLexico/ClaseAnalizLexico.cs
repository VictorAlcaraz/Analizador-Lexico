using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace AnalizadorLexico
{
    class AnalizLexico
    {
        int token, EdoActual, EdoTransicion;
        string CadenaSigma;
        public string Lexema;
        bool PasoPorEdoAcept;
        int IniLexema, FinLexema, IndiceCaracterActual;
        char CaracterActual;
        Stack<int> Pila = new Stack<int>();
        AFD AutomataFD; 

        public AnalizLexico()
        {
            CadenaSigma = "";
            PasoPorEdoAcept = false;
            IniLexema = FinLexema = -1;
            IndiceCaracterActual = -1;
            token = -1;
            Pila.Clear();
            AutomataFD = null;
        }
        public AnalizLexico(string sigma, string FileAFD, int IdAfD)
        {
            AutomataFD = new AFD();
            CadenaSigma = sigma;
            PasoPorEdoAcept = false; 
            IniLexema = 0;
            FinLexema = -1;
            IndiceCaracterActual = 0;
            token = -1;
            Pila.Clear();
            AutomataFD.LeerAFDdeArchivo(FileAFD, IdAfD);
        }
        public AnalizLexico(string sigma, string FileAFD)
        {
            AutomataFD = new AFD();
            CadenaSigma = sigma;
            PasoPorEdoAcept = false; 
            IniLexema = 0;
            FinLexema = -1;
            IndiceCaracterActual = 0;
            token = -1;
            Pila.Clear();
            AutomataFD.LeerAFDdeArchivo(FileAFD, -1);
        }
        public AnalizLexico(string sigma, AFD AutFD)
        {
            CadenaSigma = sigma;
            PasoPorEdoAcept = false; 
            IniLexema = 0;
            FinLexema = -1;
            IndiceCaracterActual = 0;
            token = -1;
            Pila.Clear();
            AutomataFD = AutFD;
        }
        public ClassEstadoAnalizLexico GetEdoAnalizLexico()
        {
            ClassEstadoAnalizLexico EdoActualAnaliz = new ClassEstadoAnalizLexico();
            EdoActualAnaliz.CaracterActual = CaracterActual;
            EdoActualAnaliz.EdoActual = EdoActual;
            EdoActualAnaliz.EdoTransicion = EdoTransicion;
            EdoActualAnaliz.FinLexema = FinLexema;
            EdoActualAnaliz.IndiceCaracterActual = IndiceCaracterActual;
            EdoActualAnaliz.IniLexema = IniLexema;
            EdoActualAnaliz.Lexema = Lexema;
            EdoActualAnaliz.PasoPorEdoAcept = PasoPorEdoAcept;
            EdoActualAnaliz.token = token;
            EdoActualAnaliz.Pila = Pila;
            return EdoActualAnaliz;
        }
        public bool SetEdoAnalizLexico(ClassEstadoAnalizLexico e)
        {
            CaracterActual = e.CaracterActual;
            EdoActual = e.EdoActual;
            EdoTransicion = e.EdoTransicion;
            FinLexema = e.FinLexema;
            IndiceCaracterActual = e.IndiceCaracterActual;
            IniLexema = e.IniLexema;
            Lexema = e.Lexema;
            PasoPorEdoAcept = e.PasoPorEdoAcept;
            token = e.token;
            Pila = e.Pila;
            return true;
        }
        public void SetSigma(string sigma)
        {
            CadenaSigma = sigma;
            PasoPorEdoAcept = false;
            IniLexema = 0;
            FinLexema = -1;
            IndiceCaracterActual = 0;
            token = -1;
            Pila.Clear();
        }
        public string CadenaXAnalizar()
        {
            return CadenaSigma.Substring(IndiceCaracterActual, CadenaSigma.Length - IndiceCaracterActual);
        }
        public int yylex()
        {
            while (true)
            {
                Pila.Push(IndiceCaracterActual);
                if (IndiceCaracterActual >= CadenaSigma.Length)
                {
                    Lexema = "";
                    return SimbolosEspeciales.FIN;
                }
                IniLexema = IndiceCaracterActual;
                EdoActual = 0;
                PasoPorEdoAcept = false;
                FinLexema = -1;
                token = -1;
                while (IndiceCaracterActual < CadenaSigma.Length)
                {
                    CaracterActual = CadenaSigma[IndiceCaracterActual];
                    EdoTransicion = AutomataFD.TablaAFD[EdoActual, CaracterActual];
                    if (EdoTransicion != -1)
                    {
                        if (AutomataFD.TablaAFD[EdoTransicion, 256] != -1)
                        {
                            PasoPorEdoAcept = true;
                            token = AutomataFD.TablaAFD[EdoTransicion, 256];
                            FinLexema = IndiceCaracterActual;
                        }
                        IndiceCaracterActual++;
                        EdoActual = EdoTransicion;
                        continue;
                    }
                    break;
                }
                if (!PasoPorEdoAcept)
                {
                    IndiceCaracterActual = IniLexema + 1;
                    Lexema = CadenaSigma.Substring(IniLexema, 1);
                    token = SimbolosEspeciales.ERROR;
                    return token;
                }
                Lexema = CadenaSigma.Substring(IniLexema, FinLexema - IniLexema +1);
                IndiceCaracterActual = FinLexema + 1;
                if (token == SimbolosEspeciales.OMITIR)
                    continue;
                else 
                    return token;
            }
        }
        public bool UndoToken()
        {
            if (Pila.Count == 0)
                return false;
            IndiceCaracterActual = Pila.Pop();
            return true;
        }
    }
}