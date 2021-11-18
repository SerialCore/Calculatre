// public members
struct calculate
{
    char*       formula;        // the formula as a string
    double      result;         // the return of plus(), the final result of calculation
    int         error;          // 1. div is 0; 2. char is unknown;

    void(*      start)(struct calculate* current);
};

void start(struct calculate* current);
