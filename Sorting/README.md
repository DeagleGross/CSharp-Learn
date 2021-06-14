# Sorting Algorithms
Below is description of algorithms. You can find implementations in [Mechanisms](./Mechanisms) folder


## BubbleSort
---
Works due to swapping adjacent elements.  
( **5 1** 4 2 8 ) –> ( **1 5** 4 2 8 ) Here, algorithm compares the first two elements, and swaps since 5 > 1. 

Complexity:
- Time  - O(n^2)
- Space - O(1) 

```
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n - i - 1; j++)    
            if (arr[j] > arr[j + 1]) -> swap [j]-th and [j+1]-th
```

## MergeSort
---
Basic principle - divide and conquer.  
Firstly, we need to divide array into two halves, then recursively every half in another half and go on till we get 2-element arrays:

Complexity:
- Time  - O(n log(n))
- Space - O(n) 

```
    [1, 234, 3, ..... , 97, 85]
                /\
               ....
               /  \
    [1, 234] [3, ..] .... [97, 85]
```

Then we start to merge them together back recursively:
`[1, 200] [45, 3] -> [1, 3, 45, 200]`

## Quicksort
---
Another `divide and conquer` approach.

- Pick element as pivot
- place all smaller elements to left, higher to right of pivot element.  
  pivot is placed in right location of array.
- launch same steps for left part of array and for right one

Complexity:
- Time  - O(n log(n))
- Space - O(1) 

## HeapSort
---
Heap can be represented in array:
- left child of `i` is `2 * i + 1`
- right child of `i` is `2 * i + 2`

Complexity:
- Time  - O(n log(n))
- Space - O(1) 

Sort works as follows:
```
    // Build heap
    for (int i = n / 2 - 1; i >= 0; i--)
        heapify(arr, n, i);

    // extract root as biggest element from heap
    for (int i = n - 1; i > 0; i--) {
        // Move current root to end
        swap(arr[0], arr[i]);
 
        // call heapify on the reduced heap
        heapify(arr, i, 0);
    }
```

## CountingSort
---
Counting numbers of objects having distinct values and then doing arithmetic to calculate position of each object.

In example:
- data in range `[0:9]`
- input: `1, 4, 1, 2, 7, 5, 2`

Complexity:
- Time  - O(n + k) `k is range of input`
- Space - O(n + k) 

1) Make a count array of distinct values
```
Index:     0  1  2  3  4  5  6  7  8  9
Count:     0  2  2  0   1  1  0  1  0  0
```
2) Modify the count array so that element at each index stores the sum of previous counts
```
Index:     0  1  2  3  4  5  6  7  8  9
Count:     0  2  4  4  5  6  6  7  7  7
```

3) build result array
```
    for (i = 0; arr[i]; ++i) {
        output[count[arr[i]] - 1] = arr[i];
        --count[arr[i]];
    }
```

## RadixSort
---
Sorts by digits from lowest rank to biggest rank

Complexity:
- Time  - O(w * n) `w is the key length`
- Space - O(w + n) 

```
    Original, unsorted list:
    170, 45, 75, 90, 802, 24, 2, 66

    Sorting by least significant digit (1s place) gives: 
    [*Notice that we keep 802 before 2, because 802 occurred 
    before 2 in the original list, and similarly for pairs 
    170 & 90 and 45 & 75.]

    170, 90, 802, 2, 24, 45, 75, 66

    Sorting by next digit (10s place) gives: 
    [*Notice that 802 again comes before 2 as 802 comes before 
    2 in the previous list.]

    802, 2, 24, 45, 66, 170, 75, 90

    Sorting by the most significant digit (100s place) gives:
    2, 24, 45, 66, 75, 90, 170, 802
```

