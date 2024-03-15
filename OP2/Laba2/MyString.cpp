#include "MyString.h"
#include <algorithm>
#include <cctype>
#include <cstring>
#include <utility>

MyString::MyString() : content("") {}

MyString::MyString(std::string str) : content(std::move(str)) {}

void MyString::trim() {
    auto left = std::find_if_not(content.begin(), content.end(), ::isspace);
    content.erase(content.begin(), left);
    auto right = std::find_if_not(content.rbegin(), content.rend(), ::isspace).base();
    content.erase(right, content.end());
}

void MyString::reduceSpaces() {
    auto new_end = std::unique(content.begin(), content.end(), [](char lhs, char rhs) {
        return (lhs == rhs) && (lhs == ' ');
    });
    content.erase(new_end, content.end());
}

size_t MyString::length() const {
    return content.length();
}

double MyString::consonantPercentage() const {
    int consonants = 0;
    for (char ch : content) {
        if (std::isalpha(ch) && !std::strchr("aeiouAEIOU", ch)) {
            consonants++;
        }
    }
    return content.empty() ? 0.0 : 100.0 * consonants / content.length();
}

const std::string &MyString::getString() const {
    return content;
}
