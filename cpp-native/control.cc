#include <string>
#include <string.h>
#include <iostream>
#include "control.h"
#include "calculate.h"

using namespace std;

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
    string input;
    cout << "Calculatre@ " << ends;
    cin >> input;

    while (!input.empty())
    {
        if (!input.compare("quit"))
        {
            break;
        }
        else if (!input.compare("help"))
        {
            gethelp();
        }
        else
        {
            calculator(const_cast<char*>(input.c_str()));
        }

        cout << "Calculatre@ " << ends;
        cin >> input;
    }
}

void calculator(char* _formula)
{
    calculate* cal = new calculate(_formula);
    cal->start();

    switch (cal->error)
    {
    case 1:
        cout << "div cannot be 0" << endl;
        break;

    case 2:
        cout << "cannot recognize the input" << endl;
        break;

    default:
        cout << cal->formula << " = " << cal->result << endl;
        break;
    }
}

void gethelp()
{
    cout << "supporting operations: ^, !, e, pi, +, -, *, /" << endl;
    cout << "sin(), cos(), tan(), asin(), acos(), atan(), sinh(), cosh(), tanh(), exp(), ln(), lg()" << endl;
    cout << "Please enter \"quit\" to exit this application." << endl;
}