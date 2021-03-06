#include <cmath>
#include <string>
#include "calculate.h"

using namespace std;


calculate::calculate(char* _formula)
{
    // initialize the variates
    this->formula = _formula;
    this->i = 0;
    this->error = 0;
    this->length = 0;
    while (formula[length] != '\0')
    {
        length++;
    }
}

calculate::~calculate()
{
    delete this->formula;
}

void calculate::start()
{
    // get the first char and launch the calculator
    next(0);
    result = plus();
}

void calculate::next(int _n)
{
    i += _n;
    if (i <= length - 1)
    {
        input = formula[i];
    }
}

double calculate::plus()
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

double calculate::multi()
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

double calculate::power()
{
    double value = 0.0;
    value = signal();
    while (input == '^')
    {
        if (input == '^')
        {
            next(1);
            value = pow(value, signal());
        }
    }
    return value;
}

double calculate::signal()
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

double calculate::high()
{
    double value = 0.0;
    if (input == '(')
    {
        next(1);
        // the operation in the bracket maybe plus
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
        value = stod(num);
        // value = atof(num.c_str());
        next(0);
    }
    else if (input == 'e')
    {
        next(1);
        if (input != 'x')
            value = M_E;
        else
        {
            next(2);
            if (input == '(')
            {
                next(1);
                value = exp(plus());
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
            value = M_PI;
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
                value = log10(plus());
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
                value = log(plus());
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
            value = sin(plus());
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
                value = sinh(plus());
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
            value = cos(plus());
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
                value = cosh(plus());
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
            value = tan(plus());
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
                value = tanh(plus());
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
                value = asin(plus());
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
                value = acos(plus());
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
                value = atan(plus());
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
        for (int num = value; num >= 1; --num)
            value *= num;
        next(1);
    }
    return value;
}