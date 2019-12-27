~~~使用程式前請先閱讀~~~(1.評測結果 & 2.我的想法)
/*P76081116鄭皓中*/

1.評測結果 :(此次評測我有用 dev c++ 跟 vs code 兩種環境都跑跑看，"截圖"都在各自的資料夾之中) 

               -----在這裡 我直接把結果的時間表示出來-----
		
   (dev c++跑的結果)	longerfirst	shorterfirst	original

	leftmost	  0.031             0.022         0.032
	
   median of three        0.123             0.109         0.125
--------------------------------------------------------------------------------
   (vs code跑的結果)	longerfirst	shorterfirst	original

	leftmost	  0.024             0.016         0.031
	
   median of three        0.094             0.093         0.098
--------------------------------------------------------------------------------
p.s. 已將compile裡的 tail recursion optimization 開啟

2.我的想法 : 
    此次測資是45萬筆亂數(助教給的"hw10"測資，我自行複製三次:15萬*3次，剛好45萬筆)，接著分析評測結果。

(1)首先先看為何兩個環境都是leftmost比median of three快，我的想法是可能有兩種原因，第一種原因是這次的測資本身可能對leftmost有利，
   所以可以跑得比較快。而第二種可能的原因是，可能是因為測資不夠大(或者不是worst case)，而median of three本身有一步是找出 left 
   middle right 的中位數當pivot，然後再去交換，多了這個步驟會比leftmost方法多花一點點的時間成本，而median of three省下來
   的時間無法與之相抵，所以才造成這種情況。

(2)接著分析leftmost內各自的三種方法，兩個環境都是shorterfirst最快，我的想法是覺得跟tail recursion有關，因為若是短的先跑，長(Long)的
   就會在尾位置，如此一來tail recursion optimization，長的所減少的時間成本會比較多，所以相較於longerfirst的方法，shorterfirst會比較快。
   然而兩個環境的original都是最慢的，但是dev c++的環境下 original 與 longerfirst其實沒差很多，我的想法是original的尾位置其實隨機的，
   有時候可能是長的在尾位置，有時候是短的，因為程式碼是固定的，依我的想法，我覺得longerfirst應該是worst case，因為每次尾位置都是短的，
   相比於original的隨機長短，可能顯得更慢。所以我覺得original應該時間會在longerfirst 與 shorterfirst 之間，造成original最長的原因可能
   是有一點小bug吧，助教請別太在意XD(若是這個想法是錯的話，我的另一個觀點在下面median of three最後。)

(3)接著分析median of three內各自的三種方法，其實這跟leftmost所發現的理由其實是差不多的。所以就偷懶複製了一下，兩個環境都是shorterfirst最快，
   我的想法是覺得跟tail recursion有關，因為若是短的先跑，長(Long)的就會在尾位置，如此一來tail recursion optimization，
   長的所減少的時間成本會比較多，所以相較於longerfirst的方法，shorterfirst會比較快。與leftmost一樣，original的方法都比其他兩者都慢，若是我
   leftmost時所闡述的想法不對的話，那應該是因為longerfirst與shorterfirst都有做tail recursion optimization，而original就是一般的情況，
   沒有優化過，所以可能會因此慢了一些些。