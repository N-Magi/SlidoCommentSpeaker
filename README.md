# SlidoCommentSpeaker

Slidoのコメントを読み取りずんだもんにしゃべらせるソフトです。

## 各ソリューションの役回り

- SlidoCommentSpeaker

  CLIアプリケーションです。
  Slidoライブラリおよび、VoicevoxApiを触るためのライブラリを簡単に使用できるようにしています
  

- SlidoCommentSpeakerGUI
  
  GUIアプリケーションです。
  app.sli.do/... 以下のURL(接続URL)を指定することでQ/Aを取得することができます
  
  すでに投稿済みのQ/Aに関しては読み込まれません。
  コメントの出力機能もありません。

## 必要な物
 - 最新版のWindows 10
   
## 使用方法

### SlidoCommentSpeakerGUI
 1. [リリース](https://github.com/N-Magi/SlidoCommentSpeaker/releases)から最新版のバイナリをダウンロードして解凍します。
 2. 同じく[リリース](https://github.com/N-Magi/SlidoCommentSpeaker/releases)から公開されている `VoicevoxPlugin`をダウンロードします。
 3. **(読み上げ利用する場合)** Voicevoxを起動します
 4. バイナリ内部のexeファイルを起動します。
 5. 一度起動すると`実行ファイル`と同じディレクトリに`plugins`フォルダが作成されます
 6. 一度ソフトを終了して`plugins`フォルダに　`VoicevoxPlugin`内部の`VoicevoxPlugin.dll`をコピーします
 7. ソフトを起動します。　左斜め上のプラグインフォルダを押すと`Voicevox読み上げ`が追加されており、クリックすることでvoicevox読み上げを有効にできます
 8. 下のテキストボックスにSlidoURLを指定して Connectボタンを押下します。
 9. 接続後、各セクションごとの名前が表示されます
 10. **(読み上げを利用する場合)** **voicevoxを起動していることを確認して**　Voicevoxチェックマークをチェックします　接続した旨しゃべります
### SlidoCommentSpeaker
 1. [リリース](https://github.com/N-Magi/SlidoCommentSpeaker/releases)から最新版のバイナリをダウンロードします。
 2. ずんだもんを起動します
 3. 解凍し内部のexeファイルを実行します。
 4. SlidoURLを入力します。　接続されると動きます
 
 
