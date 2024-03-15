#include <iostream>
#include <cmath>

class Figure {
protected:
    double x1, y1;
    double x2, y2;
    double x3, y3;
    double x4, y4;

public:
    Figure(double ax1, double ay1, double ax2, double ay2,
           double ax3, double ay3, double ax4, double ay4)
            : x1(ax1), y1(ay1), x2(ax2), y2(ay2),
              x3(ax3), y3(ay3), x4(ax4), y4(ay4) {}

    double sideLength(double x1, double y1, double x2, double y2) {
        return sqrt(pow(x2 - x1, 2) + pow(y2 - y1, 2));
    }
};

class Trapezoid : public Figure {
public:
    Trapezoid(double ax1, double ay1, double ax2, double ay2,
              double ax3, double ay3, double ax4, double ay4)
            : Figure(ax1, ay1, ax2, ay2, ax3, ay3, ax4, ay4) {}


    double area() {
        double a = sideLength(x1, y1, x2, y2);
        double b = sideLength(x3, y3, x4, y4);
        double h = abs(y1 - y3);
        return (a + b) / 2 * h;
    }

    double perimeter() {
        double a = sideLength(x1, y1, x2, y2);
        double b = sideLength(x3, y3, x4, y4);
        double c = sideLength(x2, y2, x3, y3);
        double d = sideLength(x4, y4, x1, y1);
        return a + b + c + d;
    }


    void printCoordinates() {
        std::cout << "Vertices of the trapezoid: (" << x1 << ", " << y1 << "), "
                  << "(" << x2 << ", " << y2 << "), "
                  << "(" << x3 << ", " << y3 << "), "
                  << "(" << x4 << ", " << y4 << ")\n";
    }
};

int main() {

    Trapezoid t(0, 0, 4, 0, 3, 2, 1, 2);
    t.printCoordinates();
    std::cout << "Area of the trapezoid: " << t.area() << std::endl;
    std::cout << "Perimeter of the trapezoid: " << t.perimeter() << std::endl;

    return 0;
}
