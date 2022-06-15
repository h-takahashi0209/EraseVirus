
/// <summary>
/// �S�̋��ʒ�`
/// </summary>
namespace TakahashiH
{
    /// <summary>
    /// �V�[�����
    /// </summary>
    public enum SceneType
    {
        None            = -1,
        ResidentScene   ,
        TitleScene      ,
        SettingScene    ,
        GameScene       ,
        Sizeof          ,
    }

    /// <summary>
    /// �T�E���h���
    /// </summary>
    public enum SoundType
    {
        None    = -1,
        Bgm     ,
        Se      ,
        Sizeof  ,
    }

    /// <summary>
    /// �J�v�Z���������x���
    /// </summary>
    public enum FallSpeedType
    {
        Low ,
        Mid ,
        Hi  ,
    }

    /// <summary>
    /// ���l��`
    /// </summary>
    public static class CommonDef
    {
        /// <summary>
        /// �ő僌�x��
        /// </summary>
        public const int MaxLevel = 20;

        /// <summary>
        /// �t�F�[�h�ɂ����鎞�ԁi�b�j
        /// </summary>
        public const float FadeTimeSec = 0.5f;
    }
}
