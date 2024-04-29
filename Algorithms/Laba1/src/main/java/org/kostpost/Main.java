package org.kostpost;

public class Main {

    public static void main(String[] args) {
        int[] arr = {5, 2, 8, 1, 9, 3, 7, 4, 6};
        sort(arr);
        for (int num : arr) {
            System.out.print(num + " ");
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