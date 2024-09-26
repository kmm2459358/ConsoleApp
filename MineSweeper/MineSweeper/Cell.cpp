//======================================
//	�}�C���X�B�[�p�[ �Z��
//======================================
#include "Cell.h"
// �Z�b�g�t�b�v(������)��Ԃɂ���
void SetupCell(Cell* cell)
{
	cell->bomb = false;
	cell->hide = true;
	cell->flag = false;
	cell->adjacentBombs = 0;
}
// bomb��?
bool IsBomb(Cell* cell)
{
	return cell->bomb;
}
// bomb ���Z�b�g
void SetBomb(Cell* cell, bool value)
{
	cell->bomb = value;
}
// hide��?
bool IsHide(Cell* cell)
{
	return cell->hide;
}
// hide ���Z�b�g
void SetHide(Cell* cell, bool value)
{
	cell->hide = value;
}
// flag��?
bool IsFlag(Cell* cell)
{
	return cell->flag;
}
// flag �𔽓]
void FlipFlag(Cell* cell)
{
	cell->flag = !cell->flag;
}
// adjacentBombs���擾
int GetAdjacentBombs(Cell* cell)
{
	return cell->adjacentBombs;
}
// adjacentBombs���Z�b�g
void SetAdjacentBombs(Cell* cell, int value)
{
	cell->adjacentBombs = value;
}
// �J������
void OpenCell(Cell* cell)
{
	cell->hide = false;
	cell->flag = false;
}