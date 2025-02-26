namespace TextRenderer3 {
    public abstract class CMacro
    {
        protected CScope parent_scope;
        protected CMacro(CScope parent_scope)
        {
            this.parent_scope = parent_scope;
        }
    }
    public class SerialPeaker : CMacro
    {
        int m_serial = 0;
        public SerialPeaker(CScope currentScope) : base(currentScope) { }
        public int NextSerial()
        {
            return m_serial++;
        }
        public int CurrentSerial()
        {
            return m_serial;
        }
    }
    public class CResult : CMacro
    {
        public CResult(CScope currentScope) : base(currentScope) { }
        public string Result(string[] parameters)
        {
            if (parameters.Length < 2) throw new ArgumentException("Result macro requires two parameters");
            string param1 = parent_scope.GetValue(parameters[0]);
            string param2 = parent_scope.GetValue(parameters[1]);

            if (int.TryParse(param1, out int num1) && int.TryParse(param2, out int num2))
            {
                return (num1 + num2).ToString();
            }
            throw new ArgumentException("Parameters must be integers");
        }
    }
}
