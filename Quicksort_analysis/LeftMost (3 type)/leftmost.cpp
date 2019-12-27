#include <fstream>
#include <iostream>
#include <stdlib.h>
#include <time.h>
using namespace std;
//test data.txt

clock_t start, over;
double cpu_time_used;
void swap(int&a, int&b);
void quickSort(int arr[], int left, int right);

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
	quickSort(num, 0, count - 1);
	over = clock();

	cpu_time_used = ((double)(over - start))/CLOCKS_PER_SEC ;

	// Printing sorted array
	/*
	for (int counter = 0; counter < count; counter++)
	{
		cout << num[counter] << ",";
	}*/

	ofstream file("leftmost(original).txt");
	for (int counter = 0; counter < count; counter++)
	{
		file << num[counter] << " ";
	}
	file.close();

	cout << "\n";
	cout << count << "\n";
	cout << "leftmost [original] execution time : " << cpu_time_used << "\n";
	cout << endl;
	system("pause");
	return 0;
}

void quickSort(int arr[], int left, int right)
{
	if (left < right)
	{
		int pivot = arr[left];//假設pivot在第一個的位置
		int l = left;
		int r = right + 1;
		
		while (1)
		{
			while (l < right && arr[++l] < pivot);//向右找小於pivot的數值的位置
			while (r > 0 && arr[--r] > pivot);//向左找大於pivot的數值的位置

			if (l >= r)//範圍內pivot右邊沒有比pivot小的數,反之亦然
			{
				break;
			}
		
			swap(arr[l], arr[r]);//比pivot大的數移到右邊，比pivot小的數換到左邊
		}

		swap(arr[left], arr[r]);//將pivot移到中間

		quickSort(arr, left, r - 1);//左子數列做遞迴
		quickSort(arr, r + 1, right);//右子數列做遞迴
	}
	
}

void swap(int&a, int&b)
{
	int temp;
	temp = a;
	a = b;
	b = temp;
}
