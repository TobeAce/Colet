// the codes come from the book:
//      "the Art of Java"
// may have suspension of infringement

public class Parser
    {
        // These are the token types.
        const int NONE = 0;
        const int DELIMITER = 1;
        const int VARIABLE = 2;
        const int NUMBER = 3;
        // These are the types of syntax errors.
        const int SYNTAX = 0;
        const int UNBALPARENS = 1;
        const int NOEXP = 2;
        const int DIVBYZERO = 3;
        // This token indicates end-of-expression.
        const string EOE = "\0";
        private string exp; // refers to expression string
        private int expIdx; // current index into the expression
        private string token; // holds current token
        private int tokType; // holds token's type
        // Parser entry point.
        public double evaluate(string expstr)
        {
            double result;
            exp = expstr;
            expIdx = 0;
            getToken();
            if (token == EOE)
                handleErr(NOEXP); // no expression present
            // Parse and evaluate the expression.
            result = evalExp2();
            if (token != EOE) // last token must be EOE
                handleErr(SYNTAX);
            return result;
        }
        // Add or subtract two terms.
        private double evalExp2()
        {
            string op;
            double result;
            double partialResult;
            result = evalExp3();
            while ((op = token.Substring(0)) == "+" || op == "-")
            {
                getToken();
                partialResult = evalExp3();
                switch (op)
                {
                    case "-":
                        result = result - partialResult;
                        break;
                    case "+":
                        result = result + partialResult;
                        break;
                }
            }
            return result;
        }
        // Multiply or divide two factors.
        private double evalExp3()
        {
            string op;
            double result;
            double partialResult;
            result = evalExp4();
            while ((op = token.Substring(0)) == "*" || op == "/" || op == "%")
            {
                getToken();
                partialResult = evalExp4();
                switch (op)
                {
                    case "*":
                        result = result * partialResult;
                        break;
                    case "/":
                        if (partialResult == 0.0)
                            handleErr(DIVBYZERO);
                        result = result / partialResult;
                        break;
                    case "%":
                        if (partialResult == 0.0)
                            handleErr(DIVBYZERO);
                        result = result % partialResult;
                        break;
                }
            }
            return result;
        }
        // Process an exponent.
        private double evalExp4()
        {
            double result;
            double partialResult;
            double ex;
            int t;
            result = evalExp5();
            if (token == "^")
            {
                getToken();
                partialResult = evalExp4();
                ex = result;
                if (partialResult == 0.0)
                {
                    result = 1.0;
                }
                else
                    for (t = (int)partialResult - 1; t > 0; t--)
                        result = result * ex;
            }
            return result;
        }
        // Evaluate a unary + or -.
        private double evalExp5()
        {
            double result;
            string op;
            op = "";
            if ((tokType == DELIMITER) &&
            token == "+" || token == "-")
            {
                op = token;
                getToken();
            }
            result = evalExp6();
            if (op == "-") result = -result;
            return result;
        }
        // Process a parenthesized expression.
        private double evalExp6()
        {
            double result;
            if (token == "(")
            {
                getToken();
                result = evalExp2();
                if (token != ")")
                    handleErr(UNBALPARENS);
                getToken();
            }
            else result = atom();
            return result;
        }
        // Get the value of a number.
        private double atom()
        {
            double result = 0.0;
            if (tokType == NUMBER)
            {
                result = double.Parse(token);
                getToken();
            }
            return result;
        }
        // Obtain the next token.
        private void getToken()
        {
            tokType = NONE;
            token = "";
            // Check for end of expression.
            if (expIdx == exp.Length)
            {
                token = EOE;
                return;
            }
            if (isDelim(exp.Substring(expIdx,1)))
            { // is operator
                token += exp.Substring(expIdx,1);
                expIdx++;
                tokType = DELIMITER;
            }
            else if (isLetter(exp.Substring(expIdx,1)))
            { // is variable
                while (!isDelim(exp.Substring(expIdx,1)))
                {
                    token += exp.Substring(expIdx,1);
                    expIdx++;
                    if (expIdx >= exp.Length) break;
                }
                tokType = VARIABLE;
            }
            else if (isDigit(exp.Substring(expIdx,1)))
            { // is number
                while (!isDelim(exp.Substring(expIdx,1)))
                {
                    token += exp.Substring(expIdx,1);
                    expIdx++;
                    if (expIdx >= exp.Length) break;
                }
                tokType = NUMBER;
            }
            else
            { // unknown character terminates expression
                token = EOE;
                return;
            }
        }
        // Handle an error
        private void handleErr(int error)
        {
            string[] err = {
                "Syntax Error",
                "Unbalanced Parentheses",
                "No Expression Present",
                "Division by Zero"};
            Console.WriteLine(err[error]);
        }
        // Return true if c is a delimiter.
        private bool isDelim(string s)
        {
            if ((" +-/*%^=()".IndexOf(s) != -1)) return true;
            return false;
        }
        private bool isLetter(string s)
        {
            char[] temp = s.ToCharArray();
            if ((temp[0] >= 'a' && temp[0] <= 'z') || (temp[0] >= 'A' && temp[0] <= 'Z')) return true;
            return false;
        }
        private bool isDigit(string s)
        {
            char[] temp = s.ToCharArray();
            if (temp[0] >= '0' && temp[0] <= '9') return true;
            return false;
        }
    }
