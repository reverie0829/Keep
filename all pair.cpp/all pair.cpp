#include <iostream>
#include <cstdlib>
using namespace std;
/*P76081116�G�q��*/
#define INFINITY 9999
#define max 10

//�ڪ� Dijkstra function �i�H���椣�P�I��_�l�I 
void Dijkstra(int G[max][max],int n,int startnode);
//�ҥH��o�X all pair �����G 
int main(){		   
	//cout <<"�п�J���X��node : "<<"\n";
	int node; cin>>node;
	int G[max][max];
	for(int i=0;i<node;i++)
      for(int j=0;j<node;j++)
      	G[i][j]=0;
	//cout <<"�п�J���X���� : "<<"\n";
	int edge; cin>>edge;
	for(int i=0;i<edge;i++){
		//cout <<"�п�J�䪺����I & weight : "<<"\n";
		int a,b,w; cin>>a>>b>>w;
		G[a][b]=w;
	}
	cout<<"------------------output---------------------"<<endl;
	//�Q�� Dijkstra function ��J���P�I��_�l�I�N��o�X all pair �����G 
	for(int i=0;i<node;i++){
		Dijkstra(G,node,i);
	}		
	cout<<"\n";
	system("pause");	
}


void Dijkstra(int G[max][max],int n,int startnode) {
   int cost[max][max],distance[max],pred[max];
   int visited[max],count,mindistance,nextnode,i,j;
   for(i=0;i<n;i++){
   		for(j=0;j<n;j++){
   			if(G[i][j]==0)
      			cost[i][j]=INFINITY;
   			else
      			cost[i][j]=G[i][j];
	   }
   }  
   for(i=0;i<n;i++) {
      distance[i]=cost[startnode][i];
      pred[i]=startnode;
      visited[i]=0;
   }
   distance[startnode]=0;
   visited[startnode]=1;
   count=1;
   while(count<n-1) {
      mindistance=INFINITY;
      for(i=0;i<n;i++){
      	if(distance[i]<mindistance&&!visited[i]) {
        mindistance=distance[i];
        nextnode=i;
      	}
	  }         
    visited[nextnode]=1;
    for(i=0;i<n;i++){
      	if(!visited[i]){
      		if(mindistance+cost[nextnode][i]<distance[i]) {
         		distance[i]=mindistance+cost[nextnode][i];
         		pred[i]=nextnode;
      		}
		}
	} 
    count++;
   }

	for(i=0;i<n;i++){
		int output[10];  //output�M�� 
   		int o=0;         //output�M�� 
   		if(i!=startnode){
   			//cout<<"\n�� "<<i<<" ���̵u���|�O : "<<distance[i];
      		if(distance[i]==9999){
				cout<<"\n";
      			cout<<"Impossible";      			
			} else{
				output[o++]=i;
      			j=i;
      			do {
         			j=pred[j];
         			output[o++]=j;
      			}while(j!=startnode);
      			cout<<"\n";
      			for(int k=o-1;k>0;k--){
      				cout<<output[k]<<",";
				}
				cout<<output[0]<<" ";   
   				cout<<distance[i];
			}
		} 
	} 
}
