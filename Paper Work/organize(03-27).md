# Organize
I.<br>*`Delay Analysis and Optimization in Cache-enabled`*  :<br>
 
  &emsp;system model 跟學長那篇有點像，但是average delay的公式是利用 queueing theory的M/M/1推的， 接著要去最佳化公式，也是跟學長一樣先去證明是NP-complete，然後再去找驗算法。<br>
  &emsp;作者找演算法的流程是將優化問題表示為受擬陣(matroid)約束的非遞減子模函數的最大化，從而使我們能夠採用具有1/2近似性能保證的低複雜度貪婪緩存算法。然後提出了一種啟發式貪婪演算法(Heuristic greedy algorithm)，該方案不僅考慮內容的受歡迎程度，存儲容量和用戶請求到達率，還考慮內容的大小。------>但是這段證明我實在看不懂，需要蠻多先備知識的。

  <img src="./picture/1.png" width="40%"/>

II.<br>*`Content-Exchanged Based Cooperative Caching in 5G Wireless Networks`* :<br>
  <br>&emsp;( 03/16 meeting 內容 )

III.<br>*`Cooperative Edge Caching in User-Centric Clustered Mobile Networks`* :<br> 

  &emsp;學長論文內所比較的 Zhang's scheme <br>
  其實我有點好奇學長的方法為何會比較快，因為這篇論文也是有用到cluster的想法，而且還不需要用到backhaul傳遞。
<p class="half">
    <img src="./picture/5.png" width="31.5%"/><img src="./picture/6.png"width="45%"/>   
</p>

IV.<br>1 ) *`Cooperative Caching and Transmission Design in Cluster-Centric Small Cell Networks`* :<br>
2 ) *`Cluster-centric cache utilization design in cooperative small cell networks`* :<br>


&emsp;( 用來了解 Cluster-Centric  與 User-Centric的差異 : 作者用 Cluster-Centric 是為了分析方便)<br> 

&emsp;為了平衡傳輸和內容多樣性，作者提出了一種以緩存為中心的策略，結合緩存和傳輸策略的設計。cluster中的整個緩存空間由中央控制器安排，以便在每個SBS中分發相同的最流行內容，或在不同的SBS中存儲較不受歡迎的內容的不同分區(MPC & LCD)，從而確保可以在cluster內部找到所有緩存的內容。<br>
&emsp;根據所請求內容的可用性和位置，具有聯合傳輸或併行傳輸的協作多點技術(CoMP)用於將內容交付給服務的用戶。

<p class="half">
    <img src="./picture/4.png" width="41.8%"/><img src="./picture/3.png"width="40%"/>   
</p>
<!-- V.<br>*``* :<br>   -->

# Reference
  [1]&emsp;Y. Sun, Z. Chen and H. Liu, "Delay Analysis and Optimization in Cache-Enabled Multi-Cell Cooperative Networks," 2016 IEEE Global Communications Conference (GLOBECOM), Washington, DC, 2016, pp. 1-7.

  [2]&emsp;Z. Chen, J. Lee, T. Q. S. Quek and M. Kountouris, "Cooperative Caching and Transmission Design in Cluster-Centric Small Cell Networks," in IEEE Transactions on Wireless Communications, vol. 16, no. 5, pp. 3401-3415, May 2017.