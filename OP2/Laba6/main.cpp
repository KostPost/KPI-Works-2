#include <iostream>
#include <cmath>
#include <stdexcept>
#include <vector>

class Expression {
private:
    double a, b, c, d;

public:
    Expression() : a(0), b(0), c(0), d(1) {}

    void setValues(double a, double b, double c, double d) {
        if (d == 0) {
            throw std::invalid_argument("Argument 'd' cannot be zero.");
        }
        this->a = a;
        this->b = b;
        this->c = c;
        this->d = d;
    }

    double compute() {
        double denominator = b + c/d - 1;
        if (denominator == 0) {
            throw std::runtime_error("Denominator cannot be zero.");
        }
        return log((4 * b - c) * a) / denominator;
    }
};

int main() {
    std::vector<Expression> expressions;
    int numberOfExpressions;
    std::cout << "Enter the number of expressions:";
    std::cin >> numberOfExpressions;

   
    if (numberOfExpressions <= 0) {
        std::cerr << "The number of expressions should be a positive integer." << std::endl;
        return 1;
    }

    for (int i = 0; i < numberOfExpressions; ++i) {
        double a, b, c, d;
        std::cout << "Enter values for expression " << (i+1) << " (a, b, c, d): ";
        std::cin >> a >> b >> c >> d;

        try {
            Expression expr;
            expr.setValues(a, b, c, d);
            expressions.push_back(expr);
        } catch (const std::exception& e) {
            std::cerr << "Error: " << e.what() << std::endl;
            --i;
        }
    }

    try {

        for (size_t i = 0; i < expressions.size(); ++i) {
            std::cout << "Result of expression" << (i+1) << ": " << expressions[i].compute() << std::endl;
        }
    } catch (const std::exception& e) {
        std::cerr << "Error while computing:" << e.what() << std::endl;
    }

    return 0;
}
