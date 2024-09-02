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
 2. **(読み上げ利用する場合)** Voicevoxを起動します
 3. バイナリ内部のexeファイルを起動します。
 4. 下のテキストボックスにSlidoURLを指定して Connectボタンを押下します。
 5. 接続後、各セクションごとの名前が表示されます
 6. **(読み上げを利用する場合)** **voicevoxを起動していることを確認して**　Voicevoxチェックマークをチェックします　接続した旨しゃべります
### SlidoCommentSpeaker
 1. [リリース](https://github.com/N-Magi/SlidoCommentSpeaker/releases)から最新版のバイナリをダウンロードします。
 2. ずんだもんを起動します
 3. 解凍し内部のexeファイルを実行します。
 4. SlidoURLを入力します。　接続されると動きます
 
 
