#include <iostream>
#include <sstream>
#include <string>
#include <vector>
using namespace std;
/*P76081116鄭皓中*/

typedef struct Node *NodePointer;
struct Node
{
  int head;         //record the key value of this vertex
  int distance;     //record the cost of the activity from header to this node struct
  NodePointer Next; //points toward next head of the header
};

typedef struct Activity *ActivityPointer;
struct Activity
{
  int index;
  int left;
  int right;
  ActivityPointer NextActivity;
};

typedef struct Header *HeaderPointer;
struct Header
{
  int tail;         //record the key value of this vertex
  int count;        //record how many left it has
  NodePointer Next; //points toward it's right
};

int Edge, Event;
ActivityPointer tempActivity;
ActivityPointer initialActivity;
HeaderPointer tempHeader;
NodePointer tempNode;
vector<int> tempSave;
vector<int> Head;
vector<HeaderPointer> HeaderAvailable;

void StringSplit(string);
int CheckHeader(int);
void CheckActivity(int);
void UpdateCount();
void TopologicalOrder();

int main()
{
  //cout<<"Please enter the number of activity"<<endl;
  cin >> Edge;
  cin.ignore();
  tempActivity = new Activity;
  tempActivity->NextActivity = NULL;
  initialActivity = tempActivity;
  for (int i = 0; i < Edge; i++)
  {
    //cout<<"Please enter activity index & the information of activities "<<endl;
    string input;
    getline(cin, input);
    StringSplit(input);
    //Initialising activity
    tempActivity = new Activity;
    tempActivity->index = tempSave.at(0);
    tempActivity->left = tempSave.at(1);
    tempActivity->right = tempSave.at(2);
    tempActivity->NextActivity = initialActivity->NextActivity;
    initialActivity->NextActivity = tempActivity;
    //End initialising activity

    //intialising Header and Node
    unsigned int index = CheckHeader(tempSave.at(1));
    if (index == HeaderAvailable.size()) //If the correcponding header does not exist , it will create a new header for this vertex.
    {
      tempHeader = new Header;
      tempHeader->tail = tempSave.at(1);
      tempHeader->Next = NULL;
      tempHeader->count = 0;
      HeaderAvailable.push_back(tempHeader);
    }
    //If it exist , then it will update the header by adding a new node to it.
    //Node struct will be created and Next from coresponding Header will points toward this struct.
    tempNode = new Node;
    tempNode->head = tempSave.at(2);
    tempNode->distance = tempSave.at(3);
    tempNode->Next = HeaderAvailable.at(index)->Next;
    HeaderAvailable.at(index)->Next = tempNode;
    Head.push_back(tempSave.at(2));

    index = CheckHeader(tempSave.at(2));
    if (index == HeaderAvailable.size())
    {
      tempHeader = new Header;
      tempHeader->tail = tempSave.at(2);
      tempHeader->Next = NULL;
      tempHeader->count = 0;
      HeaderAvailable.push_back(tempHeader);
    }
  }
  //End intialising header and node

  UpdateCount();
  Event = HeaderAvailable.size();
  int ee[Event];
  int le[Event];
  for (int i = 0; i < Event; i++)
  {
    ee[i] = 0;
    le[i] = 9999;
  }
  TopologicalOrder();
  for (unsigned int i = 0; i < Head.size(); i++)
  {
    int Current_tail = Head.at(i);
    int index = CheckHeader(Current_tail);
    tempNode = HeaderAvailable.at(index)->Next;
    while (tempNode)
    {
      if (tempNode->distance + ee[Current_tail] > ee[tempNode->head])
      {
        ee[tempNode->head] = tempNode->distance + ee[Current_tail];
      }
      tempNode = tempNode->Next;
    }
  }
  le[Head.at(Head.size() - 1)] = ee[Head.at(Head.size() - 1)];

  for (int i = Head.size() - 2; i >= 0; i--)
  {
    int Current_tail = Head.at(i);
    int index = CheckHeader(Current_tail);
    tempNode = HeaderAvailable.at(index)->Next;
    while (tempNode)
    {
      if (le[tempNode->head] - tempNode->distance < le[Current_tail])
      {
        le[Current_tail] = le[tempNode->head] - tempNode->distance;
      }
      tempNode = tempNode->Next;
    }
  }
  cout << "\n";
  cout << "------------------output------------------\n";
  cout << "event\t"
       << "ee\t"
       << "le\t\n";
  for (int i = 0; i < Event; i++)
  {
    cout << i << "\t" << ee[i] << "\t" << le[i] << "\t\n";
  }
  cout << "\n";
  int output[100], output_number = 0;
  int e[Edge], l[Edge], slack[Edge];
  cout << "act.\t"
       << "e\t"
       << "l\t"
       << "slack\t"
       << "critical\n";
  for (int i = 0; i < Edge; i++)
  {
    CheckActivity(i + 1);
    e[i] = ee[tempActivity->left];
    int index = CheckHeader(tempActivity->left);
    tempNode = HeaderAvailable.at(index)->Next;
    while (tempNode->head != tempActivity->right)
    {
      tempNode = tempNode->Next;
    }
    l[i] = le[tempActivity->right] - tempNode->distance;
    slack[i] = l[i] - e[i];
    cout << i + 1 << "\t" << e[i] << "\t" << l[i] << "\t" << slack[i] << "\t";
    if (slack[i] == 0)
    {
      cout << "Yes" << endl;
      output[output_number++] = i + 1;
    }
    else
    {
      cout << "No" << endl;
    }
  }
  cout << "\n";
  for (int i = 0; i < output_number; i++)
  {
    cout << output[i] << " ";
  }
  cout << "\n";
  system("pause");
}

