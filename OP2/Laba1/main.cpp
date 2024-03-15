#include <iostream>
#include <limits>
#include <stdexcept>

using namespace std;

void incrementCounter(int &value) {
    value += 1;
}

int incrementCounterReturn(int value) {
    return value + 1;
}

void isLessThan(int a, int b, bool &result) {
    result = a < b;
}

bool isLessThanReturn(int a, int b) {
    return a < b;
}

int main() {

    cout << boolalpha;

    int testIncrementValues[] = {-87, 511, 183};
    int testLessThanValuesA[] = {100, 100};
    int testLessThanValuesB[] = {-8, 132};
    int testLessThanValuesC[] = {100, 131};

    cout << "Testing incrementCounter function:" << endl;
    for (int value : testIncrementValues) {
        incrementCounter(value);
        cout << "Result after incrementing: " << value << endl;
    }

    cout << "\nTesting incrementCounterReturn function:" << endl;
    for (int value : testIncrementValues) {
        cout << "Original value: " << value << " Result after incrementing: " << incrementCounterReturn(value) << endl;
    }

    cout << "\nTesting isLessThan function:" << endl;
    bool result;
    for (size_t i = 0; i < 2; ++i) {
        isLessThan(testLessThanValuesA[i], testLessThanValuesB[i], result);
        cout << testLessThanValuesA[i] << " < " << testLessThanValuesB[i] << ":" << result << endl;
    }

    cout << "\nTesting isLessThanReturn function:" << endl;
    for (size_t i = 0; i < 2; ++i) {
        cout << testLessThanValuesA[i] << " < " << testLessThanValuesC[i] << ":"
             << isLessThanReturn(testLessThanValuesA[i], testLessThanValuesC[i]) << endl;
    }


    return 0;
}
