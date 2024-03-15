#include <iostream>
#include <cmath>

class Point {
public:
    double x, y;

    Point(double x = 0.0, double y = 0.0) : x(x), y(y) {}
};

class Segment {
private:
    Point start, end;

public:
    Segment() : start(Point()), end(Point()) {}

    Segment(Point start, Point end) : start(start), end(end) {}

    Segment(const Segment &other) : start(other.start), end(other.end) {}

    double length() const {
        return std::hypot(end.x - start.x, end.y - start.y);
    }

    [[nodiscard]] Point getStart() const { return start; }
    [[nodiscard]] Point getEnd() const { return end; }

    Segment operator+(const Segment &other) const {
        Point newEnd(end.x + other.end.x - other.start.x,
                     end.y + other.end.y - other.start.y);
        return Segment(start, newEnd);
    }

    Segment operator-(const Segment &other) const {
        Point newEnd(end.x - other.end.x + other.start.x,
                     end.y - other.end.y + other.start.y);
        return {start, newEnd};
    }

    void print() const {
        std::cout << "Segment[(" << start.x << ", " << start.y << "), ("
                  << end.x << ", " << end.y << ")]" << std::endl;
    }
};

int main() {
    Point p1(0, 0), p2(2, 3);
    Segment s1(p1, p2);

    Point p3(1, 1), p4(4, 5);
    Segment s2(p3, p4);

    Segment s3 = s1 + s2;
    Segment s4 = s1 - s2;

    std::cout << "Length of Segment s1: " << s1.length() << std::endl;
    std::cout << "Length of Segment s2: " << s2.length() << std::endl;
    std::cout << "Segment s3 (s1 + s2): "; s3.print();
    std::cout << "Segment s4 (s1 - s2): "; s4.print();

    return 0;
}
