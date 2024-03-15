#ifndef TEXTCONTAINER_H
#define TEXTCONTAINER_H

#include "MyString.h"
#include <vector>

class TextContainer {

public:
    void addLine(const MyString &line);
    void removeLine(size_t index);
    void clearText();
    [[nodiscard]] size_t shortestLineLength() const;
    [[nodiscard]] double overallConsonantPercentage() const;
    void printText() const;

    std::vector<MyString> lines;
};

#endif // TEXTCONTAINER_H
