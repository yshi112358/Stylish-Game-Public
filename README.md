# Re:Imagigate \<Demo\>
<p>"Re:Imagigate"というゲームです。</p>
<p>このリポジトリは著作権の含むアセットなどを含まないようにするため、一部ファイルを削除しております。</p>
<p>そのためUnityで開くことはできませんがソースコードは閲覧できるようになっております。</p>

ゲームはこちらからインストールすることができます(Windows, Mac)→
https://github.com/yshi112358/Stylish-Game-Public/releases

# ゲーム説明
<p>
 <img src="https://github.com/user-attachments/assets/117acdd2-dd2a-44c0-947b-889bdf90b764" height=200px>
 <img src="https://github.com/user-attachments/assets/5f587f29-e935-4242-9720-16733c787d40" height=200px>
</p>
<p>広大なフォールドでモンスターと戦うスタイリッシュアクションゲームです。</p>
<p>プレイヤーは剣を振り回しダメージを与える、モンスターの攻撃を避けることができます。</p>
<p>モンスターの体力を0にする、プレイヤーの体力が0になるのどちらかでゲームが終了します。</p>

## 動かし方
| 役割 | キーボード＆マウス | DualShockコントローラー |
| --- | --- | --- |
| 決定 | ENTERもしくは左クリック | ×ボタン |
| 移動 | WASD | 左スティック |
| 軽攻撃 | 左クリック | △ボタン |
| 重攻撃 | 右クリック | □ボタン |
| 回避 | E | ○ボタン |
| ジャンプ | SPACE | ×ボタン |
| ダッシュ | Shift長押し | R長押し |
| ロックオン | Q長押し | L長押し |
| 武器切り替え | １２３４ | 十字キー |

# 工夫点
## クラス設計
<p>4人での開発だったため、クラス設計をし全員で開発しやすい環境を整えました。</p>

![](https://github.com/user-attachments/assets/9339cf14-7e81-4a7b-99a3-5403a2e3c96a)

## オブジェクトパターンの利用
<p>UniRxを用いたObserverパターンで設計しました。監視する側とされる側の2要素で成るため、非常に設計しやすくオブジェクト同時の関係をよりわかりやすくすることができました。</p>



<p>This repository contains the source code of our game "Re:ImagiGate". This doesn't contains copyrighted materials (e.g. 3D Model, Animation).</p>
