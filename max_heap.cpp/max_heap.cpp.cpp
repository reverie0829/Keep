#include<iostream>
#include<cstdlib>
using namespace std;
/*P76081116鄭皓中*/


void max_heap(int *a, int m, int n) {
   int j, t;
   t = a[m];
   j = 2 * m;
   while (j <= n) {
      if (j < n && a[j+1] > a[j])
         j = j + 1;
      if (t > a[j])
         break;
      else if (t <= a[j]) {
         a[j / 2] = a[j];
         j = 2 * j;
      }
   }
   a[j/2] = t;
   return;
}
void build_maxheap(int *a,int n) {
   int k;
   for(k = n/2; k >= 1; k--) {
      max_heap(a,k,n);
   }
}
int main() {
   int k;
   int m, n, i;
   cin>>k;
   int output[k],o=1; 
   while(k!=0){
   cin>>m;
   cin>>n;
   //int a[m+1];(本來沒有宣告動態陣列)
	int *a;
	a = new int[m];
   for (i = 1; i <= m; i++) {
   	cin>>a[i];
   }
   build_maxheap(a,m);
   /* 印出 Max Heap(我自己先試看一下順序) 
   cout<<"Max Heap\n";
   for (i = 1; i <= m; i++) {
      cout<<a[i]<<endl;
   }*/
   
   int ans=0;
   while(n!=0){
   	ans=ans+a[1];
   	a[1]--;
    build_maxheap(a,m);
   	n--;
   }
   output[o++]=ans;
   k--;
   delete a;
	}
   cout<<"------------------output---------------------"<<endl;	
	for (i = 1; i < o; i++) {
   	cout<<output[i]<<"\n";
   }
   system("pause");
}
