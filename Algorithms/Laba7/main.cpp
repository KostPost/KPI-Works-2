#include <chrono>
#include <iostream>
#include <vector>
#include <string>
#include <limits>

using namespace std;

unsigned int fnv1Hash(const string &key) {
    const unsigned int FNV_offset_basis = 2166136261;
    const unsigned int FNV_prime = 16777619;
    unsigned int hash = FNV_offset_basis;

    for (char c: key) {
        hash ^= static_cast<unsigned int>(c);
        hash *= FNV_prime;
    }

    return hash;
}

struct HashEntry {
    string key;
    int value;
    bool isEmpty;

    HashEntry() : key(""), value(0), isEmpty(true) {
    }
};

class HashTable {
private:
    vector<HashEntry> table;
    int capacity;
    int size;

    int doubleHash(const string &key, int i) const {
        unsigned int hash1 = fnv1Hash(key);
        unsigned int hash2 = fnv1Hash(to_string(i));
        return (hash1 + i * hash2) % capacity;
    }

public:
    explicit HashTable(int capacity) : capacity(capacity), size(0) {
        table.resize(capacity);
    }

    void insert(const string &key, int value) {
        if (size == capacity) {
            cout << "Hash table is full!" << endl;
            return;
        }

        int index = fnv1Hash(key) % capacity;
        int i = 0;
        while (!table[index].isEmpty) {
            index = (index + doubleHash(key, i)) % capacity;
            ++i;
        }

        table[index].key = key;
        table[index].value = value;
        table[index].isEmpty = false;
        size++;
    }

    int get(const string &key) {
        int index = fnv1Hash(key) % capacity;
        int i = 0;
        while (!table[index].isEmpty && table[index].key != key) {
            index = (index + doubleHash(key, i)) % capacity;
            ++i;
        }

        if (table[index].key == key) {
            return table[index].value;
        } else {
            cout << "Key not found!" << endl;
            return -1;
        }
    }

    int getSize() const {
        return size;
    }

    void printAll() {
        for (const auto &entry: table) {
            if (!entry.isEmpty) {
                cout << "Key: " << entry.key << ", Value: " << entry.value << endl;
            }
        }
    }
};


int main() {
    int capacity;
    cout << "Enter capacity of hash table:";
    cin >> capacity;
    cin.ignore(numeric_limits<streamsize>::max(), '\n');

    HashTable hashTable(capacity);

    while (true) {
        cout << "\n1. Add an element\n2. View all elements\n3. Search for a value by key\n4. Exit\n";
        cout << "Enter your choice:";
        int choice;
        cin >> choice;
        cin.ignore(numeric_limits<streamsize>::max(), '\n');

        switch (choice) {
            case 1: {
                if (hashTable.getSize() >= capacity) {
                    cout << "Hash table is full. Cannot add more elements." << endl;
                    break;
                }

                string key;
                int value;
                cout << "Enter key:";
                getline(cin, key);
                cout << "Enter value:";
                cin >> value;
                cin.ignore(numeric_limits<streamsize>::max(), '\n');

                hashTable.insert(key, value);
                cout << "Element added successfully!" << endl;
                break;
            }
            case 2:
                cout << "\nAll data in hash table:" << endl;
            hashTable.printAll();
            break;
            case 3: {
                string key;
                cout << "Enter key to search:";
                getline(cin, key);
                int value = hashTable.get(key);
                if (value != -1) {
                    cout << "Value: " << value << endl;
                }
                break;
            }
            case 4:
                cout << "Exiting program. Bye!" << endl;
            return 0;
            default:
                cout << "Invalid choice! Please enter a valid option." << endl;
            break;
        }
    }

}