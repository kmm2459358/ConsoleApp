//======================================
//	�퍑�V�~�����[�V����  ��
//======================================
#include "Castle.h"
#include "Lord.h"
#include "Troop.h"

Castle castles[] =
{
	// CASTLE_YONEZAWA   �đ�� 
	{
		"�đ��",   // ���O
		LORD_DATE,  // ���
		TROOP_BASE, // ����
		// �ڑ����ꂽ��̃��X�g
		{
			CASTLE_KASUGAYAMA,  // �t���R��
			CASTLE_ODAWARA,     // ���c����
			CASTLE_NONE,
		},
		45, 2,      // �`��ʒu
		"�đ�",     // �}�b�v��̖��O
	},
	// CASTLE_KASUGAYAMA �t���R��
	{
		"�t���R��",     // ���O
		LORD_UESUGI,    // ���
		TROOP_BASE,     // ����
		// �ڑ����ꂽ��̃��X�g
		{
			CASTLE_YONEZAWA,        // �đ��
			CASTLE_TSUTSUJIGASAKI,  // �U�P�����
			CASTLE_GIFU,             // �򕌏�
			CASTLE_NONE,
		},
		37, 3,          // �`��ʒu
		"�t��",         // �}�b�v��̖��O
	},
	// CASTLE_TSUTSUJIGASAKI �U�P�����
	{
		"�U�P�����",   // ���O
		LORD_TAKEDA,    // ���
		TROOP_BASE,     // ����
		// �ڑ����ꂽ��̃��X�g
		{
			CASTLE_KASUGAYAMA,  // �t���R��
			CASTLE_ODAWARA,     // ���c����
			CASTLE_OKAZAKI,     // �����
			CASTLE_NONE,
		},
		39, 6,          // �`��ʒu
		"�U�P",         // �}�b�v��̖��O
	},
	// CASTLE_ODAWARA    ���c����
	{
		"���c����", // ���O
		LORD_HOJO,  // ���
		TROOP_BASE, // ����
		// �ڑ����ꂽ��̃��X�g
		{
			CASTLE_YONEZAWA,        // �đ��
			CASTLE_TSUTSUJIGASAKI,  // �U�P�����
			CASTLE_OKAZAKI,         // �����
			CASTLE_NONE,
		},
		41, 9,      // �`��ʒu
		"���c",     // �}�b�v��̖��O
	},
	// CASTLE_OKAZAKI    �����
	{
		"�����",       // ���O
		LORD_TOKUGAWA,  // ���
		TROOP_BASE,     // ����
		// �ڑ����ꂽ��̃��X�g
		{
			CASTLE_TSUTSUJIGASAKI,  // �U�P�����
			CASTLE_ODAWARA,         // ���c����
			CASTLE_GIFU,            // �򕌏�
			CASTLE_NONE,
		},
		33, 9,          // �`��ʒu
		"����",         // �}�b�v��̖��O
	},
	// CASTLE_GIFU   �򕌏�
	{
		"�򕌏�",   // ���O
		LORD_ODA,   // ���
		TROOP_BASE, // ����
		// �ڑ����ꂽ��̃��X�g
		{
			CASTLE_KASUGAYAMA,  // �t���R��
			CASTLE_OKAZAKI,     // �����
			CASTLE_NIJO,        // �����
			CASTLE_NONE,
		},
		27, 8,      // �`��ʒu
		"��",     // �}�b�v��̖��O
	},
	// CASTLE_NIJO   �����
	{
		"�����",       // ���O
		LORD_ASHIKAGA,  // ���
		TROOP_BASE,     // ����
		// �ڑ����ꂽ��̃��X�g
		{
			CASTLE_GIFU,            // �򕌏�
			CASTLE_YOSHIDAKORIYAMA, // �g�c�S�R��
			CASTLE_OKO,             // ���L��
			CASTLE_NONE,
		},
		19, 9,          // �`��ʒu
		"���",         // �}�b�v��̖��O
	},
	// CASTLE_YOSHIDAKORIYAMA    �g�c�S�R��
	{
		"�g�c�S�R��",   // ���O
		LORD_MORI,      // ���
		TROOP_BASE,     // ����
		// �ڑ����ꂽ��̃��X�g
		{
			CASTLE_NIJO,    // �����
			CASTLE_OKO,     // ���L��
			CASTLE_UCHI,    // ����
			CASTLE_NONE,
		},
		11, 9,          // �`��ʒu
		"�g�c",         // �}�b�v��̖��O
	},
	// CASTLE_OKO    ���L��
	{
		"���L��",       // ���O
		LORD_CHOSOKABE, // ���
		TROOP_BASE,     // ����
		// �ڑ����ꂽ��̃��X�g
		{
			CASTLE_NIJO,            // �����
			CASTLE_YOSHIDAKORIYAMA, // �g�c�S�R��
			CASTLE_UCHI,            // ����
			CASTLE_NONE,
		},
		13,12,          // �`��ʒu
		"���L",         // �}�b�v��̖��O
	},
	// CASTLE_UCHI  ����
	{
		"����",         // ���O
		LORD_SIMAZU,    // ���
		TROOP_BASE,     // ����
		//  �ڑ����ꂽ��̃��X�g
		{
			CASTLE_YOSHIDAKORIYAMA, // �g�c�S�R��
			CASTLE_OKO,             // ���L��
			CASTLE_NONE,
		},
		 3,14,          // �`��ʒu
		"����",         // �}�b�v��̖��O
	},
};
// ��̖��O���擾
const char* GetCastleName(Castle* castle)
{
	// castle��name��Ԃ��܂�
	return castle->name;
}
// ���ID���擾
LordId GetCastleOwner(Castle* castle)
{
	// castle��owner��Ԃ��܂�
	return castle->owner;
}
// �������擾
int GetCastleTroopCount(Castle* castle)
{
	// castle��troopCount��Ԃ��܂�
	return castle->troopCount;
}
// �ߗׂ̏郊�X�g���擾
CastleId* GetCastleConnectedList(Castle* castle)
{
	// castle��connectedList��Ԃ��܂�
	return castle->connectedList;
}
// �`��X���W���擾
int GetCastleCurx(Castle* castle)
{
	// castle��curx��Ԃ��܂�
	return castle->curx;
}
// �`��Y���W���擾
int GetCastleCury(Castle* castle)
{
	// castle��cury��Ԃ��܂�
	return castle->cury;
}
// �}�b�v��̖��O���擾
const char* GetCastleMapName(Castle* castle)
{
	// castle��mapName��Ԃ��܂�
	return castle->mapName;
}
// ������������
void SetCastleTroopCount(Castle* castle, int troopCount)
{
	// castle��troopCount�֒l���Z�b�g���܂�
	castle->troopCount = troopCount;
}
// �����Z�b�g
void SetCastleOwner(Castle* castle, LordId owner)
{
	// castle��owner�֒l���Z�b�g���܂�
	castle->owner = owner;
}
// �����ɉ��Z
void AddCastleTroopCount(Castle* castle, int add)
{
	int value = castle->troopCount + add;
	if (value < 0) {
		value = 0;
	}
	else if (value > TROOP_MAX) {
		value = TROOP_MAX;
	}
	castle->troopCount = value;
}