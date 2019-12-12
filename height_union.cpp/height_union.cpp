#include <iostream>
#include <cstdlib>
#include <string>
using namespace std;
/*P76081116鄭皓中*/

void heightUnion(int *point,int i,int j){
		if(-point[i] > -point[j]){
			point[j]=i;
		}
		else if(-point[i] < -point[j]){
			point[i]=j;				
		}
		else if(-point[i] == -point[j]){
			point[j]=i;
			point[i]=point[i]-1;
		}
	}

int collapsingFind(int *point,int i){
	int root;
	int count=0; 
    for (root = i; point[root] >= 0; root = point[root]){
    	count++;
	}
	if(point[i]!=root){
		point[i]=root;
		count++;
	}
	//return root;   本來要回傳root用，但是此次作業是要計算 number of links traversed + number of links resetted，所以在此我註解掉  
    return count;
}

int main()
{
	int point[100];
	//cout<<"請輸入有幾個set : "<<"\n";
	int s; cin>>s;
	for(int i=1;i<=s;i++){
	//cout<<"請輸入有幾個點 : "<<"\n";
	int p; cin>>p;
	//cout<<"請輸入root & 樹高 : "<<"\n";
	int root; cin>>root;
	int h; cin>>h;
	point[root]=h; 
	//cout<<"樹高是"<< -h <<"\n";
	for(int i=1;i<p;i++){
		//cout<<"請輸入第"<<i+1<<"個點 & 此點的父點 : "<<"\n";
		int node; cin>>node;
		int parent; cin>>parent;
		point[node]=parent;
	}
}
	//for(int i=0;i<=6;i++){cout<<point[i]<<" ";}  我自己先cout出來看一下 
	string str;	
	int output[100];  //準備一次把結果output專用 
	int o=0;          //準備一次把結果output專用  
	while(cin>>str&&!cin.eof()){
		if(str == "UNION"){
			int y; cin>>y;
			int z; cin>>z;
			heightUnion(point,y,z);			
			/*for(int i=0;i<=6;i++){               我自己先cout出來看一下  
				cout<<point[i]<<" ";
			}*/			
		} 
		else if(str == "FIND"){
			int k; cin>>k;			
			output[o++]=collapsingFind(point,k);
			//cout<<collapsingFind(point,k)<<"\n";
		}
		else if(str == "STOP"){
			break;
		}
	} 
cout<<"------------------output---------------------"<<endl;
	for(int i=0;i<o;i++){
		cout<<output[i]<<"\n";
	}
	
  system("pause");
}

