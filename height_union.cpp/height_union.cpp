#include <iostream>
#include <cstdlib>
#include <string>
using namespace std;
/*P76081116�G�q��*/

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
	//return root;   ���ӭn�^��root�ΡA���O�����@�~�O�n�p�� number of links traversed + number of links resetted�A�ҥH�b���ڵ��ѱ�  
    return count;
}

int main()
{
	int point[100];
	//cout<<"�п�J���X��set : "<<"\n";
	int s; cin>>s;
	for(int i=1;i<=s;i++){
	//cout<<"�п�J���X���I : "<<"\n";
	int p; cin>>p;
	//cout<<"�п�Jroot & �� : "<<"\n";
	int root; cin>>root;
	int h; cin>>h;
	point[root]=h; 
	//cout<<"�𰪬O"<< -h <<"\n";
	for(int i=1;i<p;i++){
		//cout<<"�п�J��"<<i+1<<"���I & ���I�����I : "<<"\n";
		int node; cin>>node;
		int parent; cin>>parent;
		point[node]=parent;
	}
}
	//for(int i=0;i<=6;i++){cout<<point[i]<<" ";}  �ڦۤv��cout�X�Ӭݤ@�U 
	string str;	
	int output[100];  //�ǳƤ@���⵲�Goutput�M�� 
	int o=0;          //�ǳƤ@���⵲�Goutput�M��  
	while(cin>>str&&!cin.eof()){
		if(str == "UNION"){
			int y; cin>>y;
			int z; cin>>z;
			heightUnion(point,y,z);			
			/*for(int i=0;i<=6;i++){               �ڦۤv��cout�X�Ӭݤ@�U  
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

