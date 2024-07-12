//======================================
//	コマンドUI
//======================================
// ★ここにインクルードガード(開始)を記入してください。
#ifndef _COMMANDUI_H
#define _COMMANDUI_H

#include "Command.h"
#include "TurnBattle.h"

// プレーヤのコマンド取得
Command GetPlayerCommand(TurnBattle* btl);
// 敵のコマンド取得
Command GetEnemyCommand();

// ★ここにインクルードガード(終了)を記入してください。
#endif /* _COMMANDUI_H */