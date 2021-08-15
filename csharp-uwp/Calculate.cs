using System;

namespace Calculatre
{
    class Calculate
    {
        private char input;         // the current char
        private int i;              // the char tracer
        private int length;         // the length of whole formula

        public string formula;      // the formula as a string
        public double result;       // the return of plus(), the final result of calculation
        public int error;           // 1. div is 0; 2. char is unknown;

        public Calculate(string expression)
        {
            this.i = 0;
            this.error = 0;
            this.formula = expression;
            this.length = this.formula.Length;
        }

        public void Start()
        {
            // get the first char and luanch the calculator
            Next(0);
            result = Plus();
        }

        private void Next(int n)
        {
            i += n;
            if (i <= length - 1)
            {
                input = formula[i];
            }
        }

        private double Plus()
        {
            double value = 0.0;
            // indirect recursion
            value = Multi();
            while ((input == '+' || input == '-') && i != 0 && formula[i - 1] != '(')
            {
                if (input == '+')
                {
                    Next(1);
                    value += Multi();
                }
                else if (input == '-')
                {
                    Next(1);
                    value -= Multi();
                }
            }
            return value;
        }

        private double Multi()
        {
            double div;
            double value = 0.0;
            value = Power();
            while ((input == '*') || (input == '/'))
            {
                if (input == '*')
                {
                    Next(1);
                    value *= Power();
                }
                else if (input == '/')
                {
                    Next(1);
                    div = Power();
                    if (div == 0)
                    {
                        error = 1;
                        return 0.0;
                    }
                    value /= div;
                }
            }
            return value;
        }

        private double Power()
        {
            double value = 0.0;
            value = Signal();
            while (input == '^')
            {
                if (input == '^')
                {
                    Next(1);
                    value = System.Math.Pow(value, Signal());
                }
            }
            return value;
        }

        private double Signal()
        {
            double value = 0.0;
            if (input == '-' && (i == 0 || formula[i - 1] == '('))
            {
                Next(1);
                value = -High();
            }
            else if (input == '+' && (i == 0 || formula[i - 1] == '('))
            {
                Next(1);
                value = High();
            }
            else if (input != '-' && input != '+' && input != '\0')
            {
                value = High();
            }
            return value;
        }

        private double High()
        {
            double value = 0.0;
            if (input == '(')
            {
                Next(1);
                value = Plus();
                if (input == ')')
                {
                    Next(1);
                }
                else
                {
                    error = 2;
                    return 0.0;
                }
            }
            else if (input >= '0' && input <= '9')
            {
                string num = "";
                for (; formula[i] >= '0' && formula[i] <= '9' || formula[i] == '.';)
                {
                    num += formula[i];
                    i++;
                    if (i > length - 1)
                        break;
                }
                try
                {
                    value = Convert.ToDouble(num);
                }
                catch
                {
                    value = 0.0;
                    error = 2;
                }
                Next(0);
            }
            else if (input == 'e')
            {

                Next(1);
                if (input != 'x')
                    value = Math.E;
                else
                {
                    Next(2);
                    if (input == '(')
                    {
                        Next(1);
                        value = System.Math.Exp(Plus());
                        if (input == ')')
                        {
                            Next(1);
                        }
                        else
                        {
                            error = 2;
                            return 0.0;
                        }
                    }
                }
            }
            else if (input == 'p')
            {
                Next(1);
                if (input == 'i')
                {
                    value = Math.PI;
                    Next(1);
                }
            }
            else if (input == 'l')
            {
                Next(1);
                if (input == 'g')
                {
                    Next(1);
                    if (input == '(')
                    {
                        Next(1);
                        value = System.Math.Log10(Plus());
                        if (input == ')')
                        {
                            Next(1);
                        }
                        else
                        {
                            error = 2;
                            return 0.0;
                        }
                    }
                }
                else if (input == 'n')
                {
                    Next(1);
                    if (input == '(')
                    {
                        Next(1);
                        value = System.Math.Log(Plus());
                        if (input == ')')
                        {
                            Next(1);
                        }
                        else
                        {
                            error = 2;
                            return 0.0;
                        }
                    }
                }
            }
            else if (input == 's')
            {
                Next(3);
                if (input == '(')
                {
                    Next(1);
                    value = checkPrecision(System.Math.Sin((Plus())));
                    if (input == ')')
                    {
                        Next(1);
                    }
                    else
                    {
                        error = 2;
                        return 0.0;
                    }
                }
                if (input == 'h')
                {
                    Next(1);
                    if (input == '(')
                    {
                        Next(1);
                        value = checkPrecision(System.Math.Sinh((Plus())));
                        if (input == ')')
                        {
                            Next(1);
                        }
                        else
                        {
                            error = 2;
                            return 0.0;
                        }
                    }
                }


            }
            else if (input == 'c')
            {
                Next(3);
                if (input == '(')
                {
                    Next(1);
                    value = checkPrecision(System.Math.Cos((Plus())));
                    if (input == ')')
                    {
                        Next(1);
                    }
                    else
                    {
                        error = 2;
                        return 0.0;
                    }
                }
                if (input == 'h')
                {
                    Next(1);
                    if (input == '(')
                    {
                        Next(1);
                        value = checkPrecision(System.Math.Cosh((Plus())));
                        if (input == ')')
                        {
                            Next(1);
                        }
                        else
                        {
                            error = 2;
                            return 0.0;
                        }
                    }
                }
            }
            else if (input == 't')
            {
                Next(3);
                if (input == '(')
                {
                    Next(1);
                    value = checkPrecision(System.Math.Tan((Plus())));
                    if (input == ')')
                    {
                        Next(1);
                    }
                    else
                    {
                        error = 2;
                        return 0.0;
                    }
                }
                if (input == 'h')
                {
                    Next(1);
                    if (input == '(')
                    {
                        Next(1);
                        value = checkPrecision(System.Math.Tanh((Plus())));
                        if (input == ')')
                        {
                            Next(1);
                        }
                        else
                        {
                            error = 2;
                            return 0.0;
                        }
                    }
                }
            }
            else if (input == 'a')
            {
                Next(1);
                if (input == 's')
                {
                    Next(3);
                    if (input == '(')
                    {
                        Next(1);
                        value = checkPrecision(System.Math.Asin(Plus()));
                        if (input == ')')
                        {
                            Next(1);
                        }
                        else
                        {
                            error = 2;
                            return 0.0;
                        }
                    }
                }
                else if (input == 'c')
                {
                    Next(3);
                    if (input == '(')
                    {
                        Next(1);
                        value = checkPrecision(System.Math.Acos(Plus()));
                        if (input == ')')
                        {
                            Next(1);
                        }
                        else
                        {
                            error = 2;
                            return 0.0;
                        }
                    }
                }
                else if (input == 't')
                {
                    Next(3);
                    if (input == '(')
                    {
                        Next(1);
                        value = checkPrecision(System.Math.Atan(Plus()));
                        if (input == ')')
                        {
                            Next(1);
                        }
                        else
                        {
                            error = 2;
                            return 0.0;
                        }
                    }

                }
            }
            if (input == '!')
            {
                int sum = 1;
                for (int i = 1; i <= value; i++)
                    sum *= i;
                value = sum;
                Next(1);
            }
            return value;
        }

        private double checkPrecision(double index)
        {
            if (index < 0.00000000000001 && index > 0)
                index = 0.0;
            return index;
        }
    }
}
