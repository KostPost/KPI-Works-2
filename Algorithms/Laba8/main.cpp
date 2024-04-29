#include <iostream>
#include <vector>
#include <fstream>
#include <cmath>
#include <algorithm>
#include <limits>

using namespace std;

struct Point {
    double x, y;
};

double distance(const Point& p1, const Point& p2) {
    return sqrt(pow(p1.x - p2.x, 2) + pow(p1.y - p2.y, 2));
}

vector<Point> readGraph(const string& filename) {
    ifstream file(filename);
    if (!file.is_open()) {
        cerr << "Error: Unable to open file " << filename << endl;
        exit(1);
    }

    int n;
    file >> n;

    vector<Point> graph(n);
    for (int i = 0; i < n; ++i) {
        file >> graph[i].x >> graph[i].y;
    }

    file.close();
    return graph;
}

pair<double, vector<int>> tsp(const vector<Point>& graph) {
    int n = graph.size();
    vector<int> path;
    path.reserve(n);

    vector<bool> visited(n, false);
    visited[0] = true;
    path.push_back(0);

    double totalDistance = 0.0;

    for (int i = 1; i < n; ++i) {
        int nearest = -1;
        double minDist = numeric_limits<double>::max();
        for (int j = 0; j < n; ++j) {
            if (!visited[j]) {
                double dist = distance(graph[path.back()], graph[j]);
                if (dist < minDist) {
                    minDist = dist;
                    nearest = j;
                }
            }
        }
        totalDistance += minDist;
        path.push_back(nearest);
        visited[nearest] = true;
    }

    totalDistance += distance(graph[path.back()], graph[0]);
    path.push_back(0);

    return {totalDistance, path};
}

void writeResult(const string& filename, double distance, const vector<int>& path) {
    ofstream file(filename);
    if (!file.is_open()) {
        cerr << "Error: Unable to open file " << filename << endl;
        exit(1);
    }

    file << distance << endl;
    for (int i : path) {
        file << i << " ";
    }
    file << endl;

    file.close();
}

int main() {
    string inputFilename = R"(P:\KPI-Works 2\Algorithms\Laba8\input.txt)";
    string outputFilename = R"(P:\KPI-Works 2\Algorithms\Laba8\output.txt)";

    vector<Point> graph = readGraph(inputFilename);

    auto [distance, path] = tsp(graph);

    writeResult(outputFilename, distance, path);

    cout << "Shortest distance: " << distance << endl;
    cout << "Path: ";
    for (int i : path) {
        cout << i << " ";
    }
    cout << endl;

    return 0;
}
