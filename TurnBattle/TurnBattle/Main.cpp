//======================================
//	�^�[�����o�g�� ���C��
//======================================
#include "Character.h"
#include "TurnBattle.h"
#include "CommandUI.h"
#include "Utility.h"  // InitRand(),GetKey()
#include <stdio.h>  // printf()
// �֐��v���g�^�C�v
void game();

int main()
{
	InitRand();

	int c;
	do {
		game();
		printf("������x(y/n)?");
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
		40,         // �U����
		"�䂤����", // ���O
		"");        // �A�X�L�[�A�[�g
	SetCharacter(&boss,
		255,      // HP
		6,        // MP
		50,       // �U����
		"�܂���", // ���O
		"�@�@�`���`\n" // �A�X�L�[�A�[�g
		"�Ձi���M���j��"
	);
	SetCharacter(&zako,
		3,          // HP
		3,          // MP
		2,          // �U����
		"�X���C��", // ���O 
		"�^�E�D�E�_\n" // �A�X�L�[�A�[�g
		"�`�`�`�`�`"
	);
	SetCharacter(&tyuboss,
		120,          // HP
		6,          // MP
		25,          // �U����
		"���i�[", // ���O 
		"�@�@   ��__��\n" // �A�X�L�[�A�[�g
		"�@�@�i �L�́M�j\n"
		"�@�@ (   O��O\n"
		" �߁�-�RJ ����"
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