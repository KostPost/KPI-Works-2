#include <iostream>
#include <limits>
#include <stack>
#include <string>
#include <utility>

using namespace std;

struct Product {
    string name;
    int quantity;
    double price;
    Product *left;
    Product *right;

    Product(string n, int q, double p) : name(std::move(n)), quantity(q), price(p), left(nullptr), right(nullptr) {
    }

    ~Product() {
        delete left;
        delete right;
    }
};

Product *insertProduct(Product *root, const string &name, int quantity, double price) {
    Product **current = &root;
    while (*current != nullptr) {
        if (name < (*current)->name) {
            current = &((*current)->left);
        } else if (name > (*current)->name) {
            current = &((*current)->right);
        } else {
            (*current)->quantity += quantity;
            return root;
        }
    }
    *current = new Product(name, quantity, price);
    return root;
}

double calculateTotalCost(Product *root, string name) {
    while (root != nullptr) {
        if (name == root->name) {
            return root->quantity * root->price;
        }
        if (name < root->name) {
            root = root->left;
        } else {
            root = root->right;
        }
    }
    return 0; // Return 0 if the product with the given name is not found
}


void printProducts(const Product *root) {
    stack<const Product *> s;
    const Product *current = root;

    while (current != nullptr || !s.empty()) {
        while (current != nullptr) {
            s.push(current);
            current = current->left;
        }

        current = s.top();
        s.pop();

        cout << "Product: " << current->name << ", Quantity: " << current->quantity << ", Price: " << current->price <<
                endl;

        current = current->right;
    }
}


int main() {
    Product *root = nullptr;
    char choice;

    string name;
    int quantity;
    double price;
    string searchName;
    double totalCost;
    bool isValidInput = false;

    do {
        cout << "1 - Add product\n2 - Calculate the total cost of the product\n3 - List all products\n4 - Exit\n";
        cin >> choice;

        switch (choice) {
            case '1':
                cout << "Enter product name:";
                cin >> name;

                try {
                    cout << "Enter quantity:1";
                    cin >> quantity;
                    if (cin.fail()) {
                        throw invalid_argument("Invalid input. Please enter a valid integer for quantity.");
                    }

                    cout << "Enter price: ";
                    cin >> price;
                    if (cin.fail()) {
                        throw invalid_argument("Invalid input. Please enter a valid double for price.");
                    }

                    root = insertProduct(root, name, quantity, price);
                } catch (const invalid_argument &e) {
                    cout << e.what() << endl;
                    cin.clear();
                    cin.ignore(numeric_limits<int>::max(), '\n');
                }
                break;


            case '2':
                cout << "Enter the name of the product to calculate the total cost:";
                cin >> searchName;

                totalCost = calculateTotalCost(root, searchName);
                cout << "Total cost of " << searchName << ": " << totalCost << endl;
                break;

            case '3':
                cout << "Listing all products:\n";
                printProducts(root);
                break;

            case '4':
                cout << "Exiting...\n";
                break;

            default:
                cout << "Invalid choice. Please enter 1, 2, 3, or 4.\n";
                break;
        }
    } while (choice != '4');

    return 0;
}
