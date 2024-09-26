//======================================
//	�}�C���X�B�[�p�[ ���C��
//======================================
#include "Stage.h"
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
	Stage stage[1];

	InitializeStage(stage);
	bool isEnd = false;
	while (isEnd == false) {
		DrawScreen(stage);
		Key c = GetKey();
		switch (c) {
		case ARROW_UP:
			MoveCursor(stage, 0, -1);
			break;
		case ARROW_DOWN:
			MoveCursor(stage, 0, 1);
			break;
		case ARROW_LEFT:
			MoveCursor(stage, -1, 0);
			break;
		case ARROW_RIGHT:
			MoveCursor(stage, 1, 0);
			break;
		case 'f':  // �t���Oon/off
			FlipCursorFlag(stage);
			break;
		case SPACE:  // �J��
			isEnd = OpenCursorCell(stage);
			break;
		}
	}
	if (IsLose(stage)) {
		DrawScreen(stage);
		printf("\n�a�n�l�I�I�@�x�n�t�@�k�n�r�d�D\n");
	}
	else {
		DrawScreen(stage);
		printf("\n�b�k�d�`�q�I�@�x�n�t�@�v�h�m�D\n");
	}
}