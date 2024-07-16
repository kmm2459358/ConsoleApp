//======================================
//	ターン制バトル メイン
//======================================
#include "Character.h"
#include "TurnBattle.h"
#include "CommandUI.h"
#include "Utility.h"  // InitRand(),GetKey()
#include <stdio.h>  // printf()
// 関数プロトタイプ
void game();

int main()
{
	InitRand();

	int c;
	do {
		game();
		printf("もう一度(y/n)?");
		while (true) {
			c = GetKey();
			if (c == 'y' || c == 'n') {
				break;
			}
		}
	} while (c == 'y');

	return 0;
}

void game()
{
	Character player;
	Character boss;
	Character zako;
	Character tyuboss;
	TurnBattle btl;

	SetCharacter(&player,
		100,        // HP
		15,         // MP
		40,         // 攻撃力
		"ゆうしゃ", // 名前
		"");        // アスキーアート
	SetCharacter(&boss,
		255,      // HP
		6,        // MP
		50,       // 攻撃力
		"まおう", // 名前
		"　　Ａ＠Ａ\n" // アスキーアート
		"ψ（▼皿▼）ψ"
	);
	SetCharacter(&zako,
		3,          // HP
		3,          // MP
		2,          // 攻撃力
		"スライム", // 名前 
		"／・Д・＼\n" // アスキーアート
		"～～～～～"
	);
	SetCharacter(&tyuboss,
		120,          // HP
		6,          // MP
		25,          // 攻撃力
		"モナー", // 名前 
		"　　   ∧__∧\n" // アスキーアート
		"　　（ ´∀｀）\n"
		"　　 (   O┬O\n"
		" ≡◎-ヽJ ┴◎"
	);

	SetTurnBattle(&btl, &player, &tyuboss);
	StartTurnBattle(&btl);
	IntroTurnBattle(&btl);
	bool isEnd = false;
	Command cmd;
	while (true) {
		cmd = GetPlayerCommand(&btl);
		isEnd = ExecPlayerTurn(&btl, cmd);
		if (isEnd) {
			break;
		}
		cmd = GetEnemyCommand();
		isEnd = ExecEnemyTurn(&btl, cmd);
		if (isEnd) {
			break;
		}
		NextTurnBattle(&btl);
	}
}