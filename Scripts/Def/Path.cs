
namespace TakahashiH
{
    /// <summary>
    /// �p�X��`�p�N���X
    /// </summary>
    public static class Path
    {
        /// <summary>
        /// ����
        /// </summary>
        public static class Common
        {
            /// <summary>
            /// �T�E���h�f�[�^
            /// </summary>
            public static string SoundData = "ScriptableObjects/Scenes/";
        }

        /// <summary>
        /// �V�[����
        /// </summary>
        public static class Scenes
        {
            #region GameScene

            /// <summary>
            /// GameScene
            /// </summary>
            public static class GameScene
            {
                /// <summary>
                /// �ݒ�t�@�C��
                /// </summary>
                public static string Settings = "ScriptableObjects/Scenes/GameScene/GameSceneSettings";

                /// <summary>
                /// ���J�v�Z���摜
                /// </summary>
                public static string HalfCapsuleImage = "Textures/Scenes/GameScene/HalfCapsule/HalfCapsule";

                /// <summary>
                /// �y�A�̔��J�v�Z���摜
                /// </summary>
                public static string PairedHalfCapsuleImage = "Textures/Scenes/GameScene/HalfCapsule/HalfCapsulePaired";

                /// <summary>
                /// �G�摜
                /// </summary>
                public static string EnemyImage = "Textures/Scenes/GameScene/Enemy/Enemy";

                /// <summary>
                /// �X�e�[�W csv
                /// </summary>
                public static string StageCsv = "Stage/Stage";
            }

            #endregion
        }
    }
}
