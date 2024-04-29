package org.kostpost;

import java.util.Arrays;

public class Experiment {

    public static void main(String[] args) {
        int[] sizes = {10, 100, 1000, 5000, 10000, 20000, 50000};

        for (int size : sizes) {
            int[] orderedArray = generateOrderedArray(size);
            int[] reversedArray = generateReversedArray(size);
            int[] randomArray = generateRandomArray(size);

            long orderedTime = measureTime(orderedArray);

            long reversedTime = measureTime(reversedArray);

            long randomTime = measureTime(randomArray);

            System.out.println("Array size: " + size);
            System.out.println("Ordered array time: " + orderedTime + " milliseconds");
            System.out.println("Reversed array time: " + reversedTime + " milliseconds");
            System.out.println("Random array time: " + randomTime + " milliseconds\n\n");
        }
    }

    public static void sort(int[] arr) {
        int n = arr.length;
        int[] gaps = sedgewickSequence(n);

        for (int gap : gaps) {
            for (int i = gap; i < n; i++) {
                int temp = arr[i];
                int j;
                for (j = i; j >= gap && arr[j - gap] > temp; j -= gap) {
                    arr[j] = arr[j - gap];
                }
                arr[j] = temp;
            }
        }
    }

    public static long measureTime(int[] arr) {
        long startTime = System.currentTimeMillis();
        sort(arr);
        long endTime = System.currentTimeMillis();
        return endTime - startTime;
    }

    private static int[] generateOrderedArray(int size) {
        int[] arr = new int[size];
        for (int i = 0; i < size; i++) {
            arr[i] = i;
        }
        return arr;
    }

    private static int[] generateReversedArray(int size) {
        int[] arr = new int[size];
        for (int i = 0; i < size; i++) {
            arr[i] = size - i;
        }
        return arr;
    }

    private static int[] generateRandomArray(int size) {
        int[] arr = new int[size];
        for (int i = 0; i < size; i++) {
            arr[i] = (int) (Math.random() * size);
        }
        return arr;
    }

    private static int[] sedgewickSequence(int n) {
        int k = 0;

        while (!(9 * Math.pow(4, k) - 9 * Math.pow(2, k) + 1 >= n)) {
            k++;
        }

        int[] gaps = new int[k + 1];
        for (int i = 0; i <= k; i++) {
            gaps[k - i] = (int) (9 * Math.pow(4, i) - 9 * Math.pow(2, i) + 1);
        }

        return gaps;
    }
}
