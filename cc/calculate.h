class calculate
{
private:
    char        input;          // the current char
    int         i;              // the char tracer
    int         length;         // the length of whole formula

    void        next(int _n);   // gets the next char of sequence
    double      plus();
    double      multi();
    double      power();
    double      signal();
    double      high();         // includes sin, cos, tan, asin, acos, atan, sinh, cosh, tanh, ln, lg

public:
    calculate(char* _formula);
    ~calculate();

    char*       formula;        // the formula as a string
    double      result;         // the return of plus(), the final result of calculation
    int         error;          // 1. div is 0; 2. char is unknown;

    void        start();        // the starter of this calculator
};
