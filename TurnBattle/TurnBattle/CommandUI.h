//======================================
//	�R�}���hUI
//======================================
// �������ɃC���N���[�h�K�[�h(�J�n)���L�����Ă��������B
#ifndef _COMMANDUI_H
#define _COMMANDUI_H

#include "Command.h"
#include "TurnBattle.h"

// �v���[���̃R�}���h�擾
Command GetPlayerCommand(TurnBattle* btl);
// �G�̃R�}���h�擾
Command GetEnemyCommand();

// �������ɃC���N���[�h�K�[�h(�I��)���L�����Ă��������B
#endif /* _COMMANDUI_H */