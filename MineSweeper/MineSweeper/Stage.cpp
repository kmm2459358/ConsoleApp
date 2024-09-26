//======================================
//	�}�C���X�B�[�p�[ �X�e�[�W
//======================================
#include "Stage.h"
#include "Cell.h"
#include "Utility.h"
#include <stdio.h>  // printf()
#include <assert.h>

// �֐��v���g�^�C�v
static void SetupField(Stage* stage);
static bool IsCursorPosition(Stage* stage, int x, int y);
static void UpdateField(Stage* stage);
static void OpenFieldAll(Stage* stage);
static Cell* GetCursorCell(Stage* stage);
static bool IsInField(int x, int y);
static bool IsClear(Stage* stage);
static void Beep();

// �X�e�[�W������
void InitializeStage(Stage* stage)
{
	SetupField(stage);
	UpdateField(stage);
	stage->cursorX = 0;
	stage->cursorY = 0;
	stage->isExplosion = false;
	stage->isClear = false;
}
// �t�B�[���h�̃Z�b�g�A�b�v
static void SetupField(Stage* stage)
{
	// �S�����Z�b�g
	for (int y = 0; y < FIELD_HEIGHT; y++) {
		for (int x = 0; x < FIELD_WIDTH; x++) {
			Cell* cell = GetFieldCell(stage, x, y);
			SetupCell(cell);
		}
	}
	// �����_����BOMB�z�u
	int count = BOMB_COUNT;
	while (count > 0) {
		int x = GetRand(FIELD_WIDTH);
		int y = GetRand(FIELD_HEIGHT);
		Cell* cell = GetFieldCell(stage, x, y);
		if (IsBomb(cell) == false) {
			SetBomb(cell, true);
			count--;
		}
	}
}
// �אڃ{���̐����X�V
static void UpdateField(Stage* stage)
{
	for (int y = 0; y < FIELD_HEIGHT; y++) {
		for (int x = 0; x < FIELD_WIDTH; x++) {
			Cell* cell = GetFieldCell(stage, x, y);
			int n = GetAdjacentBombsCount(stage, x, y);
			SetAdjacentBombs(cell, n);
		}
	}
}
// �t�B�[���h���J������
static void OpenFieldAll(Stage* stage)
{
	for (int y = 0; y < FIELD_HEIGHT; y++) {
		for (int x = 0; x < FIELD_WIDTH; x++) {
			Cell* cell = GetFieldCell(stage, x, y);
			OpenCell(cell);
		}
	}
}
// �אڃ{���̐����擾
int GetAdjacentBombsCount(Stage* stage, int x, int y)
{
	int count = 0;
	for (int dy = -1; dy <= 1; dy++) {
		for (int dx = -1; dx <= 1; dx++) {
			if (dx == 0 && dy == 0) {
				continue;
			}
			Cell* cell = GetFieldCell(stage, x + dx, y + dy);
			if (cell != nullptr && IsBomb(cell)) {
				count++;
			}
		}
	}
	return count;
}
// �����J��
void AutoEraseMines(Stage* stage, int x, int y)
{
	Cell* cell = GetFieldCell(stage, x, y);
	if (cell == nullptr || IsBomb(cell) || IsHide(cell) == false) {
		return;
	}
	SetHide(cell, false);
	if (GetAdjacentBombs(cell) == 0) {
		for (int dy = -1; dy <= 1; dy++) {
			for (int dx = -1; dx <= 1; dx++) {
				if (dx == 0 && dy == 0) {
					continue;
				}
				AutoEraseMines(stage, x + dx, y + dy);
			}
		}
	}
}

