#include "TextContainer.h"
#include <algorithm>
#include <iostream>
#include <cstring>
#include <cctype>

void TextContainer::addLine(const MyString &line) {
    lines.push_back(line);
}

void TextContainer::removeLine(size_t index) {
    if (index < lines.size()) {
        lines.erase(lines.begin() + index);
    }
}

void TextContainer::clearText() {
    lines.clear();
}

size_t TextContainer::shortestLineLength() const {
    if (lines.empty()) {
        return 0;
    }
    return std::min_element(lines.begin(), lines.end(), [](const MyString &a, const MyString &b) {
        return a.length() < b.length();
    })->length();
}

double TextContainer::overallConsonantPercentage() const {
    int totalConsonants = 0;
    int totalLetters = 0;

    for (const auto &line : lines) {
        const std::string& str = line.getString();
        totalConsonants += std::count_if(str.begin(), str.end(), [](const char& c) {
            return std::isalpha(c) && !std::strchr("aeiouAEIOU", c);
        });
        totalLetters += std::count_if(str.begin(), str.end(), [](const char& c) {
            return std::isalpha(c);
        });
    }

    return totalLetters > 0 ? (static_cast<double>(totalConsonants) / totalLetters) * 100.0 : 0.0;
}


void TextContainer::printText() const {
    for (const auto &line : lines) {
        std::cout << line.getString() << std::endl;
    }
}
