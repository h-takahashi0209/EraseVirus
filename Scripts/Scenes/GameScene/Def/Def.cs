
/// <summary>
/// �Q�[�������ʒ�`
/// </summary>
namespace TakahashiH.Scenes.GameScene
{
    /// <summary>
    /// �J�v�Z���E�G�̐F���
    /// </summary>
    public enum ColorType
    {
        None = -1,
        Red     ,
        Blue    ,
        Yellow  ,
        Sizeof  ,
    }

    /// <summary>
    /// �I�u�W�F�N�g���
    /// </summary>
    public enum ObjectType
    {
        None        ,
        HalfCapsule ,
        Enemy       ,
    }

    /// <summary>
    /// �ړ�����
    /// </summary>
    public enum MoveDirection
    {
        Left    ,
        Right   ,
        Down    ,
    }

    /// <summary>
    /// ��]����
    /// </summary>
    public enum RotateDirection
    {
        Left    ,
        Right   ,
    }

    /// <summary>
    /// �J�v�Z���̌���
    /// </summary>
    public enum CapsuleDirection
    {
        Vertial     ,
        Horizontal  ,
    }

    /// <summary>
    /// �y�A�ɂȂ��Ă���u���b�N���������
    /// </summary>
    public enum PairedBlockDirection
    {
        None    ,
        Up      ,
        Down    ,
        Right   ,
        Left    ,
    }

    /// <summary>
    /// �X�e�[�g
    /// </summary>
    public enum State
    {
        None = -1           ,
        GenerateStage       ,
        AppearCapsule       ,
        MoveCapsule         ,
        DisappearCapsule    ,
        FallCapsule         ,
        GameOver            ,
        GameClear           ,
        Finished            ,
    }

    /// <summary>
    /// ���l��`
    /// </summary>
    public static class GameSceneDef
    {
        /// <summary>
        /// 1�u���b�N�̃T�C�Y
        /// </summary>
        public const int OneBlockSize = 80;

        /// <summary>
        /// ���u���b�N��
        /// </summary>
        public const int BlockNumWidth = 8;

        /// <summary>
        /// �c�u���b�N��
        /// </summary>
        public const int BlockNumHeight = 16;

        /// <summary>
        /// �t�B�[���h����
        /// </summary>
        public const int FieldWidth = OneBlockSize * BlockNumWidth;

        /// <summary>
        /// �t�B�[���h�c
        /// </summary>
        public const int FieldHeight = OneBlockSize * BlockNumHeight;

        /// <summary>
        /// �J�v�Z���o��X�u���b�N���W
        /// </summary>
        public const int CapsuleAppearedBlockPosX = BlockNumWidth / 2;

        /// <summary>
        /// �J�v�Z���o��Y�u���b�N���W
        /// </summary>
        public const int CapsuleAppearedBlockPosY = BlockNumHeight - 1;

        /// <summary>
        /// �����F�̃u���b�N����������������ł����邩
        /// </summary>
        public const int DisappearBlockMatchCount = 4;
    }
}
