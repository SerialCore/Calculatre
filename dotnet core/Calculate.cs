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
            error = 0;
            i = 0;
            length = formula.Length;
            this.i = 0;
            this.error = 0;
            this.formula = expression;
            this.length = this.formula.Length;
        }

        public void start()
        {
            // get the first char and luanch the calculator
            next(0);
            _value = plus();
        }

        private void next(int n)
        {
            i += n;
            if (i <= length - 1)
            {
                input = formula[i];
            }
        }

        private double plus()
        {
            double value = 0.0;
            // indirect recursion
            value = multi();
            while ((input == '+' || input == '-') && i != 0 && formula[i - 1] != '(')
            {
                if (input == '+')
                {
                    next(1);
                    value += multi();
                }
                else if (input == '-')
                {
                    next(1);
                    value -= multi();
                }
            }
            return value;
        }

        private double multi()
        {
            double div;
            double value = 0.0;
            value = power();
            while ((input == '*') || (input == '/'))
            {
                if (input == '*')
                {
                    next(1);
                    value *= power();
                }
                else if (input == '/')
                {
                    next(1);
                    div = power();
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

        private double power()
        {
            double value = 0.0;
            value = signal();
            while (input == '^')
            {
                if (input == '^')
                {
                    next(1);
                    value = System.Math.Pow(value, signal());
                }
            }
            return value;
        }

        private double signal()
        {
            double value = 0.0;
            if (input == '-' && (i == 0 || formula[i - 1] == '('))
            {
                next(1);
                value = -high();
            }
            else if (input == '+' && (i == 0 || formula[i - 1] == '('))
            {
                next(1);
                value = high();
            }
            else if (input != '-' && input != '+' && input != '\0')
            {
                value = high();
            }
            return value;
        }

        private double high()
        {
            double value = 0.0;
            if (input == '(')
            {
                next(1);
                value = plus();
                if (input == ')')
                {
                    next(1);
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
                next(0);
            }
            else if (input == 'e')
            {

                next(1);
                if (input != 'x')
                    value = Math.E;
                else
                {
                    next(2);
                    if (input == '(')
                    {
                        next(1);
                        value = System.Math.Exp(plus());
                        if (input == ')')
                        {
                            next(1);
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
                next(1);
                if (input == 'i')
                {
                    value = Math.PI;
                    next(1);
                }
            }
            else if (input == 'l')
            {
                next(1);
                if (input == 'g')
                {
                    next(1);
                    if (input == '(')
                    {
                        next(1);
                        value = System.Math.Log10(plus());
                        if (input == ')')
                        {
                            next(1);
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
                    next(1);
                    if (input == '(')
                    {
                        next(1);
                        value = System.Math.Log(plus());
                        if (input == ')')
                        {
                            next(1);
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
                next(3);
                if (input == '(')
                {
                    next(1);
                    value = checkPrecision(System.Math.Sin((plus())));
                    if (input == ')')
                    {
                        next(1);
                    }
                    else
                    {
                        error = 2;
                        return 0.0;
                    }
                }
                if (input == 'h')
                {
                    next(1);
                    if (input == '(')
                    {
                        next(1);
                        value = checkPrecision(System.Math.Sinh((plus())));
                        if (input == ')')
                        {
                            next(1);
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
                next(3);
                if (input == '(')
                {
                    next(1);
                    value = checkPrecision(System.Math.Cos((plus())));
                    if (input == ')')
                    {
                        next(1);
                    }
                    else
                    {
                        error = 2;
                        return 0.0;
                    }
                }
                if (input == 'h')
                {
                    next(1);
                    if (input == '(')
                    {
                        next(1);
                        value = checkPrecision(System.Math.Cosh((plus())));
                        if (input == ')')
                        {
                            next(1);
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
                next(3);
                if (input == '(')
                {
                    next(1);
                    value = checkPrecision(System.Math.Tan((plus())));
                    if (input == ')')
                    {
                        next(1);
                    }
                    else
                    {
                        error = 2;
                        return 0.0;
                    }
                }
                if (input == 'h')
                {
                    next(1);
                    if (input == '(')
                    {
                        next(1);
                        value = checkPrecision(System.Math.Tanh((plus())));
                        if (input == ')')
                        {
                            next(1);
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
                next(1);
                if (input == 's')
                {
                    next(3);
                    if (input == '(')
                    {
                        next(1);
                        value = checkPrecision(multis * System.Math.Asin(plus()));
                        if (input == ')')
                        {
                            next(1);
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
                    next(3);
                    if (input == '(')
                    {
                        next(1);
                        value = checkPrecision(multis * System.Math.Acos(plus()));
                        if (input == ')')
                        {
                            next(1);
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
                    next(3);
                    if (input == '(')
                    {
                        next(1);
                        value = checkPrecision(multis * System.Math.Atan(plus()));
                        if (input == ')')
                        {
                            next(1);
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
                i++;
                Input();
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
