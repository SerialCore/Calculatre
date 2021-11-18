#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "calculate.h"

void gethelp();
void rolling();
void calculator(char* _formula);


int main(int argc, char* argv[])
{
    if (argc == 1)
    {
        rolling();
    }
    else if (argc == 2)
    {
        if (!strcmp(argv[1], "help"))
        {
            gethelp();
        }
        else
        {
            calculator(argv[1]);
        }
    }

    return 0;
}

void rolling()
{
    char input[100];
    printf("Calculatre@ ");
    scanf("%s", input);

    while (strcmp(input, ""))
    {
        if (!strcmp(input, "quit"))
        {
            break;
        }
        else if (!strcmp(input, "help"))
        {
            gethelp();
        }
        else
        {
            calculator(input);
        }

        printf("Calculatre@ ");
        scanf("%s", input);
    }
}

void calculator(char* _formula)
{
    struct calculate cal = {.formula = _formula, .result = 0, .error = 0, .start = start};
    cal.start(&cal);

    switch (cal.error)
    {
    case 1:
        printf("div cannot be 0\n");
        break;

    case 2:
        printf("cannot recognize the input\n");
        break;

    default:
        printf("%s = %lf\n", cal.formula, cal.result);
        break;
    }
}

void gethelp()
{
    printf("supporting operations: ^, !, e, pi, +, -, *, /\n");
    printf("sin(), cos(), tan(), asin(), acos(), atan(), sinh(), cosh(), tanh(), exp(), ln(), lg()\n");
    printf("Please enter \"quit\" to exit this application.\n");
}