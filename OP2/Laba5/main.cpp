#include <iostream>
#include <string>
#include <algorithm>
#include <utility>

class StringBase {
public:
    virtual ~StringBase() = default;

    [[nodiscard]] virtual size_t length() const = 0;

    virtual void sortAndPrint() const = 0;
};

class UpperCaseString : public StringBase {
private:
    std::string value;

public:
    UpperCaseString(std::string  val) : value(std::move(val)) {}

    [[nodiscard]] size_t length() const override {
        return value.length();
    }

    void sortAndPrint() const override {
        std::string sortedValue = value;
        std::sort(sortedValue.begin(), sortedValue.end());
        std::cout << "Sorted (Ascending): " << sortedValue << std::endl;
    }
};

class LowerCaseString : public StringBase {
private:
    std::string value;

public:
    explicit LowerCaseString(std::string  val) : value(std::move(val)) {}

    size_t length() const override {
        return value.length();
    }

    void sortAndPrint() const override {
        std::string sortedValue = value;
        std::sort(sortedValue.rbegin(), sortedValue.rend());
        std::cout << "Sorted (Descending): " << sortedValue << std::endl;
    }
};

int main() {
    UpperCaseString upper("HELLO WORLD");
    LowerCaseString lower("hello world");

    StringBase* strings[] = {&upper, &lower};

    for (auto* str : strings) {
        std::cout << "Length: " << str->length() << std::endl;
        str->sortAndPrint();
    }

    return 0;
}
