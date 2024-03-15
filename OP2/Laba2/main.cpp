#include <iostream>
#include "MyString.h"
#include "TextContainer.h"


int main() {
    TextContainer text;
    text.addLine(MyString("This is a line with   multiple spaces."));
    text.addLine(MyString("   This line has spaces at the start and end.   "));
    text.addLine(MyString("Short"));
    text.addLine(MyString("Longest line of text with some consonants"));

    std::cout << "Original text:" << std::endl;
    text.printText();

    for (auto & line : text.lines) {
        line.trim();
        line.reduceSpaces();
    }

    std::cout << "\nCleaned text:" << std::endl;
    text.printText();

    std::cout << "\nShortest line length: " << text.shortestLineLength() << std::endl;
    std::cout << "Overall consonant percentage: " << text.overallConsonantPercentage() << "%" << std::endl;

    return 0;
}