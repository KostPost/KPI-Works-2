#include <chrono>
#include <iostream>
#include <vector>
#include <string>
#include <limits>

using namespace std;

unsigned int fnv1Hash(const string& key) {
    const unsigned int FNV_offset_basis = 2166136261;
    const unsigned int FNV_prime = 16777619;
    unsigned int hash = FNV_offset_basis;

    for (char c : key) {
        hash ^= static_cast<unsigned int>(c);
        hash *= FNV_prime;
    }

    return hash;
}

struct HashEntry {
    string key;
    int value;
    bool isEmpty;
    HashEntry() : key(""), value(0), isEmpty(true) {}
};

class HashTable {
private:
    vector<HashEntry> table;
    int capacity;
    int size;

    int doubleHash(const string& key, int i) const {
        unsigned int hash1 = fnv1Hash(key);
        unsigned int hash2 = fnv1Hash(to_string(i));
        return (hash1 + i * hash2) % capacity;
    }

public:
    explicit HashTable(int capacity) : capacity(capacity), size(0) {
        table.resize(capacity);
    }

    void insert(const string& key, int value) {
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

    int get(const string& key) {
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
        for (const auto& entry : table) {
            if (!entry.isEmpty) {
                cout << "Key: " << entry.key << ", Value: " << entry.value << endl;
            }
        }
    }
};

int main() {
    // Зчитуємо розмір структури від користувача
    int capacities[] = {100, 1000, 5000, 10000, 20000};

    for (int capacity : capacities) {
        HashTable hashTable(capacity);

        auto start = chrono::high_resolution_clock::now(); // Початок вимірювання часу

        // Ваш код, що заповнює хеш-таблицю

        auto stop = chrono::high_resolution_clock::now(); // Кінець вимірювання часу
        auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start); // Розрахунок тривалості

        cout << "Time taken for capacity " << capacity << ": " << duration.count() << " milliseconds" << endl;
    }

    return 0;
}


// int main() {
//     int capacity;
//     cout << "Enter capacity of hash table:";
//     cin >> capacity;
//     cin.ignore(numeric_limits<streamsize>::max(), '\n');
//
//     HashTable hashTable(capacity);
//
//     string key;
//     int value;
//
//     while (true) {
//         if (hashTable.getSize() >= capacity) {
//             cout << "Hash table is full. Cannot add more elements." << endl;
//             break;
//         }
//
//         cout << "Enter key (type 'exit' to stop):";
//         getline(cin, key);
//         if (key == "exit") {
//             break;
//         }
//
//         cout << "Enter value:";
//         cin >> value;
//         cin.ignore(numeric_limits<streamsize>::max(), '\n');
//
//         hashTable.insert(key, value);
//         cout << "Remaining capacity: " << capacity - hashTable.getSize() << endl;
//     }
//
//     cout << "All data in hash table:" << endl;
//     hashTable.printAll();
//
//     return 0;
// }
