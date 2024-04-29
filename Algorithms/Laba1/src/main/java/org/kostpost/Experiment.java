package org.kostpost;

import java.util.Arrays;
import java.util.Random;

public class Experiment {

    // Метод для генерації впорядкованого масиву
    public static int[] generateSortedArray(int size) {
        int[] arr = new int[size];
        for (int i = 0; i < size; i++) {
            arr[i] = i;
        }
        return arr;
    }

    // Метод для генерації зворотно впорядкованого масиву
    public static int[] generateReverseSortedArray(int size) {
        int[] arr = new int[size];
        for (int i = 0; i < size; i++) {
            arr[i] = size - i;
        }
        return arr;
    }

    // Метод для генерації масиву випадкових чисел
    public static int[] generateRandomArray(int size) {
        Random random = new Random();
        int[] arr = new int[size];
        for (int i = 0; i < size; i++) {
            arr[i] = random.nextInt(size * 10); // Випадкові числа від 0 до size * 10
        }
        return arr;
    }

    // Метод для вимірювання часу виконання сортування
    public static long measureSortingTime(int[] arr) {
        long startTime = System.nanoTime();
        // Тут викличете ваш метод сортування
        sort(arr);
        long endTime = System.nanoTime();
        return endTime - startTime;
    }

    // Ваш сортувальний алгоритм (наприклад, сортування бульбашкою)
    public static void sort(int[] arr) {
        // Реалізація вашого сортувального алгоритму
        Arrays.sort(arr); // Тимчасово використовуємо вбудовану функцію сортування
    }

    public static void main(String[] args) {
        // Розміри масивів для експериментів
        int[] sizes = {10, 100, 1000, 5000, 10000, 20000, 50000};

        // Проведення експериментів для різних розмірностей та наборів даних
        for (int size : sizes) {
            int[] sortedArray = generateSortedArray(size);
            int[] reverseSortedArray = generateReverseSortedArray(size);
            int[] randomArray = generateRandomArray(size);

            long sortingTimeSorted = measureSortingTime(sortedArray);
            long sortingTimeReverseSorted = measureSortingTime(reverseSortedArray);
            long sortingTimeRandom = measureSortingTime(randomArray);

            System.out.println("Size: " + size);
            System.out.println("Sorting time for sorted array: " + sortingTimeSorted + " ns");
            System.out.println("Sorting time for reverse sorted array: " + sortingTimeReverseSorted + " ns");
            System.out.println("Sorting time for random array: " + sortingTimeRandom + " ns");
            System.out.println();
        }
    }
}

