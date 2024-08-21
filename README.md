# SlidoCommentSpeaker

Slidoのコメントを読み取りずんだもんにしゃべらせるソフトです。

## 必要な物
 - 最新版のWindows 10

## 使用方法
 1. [リリース](https://github.com/N-Magi/SlidoCommentSpeaker/releases)から最新版のバイナリをダウンロードします。
 2. ずんだもんを起動します
 3. 解凍し内部のexeファイルを実行します。
 4. ブラウザを使用した設定を行います

 ## ブラウザを使用した設定
 ![](https://github.com/N-Magi/SlidoCommentSpeaker/blob/master/screenshot.png?raw=true)
 1. Slidoを開きf12を押して開発者モードを起動します
 2. ネットワークモニタ画面に切り替えて、ページをリロードします
 3. WSフィルタを有効にして通信されるファイルを監視します。
 4. 101 GETで一点だけ送受信される通信が見つかるのでこのファイルのURLをコピーします
 5. このURLをソフト起動時の最初に要求されるuriに入力します。
 6. その後応答タブから420 subscribeが送信されるのでこの内容をコピーしてメモ帳に貼り付けます
 7. AccessToken/Targetをそれぞれコピーしてソフトが要求した順番で貼り付けます
 8. 成功すると読み込めます
 
 
 