const char* AA_CURSOR = "��";  // �J�[�\��
const char* AA_EXPLOSION = "��"; // ����
const char* AA_FLAG = "��";  // �t���O
const char* AA_BOMB = "��";  // ���e
const char* AA_MINE = "��";  // ���J��
const char* AA_NUMBERS[] = {   // �J����
	"�E",
	"�P",
	"�Q",
	"�R",
	"�S",
	"�T",
	"�U",
	"�V",
	"�W",
	"�X",
};
// �X�N���[���`��
void DrawScreen(Stage* stage)
{
	int hideCount = 0;
	int bombCount = 0;
	int flagCount = 0;

	ClearScreen();
	for (int y = 0; y < FIELD_HEIGHT; y++) {
		for (int x = 0; x < FIELD_WIDTH; x++) {
			Cell* cell = GetFieldCell(stage, x, y);
			if (IsCursorPosition(stage, x, y) && stage->isClear == false) {
				const char* tmp = stage->isExplosion ? AA_EXPLOSION : AA_CURSOR;
				printf(tmp);
			}
			else {
				if (IsFlag(cell)) {
					printf(AA_FLAG);
				}
				else if (IsHide(cell)) {
					printf(AA_MINE);
				}
				else if (IsBomb(cell)) {
					printf(AA_BOMB);
				}
				else {
					int n = GetAdjacentBombs(cell);
					assert(0 <= n && n <= 8);
					printf(AA_NUMBERS[n]);
				}
			}
			if (IsBomb(cell)) bombCount++;
			if (IsHide(cell)) hideCount++;
			if (IsFlag(cell)) flagCount++;
		}
		putchar('\n');
	}
	putchar('\n');
	printf("�t���O�^�{��:%3d/%3d\n", flagCount, bombCount);
	int open1 = FIELD_WIDTH * FIELD_HEIGHT - hideCount;
	int open2 = FIELD_WIDTH * FIELD_HEIGHT - bombCount;
	printf("�J���@�@�@�@:%3d/%3d\n", open1, open2);
}
// �J�[�\���ʒu��?
static bool IsCursorPosition(Stage* stage, int x, int y)
{
	return x == stage->cursorX && y == stage->cursorY;
}

// �J�[�\���ړ�
void MoveCursor(Stage* stage, int addX, int addY)
{
	int x = stage->cursorX + addX;
	int y = stage->cursorY + addY;
	if (IsInField(x, y)) {
		stage->cursorX = x;
		stage->cursorY = y;
	}
}
// �J�[�\����flag���]
void FlipCursorFlag(Stage* stage)
{
	Cell* cell = GetCursorCell(stage);
	FlipFlag(cell);
}
// �J�[�\���̃Z�����J��(�I���t���O��Ԃ�)
bool OpenCursorCell(Stage* stage)
{
	Cell* cell = GetCursorCell(stage);
	SetHide(cell, false);
	if (IsBomb(cell)) {
		Beep();
		stage->isExplosion = true;
		OpenFieldAll(stage);
	}
	else if (IsClear(stage)) {
		Beep();
		stage->isClear = true;
	}
	// �����J��
	for (int dy = -1; dy <= 1; dy++) {
		for (int dx = -1; dx <= 1; dx++) {
			if (dx == 0 && dy == 0) {
				continue;
			}
			int x = stage->cursorX + dx;
			int y = stage->cursorY + dy;
			AutoEraseMines(stage, x, y);
		}
	}
	return stage->isExplosion || stage->isClear;
}
// �N���A��?
static bool IsClear(Stage* stage)
{
	for (int y = 0; y < FIELD_HEIGHT; y++) {
		for (int x = 0; x < FIELD_WIDTH; x++) {
			Cell* cell = GetFieldCell(stage, x, y);
			if (IsBomb(cell) == false && IsHide(cell)) {
				return false;
			}
		}
	}
	return true;
}
// �J�[�\���ʒu�̃Z�����擾
static Cell* GetCursorCell(Stage* stage)
{
	return GetFieldCell(stage, stage->cursorX, stage->cursorY);
}
// �t�B�[���h�̃Z���擾
Cell* GetFieldCell(Stage* stage, int x, int y)
{
	if (IsInField(x, y)) {
		return &stage->field[y][x];
	}
	return nullptr;
}
// �t�B�[���h����?
bool IsInField(int x, int y)
{
	return 0 <= x && x < FIELD_WIDTH
		&& 0 <= y && y < FIELD_HEIGHT;
}
// ������?
bool IsWin(Stage* stage)
{
	return stage->isClear;
}
// ������?
bool IsLose(Stage* stage)
{
	return stage->isExplosion;
}
// �r�[�v����炷
static void Beep()
{
	putchar('\a');
}
