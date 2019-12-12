~~~使用程式前請先閱讀~~~(1.程式執行流程 & 2.程式架構 & 功能設計 & compare weightedUnion and heightUnion )
/*P76081116鄭皓中*/

第二部分還有compare weightedUnion and heightUnion，不是只有流程哦!!!

1.程式執行流程 :(此程式是以c++所寫) 

   請照著助教公布的作業的input輸入(如下表示) :
3                  ---------> 請輸入有幾個set
3                  ---------> 請輸入第一個set的樹有幾個點
0 -2               ---------> 請輸入第一個set的樹的root & 樹高
1 0                ---------> 請輸入第一個set的樹的其他點 & 此點的parent
2 0                ---------> 請輸入第一個set的樹的其他點 & 此點的parent
3                  ---------> 請輸入第2個set的樹有幾個點
3 -3               ---------> 請輸入第2個set的樹的root & 樹高
4 3                ---------> 請輸入第2個set的樹的其他點 & 此點的parent
5 4                ---------> 請輸入第2個set的樹的其他點 & 此點的parent
1                  ---------> 請輸入第三個set的樹有幾個點
6 -1               ---------> 請輸入第三個set的樹的root & 樹高
UNION 0 3          ---------> 用heightUnion合併兩個set(樹)
FIND 5             ---------> collapsingFind找5的root(但是此作業是算出number of links traversed + number of links resetted)
FIND 1             ---------> collapsingFind找1的root(但是此作業是算出number of links traversed + number of links resetted)
FIND 1             ---------> collapsingFind找1的root(但是此作業是算出number of links traversed + number of links resetted)
STOP               ---------> 輸入STOP就停止輸入，接著以下就是把之前的所有結果一次顯示出來
------------------output---------------------
3                  ---------> FIND 5 的 number of links traversed + number of links resetted算出來是2+1=3
3                  ---------> FIND 1 的 number of links traversed + number of links resetted算出來是2+1=3
1                  ---------> FIND 1 之前執行過了，所以這裡的number of links traversed + number of links resetted是1+0=1

2.程式架構 & 功能設計 & compare weightedUnion and heightUnion : 
    此程式我設計了兩個function，一個是heightUnion，另一個是collapsingFind。heightUnion我是用比較樹高，樹高較高的當新的樹的root且樹高不變，若是兩棵樹等高，我是設計先輸入的root當新的樹的root，然後樹高加1。而collapsingFind這個function照理說是要找出root然後回傳root，但是此次作業是要計算 number of links traversed + number of links resetted，所以在此我設計用count計算number of links traversed + number of links resetted，每往前找尋一次parent，count就加1，直到找到root，然後把那個點的parent設成root，count也要加1。
    比較 weightedUnion and heightUnion ，對我來說我是覺得兩者以實作來說沒什麼差別，因為在輸入root時，heightUnion接著會輸入樹高，而weightedUnion則是輸入點的個數，所以你在Union時也只要比較root的所存的值就好。我覺得可能有差的是collapsingFind，但我也覺得差不多，假如說有兩棵樹要合併，第一棵樹有5個點，樹高是2，第二顆樹有三個點，樹高是3;以weightedUnion的方法:第一個樹的root會變成新的樹的root，樹高是4，以heightUnion方法:第二顆樹的root會變成新的是的root，樹高是3。所以你要找離root最遠的那個leaf時，heightUnion的方法可以往上少搜尋一次。以這個例子來說，heightUnion會相對好一點點。