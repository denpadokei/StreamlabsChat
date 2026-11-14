# StreamlabsChat

[![Build and Release](https://github.com/denpadokei/StreamlabsChat/actions/workflows/build-and-release.yml/badge.svg)](https://github.com/denpadokei/StreamlabsChat/actions/workflows/build-and-release.yml)

Streamlabs のチャットボックスへ流れたコメントを ChatCore 経由でゲーム内に取り込む MOD です。Streamlabs のチャットウィジェットから WebSocket でメッセージを受け取り、ChatCore のメッセージとして他の MOD に渡します。

## 機能

- メニューシーンの読み込み後に `ws://localhost:50055` で WebSocket サーバーを起動する
- Streamlabs のチャットボックスの Custom HTML/CSS から送られた JSON を受信する
- 受信した JSON を ChatCore の `IChatMessage` / `IChatChannel` / `IChatUser` に変換する
- ユーザー名、表示名、色、バッジ、モデレーター・配信者フラグをマッピングする
- 変換したメッセージを ChatCore のマルチプレクサへ流し、ChatCore 対応の他 MOD から利用できるようにする
- リリースビルドでは Twitch 由来のメッセージを除外し、YouTube などのプラットフォームを対象にする (DEBUG ビルドでは全プラットフォーム)
- エモートは未対応で、空の配列として扱う

## 動作環境

| 項目 | 内容 |
| --- | --- |
| Beat Saber | 1.14.0 |
| 依存 MOD | BSIPA ^4.1.0 / ChatCore ^2.0.0 |

## インストール

1. Beat Saber を 1.14.0 に合わせる (BSManager などのバージョン管理ツールを推奨)
2. BSIPA と上記の依存 MOD をインストールする
3. [Releases](https://github.com/denpadokei/StreamlabsChat/releases) から `StreamlabsChat-0.0.1-bs1.14.0.zip` をダウンロードし、`Plugins` フォルダをゲームフォルダへ上書きコピーする

## 使い方

1. [Streamlabs のチャットボックス](https://streamlabs.com/dashboard#/chatbox) を開き、チャットを表示するときに `Enable CustomHTML/CSS` の項目を有効にします。

   ![image](https://user-images.githubusercontent.com/55026301/115442439-fa91b780-a24c-11eb-8c17-3a6ccaacd523.png)

2. `JS` タブに WebSocket で JSON を送るコードを書きます。リポジトリの `StreamlabsChat/JSSample/chatjs.js` にサンプルがあるので、そのままコピーしても使えます。サンプルはチャットのイベントを受け取るたびに `ws://localhost:50055/` へ接続し、`obj.detail` を JSON 文字列にして送信します。

   ![image](https://user-images.githubusercontent.com/55026301/115442644-362c8180-a24d-11eb-85b1-3bcc918191c5.png)

3. OBS のブラウザソースとして追加するか、`Launch` ボタンを押してコメントボックスを表示します。
4. Beat Saber を起動します。WebSocket サーバーはメニューシーンの読み込み後に起動するため、ゲームより先にチャットボックスを開いていても、ゲーム起動後に送信されたメッセージから受信できます。

## ビルド

```powershell
git clone https://github.com/denpadokei/StreamlabsChat.git
cd StreamlabsChat
dotnet build .\StreamlabsChat\StreamlabsChat.csproj -c Release
```

`BeatSaberDir` (または `GameDirectory`) に Beat Saber のインストール先を設定すると、ゲームの参照アセンブリを解決できます。

GitHub Actions の `build-and-release.yml` が push ごとにビルドし、`X.X.X` 形式のタグを push するとリリースを作成します。

## ライセンス

MIT
