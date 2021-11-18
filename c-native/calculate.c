#include <math.h>
#include <stdlib.h>
#include "calculate.h"

// private members
struct _calculate
{
    char*       formula;        // the formula as a string
    int         length;         // the length of whole formula
    char        input;          // the current char
    int         i;              // the char tracer
    
    double      result;         // the return of plus(), the final result of calculation
    int         error;          // 1. div is 0; 2. char is unknown;
} this;

void    next(int _n);   // gets the next char of sequence
double  plus();
double  multi();
double  power();
double  signal();
double  high();         // includes sin, cos, tan, asin, acos, atan, sinh, cosh, tanh, ln, lg


void start(struct calculate* current)
{
    // initialize the variates
    this.formula = current->formula;
    this.i = 0;
    this.error = 0;
    this.length = 0;
    while (this.formula[this.length] != '\0')
    {
        this.length++;
    }

    // get the first char and launch the calculator
    next(0);
    this.result = plus();

    // return the result
    current->result = this.result;
    current->error = this.error;
}

void next(int _n)
{
    this.i += _n;
    if (this.i <= this.length - 1)
    {
        this.input = this.formula[this.i];
    }
}

double plus()
{
    double value = 0.0;
    // indirect recursion
    value = multi();
    while ((this.input == '+' || this.input == '-') && this.i != 0 && this.formula[this.i - 1] != '(')
    {
        if (this.input == '+')
        {
            next(1);
            value += multi();
        }
        else if (this.input == '-')
        {
            next(1);
            value -= multi();
        }
    }
    return value;
}

double multi()
{
    double div;
    double value = 0.0;
    value = power();
    while ((this.input == '*') || (this.input == '/'))
    {
        if (this.input == '*')
        {
            next(1);
            value *= power();
        }
        else if (this.input == '/')
        {
            next(1);
            div = power();
            if (div == 0)
            {
                this.error = 1;
                return 0.0;
            }
            value /= div;
        }
    }
    return value;
}

double power()
{
    double value = 0.0;
    value = signal();
    while (this.input == '^')
    {
        if (this.input == '^')
        {
            next(1);
            value = pow(value, signal());
        }
    }
    return value;
}

double signal()
{
    double value = 0.0;
    if (this.input == '-' && (this.i == 0 || this.formula[this.i - 1] == '('))
    {
        next(1);
        value = -high();
    }
    else if (this.input == '+' && (this.i == 0 || this.formula[this.i - 1] == '('))
    {
        next(1);
        value = high();
    }
    else if (this.input != '-' && this.input != '+' && this.input != '\0')
    {
        value = high();
    }
    return value;
}

double high()
{
    double value = 0.0;
    if (this.input == '(')
    {
        next(1);
        // the operation in the bracket maybe plus
        value = plus();
        if (this.input == ')')
        {
            next(1);
        }
        else
        {
            this.error = 2;
            return 0.0;
        }
    }
    else if (this.input >= '0' && this.input <= '9')
    {
        char num[20] = "";
        for (int j = 0; this.formula[this.i] >= '0' && this.formula[this.i] <= '9' || this.formula[this.i] == '.'; j++)
        {
            num[j] += this.formula[this.i];
            this.i++;
            if (this.i > this.length - 1)
                break;
        }
        value = atof(num);
        next(0);
    }
    else if (this.input == 'e')
    {
        next(1);
        if (this.input != 'x')
            value = M_E;
        else
        {
            next(2);
            if (this.input == '(')
            {
                next(1);
                value = exp(plus());
                if (this.input == ')')
                {
                    next(1);
                }
                else
                {
                    this.error = 2;
                    return 0.0;
                }
            }
        }
    }
    else if (this.input == 'p')
    {
        next(1);
        if (this.input == 'i')
        {
            value = M_PI;
            next(1);
        }
    }
    else if (this.input == 'l')
    {
        next(1);
        if (this.input == 'g')
        {
            next(1);
            if (this.input == '(')
            {
                next(1);
                value = log10(plus());
                if (this.input == ')')
                {
                    next(1);
                }
                else
                {
                    this.error = 2;
                    return 0.0;
                }
            }
        }
        else if (this.input == 'n')
        {
            next(1);
            if (this.input == '(')
            {
                next(1);
                value = log(plus());
                if (this.input == ')')
                {
                    next(1);
                }
                else
                {
                    this.error = 2;
                    return 0.0;
                }
            }
        }
    }
    else if (this.input == 's')
    {
        next(3);
        if (this.input == '(')
        {
            next(1);
            value = sin(plus());
            if (this.input == ')')
            {
                next(1);
            }
            else
            {
                this.error = 2;
                return 0.0;
            }
        }
        if (this.input == 'h')
        {
            next(1);
            if (this.input == '(')
            {
                next(1);
                value = sinh(plus());
                if (this.input == ')')
                {
                    next(1);
                }
                else
                {
                    this.error = 2;
                    return 0.0;
                }
            }
        }
    }
    else if (this.input == 'c')
    {
        next(3);
        if (this.input == '(')
        {
            next(1);
            value = cos(plus());
            if (this.input == ')')
            {
                next(1);
            }
            else
            {
                this.error = 2;
                return 0.0;
            }
        }
        if (this.input == 'h')
        {
            next(1);
            if (this.input == '(')
            {
                next(1);
                value = cosh(plus());
                if (this.input == ')')
                {
                    next(1);
                }
                else
                {
                    this.error = 2;
                    return 0.0;
                }
            }
        }
    }
    else if (this.input == 't')
    {
       next(3);
        if (this.input == '(')
        {
            next(1);
            value = tan(plus());
            if (this.input == ')')
            {
                next(1);
            }
            else
            {
                this.error = 2;
                return 0.0;
            }
        }
        if (this.input == 'h')
        {
            next(1);
            if (this.input == '(')
            {
                next(1);
                value = tanh(plus());
                if (this.input == ')')
                {
                    next(1);
                }
                else
                {
                    this.error = 2;
                    return 0.0;
                }
            }
        }
    }
    else if (this.input == 'a')
    {
        next(1);
        if (this.input == 's')
        {
            next(3);
            if (this.input == '(')
            {
                next(1);
                value = asin(plus());
                if (this.input == ')')
                {
                    next(1);
                }
                else
                {
                    this.error = 2;
                    return 0.0;
                }
            }
        }
        else if (this.input == 'c')
        {
            next(3);
            if (this.input == '(')
            {
                next(1);
                value = acos(plus());
                if (this.input == ')')
                {
                    next(1);
                }
                else
                {
                    this.error = 2;
                    return 0.0;
                }
            }
        }
        else if (this.input == 't')
        {
            next(3);
            if (this.input == '(')
            {
                next(1);
                value = atan(plus());
                if (this.input == ')')
                {
                    next(1);
                }
                else
                {
                    this.error = 2;
                    return 0.0;
                }
            }
        }
    }
    if (this.input == '!')
    {
        for (int num = value; num >= 1; --num)
            value *= num;
        next(1);
    }
    return value;
}