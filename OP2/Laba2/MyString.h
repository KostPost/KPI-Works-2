#ifndef MYSTRING_H
#define MYSTRING_H

#include <string>

class MyString {
private:
    std::string content;

public:
    MyString();
    explicit MyString(std::string str);

    void trim();
    void reduceSpaces();
    size_t length() const;
    double consonantPercentage() const;
    const std::string &getString() const;
};

#endif // MYSTRING_H
