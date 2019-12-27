#include <fstream>
#include <iostream>
#include <stdlib.h>
#include <time.h>
using namespace std;
//test data.txt

clock_t start, over;
double cpu_time_used;
int GetMedian(int *Array, int startIndex, int middle, int endIndex);
int Partition(int *Array, int startIndex, int endIndex);
void QuickSort_MedianOfThree(int *Array, int startIndex, int endIndex);

int main()
{
    FILE *fp;
    //char filename[100];
    int num[500000];
    int count = 0;

    //cout << "please enter the filename:";
    //gets(filename);
    fp = fopen("hw10.txt", "r");
    if (fp == NULL)
    {
        cout << "opening the file is error!";
        getchar();
        exit(0);
    }
    while (fscanf(fp, "%d", &num[count]) != EOF)
    {
        count++;
    }
    fclose(fp);
    /*
    cout<< "begin : " << "\n";
    for (int j = 0; j < count; j++)
    {
        cout<< num[j]<<",";
        }
    cout<<"\n";
    */
    start = clock();
    // Running QuickSort
    QuickSort_MedianOfThree(num, 0, count - 1);
    over = clock();

    cpu_time_used = ((double)(over - start)) / CLOCKS_PER_SEC;

    // Printing sorted array
    /*
	for (int counter = 0; counter < count; counter++)
	{
		cout << num[counter] << ", ";
	}*/

    ofstream file("MedianOfThree-Shorterfisrt.txt");
    for (int counter = 0; counter < count; counter++)
    {
        file << num[counter] << " ";
    }
    file.close();

    cout << "\n";
    cout << count << "\n";
    cout << "median of three [Shorterfisrt] execution time : " << cpu_time_used << "\n";
    cout << endl;
    system("pause");
}

int GetMedian(int *Array, int startIndex, int middle, int endIndex)
{
    if (Array[startIndex] <= Array[middle])
    {
        if (Array[middle] <= Array[endIndex])
            return middle;
        else if (Array[startIndex] <= Array[endIndex])
            return endIndex;
        else
            return startIndex;
    }
    else
    {
        if (Array[startIndex] <= Array[endIndex])
            return startIndex;
        else if (Array[middle] <= Array[endIndex])
            return endIndex;
        else
            return middle;
    }
}

int Partition(int *Array, int startIndex, int endIndex)
{
    int temp;                             // Temporary variable to use in exchanges
    int lowerRangeIndex = startIndex - 1; // The lowerRangeIndex variable hold the last index of lower-than-pivot range from startIndex to it

    // The pivotIndex will assign to the index of median of three element of part that sholud be sorted (the first, middle and last element)
    int pivotIndex = GetMedian(Array, startIndex, startIndex + ((endIndex - startIndex) / 2), endIndex);
    int pivotValue = Array[pivotIndex];

    // Exchanging the pivot and last element of Array (in range [startIndex ... endIndex]) for simplicity
    temp = Array[pivotIndex];
    Array[pivotIndex] = Array[endIndex];
    Array[endIndex] = temp;

    for (int tempIndex = startIndex; tempIndex < endIndex; tempIndex++)
    {
        if (Array[tempIndex] <= pivotValue)
        {
            // The next place of lowerRangeIndex is the correct place to hold the next lower-than-pivot element
            lowerRangeIndex = lowerRangeIndex + 1;

            //Exchanging Array[lowerRangeIndex] and an element that is lower than pivot
            temp = Array[lowerRangeIndex];
            Array[lowerRangeIndex] = Array[tempIndex];
            Array[tempIndex] = temp;
        }
    }

    // Putting the pivot to correct place
    if (lowerRangeIndex != endIndex)
    {
        // The next place of lowerRangeIndex is the correct place to hold the pivot
        lowerRangeIndex = lowerRangeIndex + 1;

        // Exchanging the pivot and current value in pivot's correct place
        temp = Array[lowerRangeIndex];
        Array[lowerRangeIndex] = Array[endIndex];
        Array[endIndex] = temp;
    }

    return lowerRangeIndex;
}

void QuickSort_MedianOfThree(int *Array, int startIndex, int endIndex)
{
    if (startIndex < endIndex)
    {
        // After running partition() an element called pivot get the correct place in array,
        // then we will partition the Array in two parts corresponding the pivot index
        int pivotIndex = Partition(Array, startIndex, endIndex);

        // Partitioning the Array to two parts, first before the pivot, second after the pivot
        if (pivotIndex - startIndex < endIndex - pivotIndex)
        {
            QuickSort_MedianOfThree(Array, startIndex, pivotIndex - 1);
            QuickSort_MedianOfThree(Array, pivotIndex + 1, endIndex);
        }
        else
        {
            QuickSort_MedianOfThree(Array, pivotIndex + 1, endIndex);
            QuickSort_MedianOfThree(Array, startIndex, pivotIndex - 1);
        }
    }
}