//TODO:(functions)
void StringSplit(string input)
{
  tempSave.erase(tempSave.begin(), tempSave.end());
  istringstream split(input);
  string temp;
  while (split >> temp)
  {
    tempSave.push_back(stoi(temp));
  }
}

int CheckHeader(int key)
{
  unsigned int length = HeaderAvailable.size();
  for (unsigned int i = 0; i < length; i++)
  {
    if (HeaderAvailable.at(i)->tail == key)
    {
      return i;
    }
  }
  return length;
}

void UpdateCount()
{
  unsigned int length = HeaderAvailable.size();
  for (unsigned int i = 0; i < length; i++)
  {
    int count = 0;
    for (unsigned int j = 0; j < Head.size(); j++)
    {
      if (Head.at(j) == HeaderAvailable.at(i)->tail)
      {
        count++;
      }
    }
    HeaderAvailable.at(i)->count = count;
  }
}

void TopologicalOrder()
{
  Head.erase(Head.begin(), Head.end()); //Head is useless after determining count, so reuse it.
  int top = -1;
  unsigned int length = HeaderAvailable.size();
  for (unsigned int i = 0; i < length; i++)
  {
    if (HeaderAvailable.at(i)->count == 0)
    {
      HeaderAvailable.at(i)->count = top;
      top = i;
    }
  }
  for (unsigned int i = 0; i < length; i++)
  {
    int j = HeaderAvailable.at(top)->tail;
    Head.push_back(j);
    tempNode = HeaderAvailable.at(top)->Next;
    top = HeaderAvailable.at(top)->count;
    while (tempNode)
    {
      int index = (CheckHeader(tempNode->head));
      HeaderAvailable.at(index)->count--;
      if (HeaderAvailable.at(index)->count == 0)
      {
        HeaderAvailable.at(index)->count = top;
        top = index;
      }
      tempNode = tempNode->Next;
    }
  }
}

void CheckActivity(int index)
{
  tempActivity = initialActivity;
  while (tempActivity)
  {
    if (tempActivity->index == index)
    {
      return;
    }
    tempActivity = tempActivity->NextActivity;
  }
}
