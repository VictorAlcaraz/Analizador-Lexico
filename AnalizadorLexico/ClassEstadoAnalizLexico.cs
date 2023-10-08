public class ClassEstadoAnalizLexico
    {
        public int token, EdoActual, EdoTransicion;
        public string CadenaSigma;
        public string Lexema;
        public bool PasoPorEdoAcept;
        public int IniLexema, FinLexema, IndiceCaracterActual;
        public char CaracterActual;
        public Stack<int> Pila = new Stack<int>(); 
    }