//======================================
//	3D�_���W���� �f�B���N�V����(����)
//======================================
#include "Direction.h"
#include <assert.h>

// �S�����̃x�N�^�[�擾
Vector2 GetDirVector2(Direction d)
{
	static Vector2 dirVector2[] = {
		{ 0,-1},  //DIR_NORTH
		{-1, 0},  //DIR_WEST
		{ 0, 1},  //DIR_SOUTH,
		{ 1, 0},  //DIR_EAST,
	};
	assert(0 <= d && d < DIR_MAX);
	return dirVector2[d];
}

// �f�B���N�V�������Z
Direction DirectionAdd(Direction dir, int add)
{
	int tmp = dir + add;
	return (Direction)((tmp + DIR_MAX) % DIR_MAX);
}
// �f�B���N�V�����͈̓`�F�b�N
bool IsInDirection(Direction d)
{
	return 0 <= d && d < DIR_MAX;
}