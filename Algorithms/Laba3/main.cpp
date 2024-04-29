#include<bits/stdc++.h>
using namespace std;

vector<vector<int>> edges;

void addEdge(int u, int v) {
    edges[u][v] += 1;
    edges[v][u] += 1;
}

int DFSCount(int start, vector<bool>& visited) {
    stack<int> s;
    s.push(start);
    visited[start] = true;
    int count = 0;
    while (!s.empty()) {
        int v = s.top();
        s.pop();
        count++;
        for (int i = 0; i < edges[v].size(); i++) {
            if (edges[v][i] && !visited[i]) {
                s.push(i);
                visited[i] = true;
            }
        }
    }
    return count;
}


bool validEdge(int u, int v) {
    int count = 0;
    for (int i : edges[u])
        if (i)
            count++;
    if (count == 1)
        return true;

    vector<bool> visited(edges.size(), false);
    int count1 = DFSCount(u, visited);

    edges[u][v]--;
    edges[v][u]--;
    visited.clear();
    visited.resize(edges.size(), false);
    int count2 = DFSCount(u, visited);

    edges[u][v]++;
    edges[v][u]++;
    return !(count1 > count2);
}

void printEuclideanTour(int start) {
    stack<int> s;
    s.push(start);

    while (!s.empty()) {
        int u = s.top();
        bool found = false;

        for (int v = 0; v < edges.size(); ++v) {
            if (edges[u][v] && validEdge(u, v)) {
                cout << u << "-" << v << " ";
                edges[u][v]--;
                edges[v][u]--;
                s.push(v);
                found = true;
                break;
            }
        }

        if (!found) {
            s.pop();
        }
    }
}


int main() {
    edges.resize(4, vector<int>(4, 0));
    addEdge(0, 1);
    addEdge(0, 2);
    addEdge(1, 2);
    addEdge(2, 0);
    addEdge(2, 3);
    addEdge(3, 3);
    printEuclideanTour(2);

    return 0;
}