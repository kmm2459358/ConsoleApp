//========================================
//      ���C�t�Q�[��:�p�^�[��
//========================================
// �������ɃC���N���[�h�J�[�h(�J�n)���L�����Ă�������
#ifndef __PATTEM_H
#define __PATTEM_H

typedef struct {
	int width;         // �p�^�[���̉���
	int height;        // �p�^�[���̏c��
	bool* data;        // �p�^�[��
	const char* name;  // �p�^�[���̖��O
	bool isLoopCells;  // �t�B�[���h�̍��E�A�㉺�����[�v���Ă��邩?
} Pattern;

// �p�^�[���I�����s��
Pattern* SelectPattern();

// �������ɃC���N���[�h�J�[�h(�I��)���L�����Ă�������
#endif // __PATTEM_H