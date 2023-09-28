using System;

namespace Actor
{
    /// <summary>
    /// Actor�̏�Ԉُ���Ǘ�����N���X
    /// </summary>
    public class ActorStatusEffect
    {
        private StatusEffectType _currentStatus;
        public StatusEffectType CurrentStatus => _currentStatus;

        public void AddStatusEffect(StatusEffectType status)
        {
            _currentStatus |= status;
        }
        public void RemoveStatusEffect(StatusEffectType status)
        {
            _currentStatus &= ~status;
        }

        public ActorStatusEffect Clone()
        {
            // ActorStatusEffect�N���X�̐V�����C���X�^���X�ɃR�s�[
            ActorStatusEffect newStatusEffect = new ActorStatusEffect();

            // StatusEffectType�񋓌^�̃t�B�[���h���R�s�[
            newStatusEffect._currentStatus = _currentStatus;

            return newStatusEffect;
        }
    }

    [Serializable, Flags]
    public enum StatusEffectType
    {
        /// <summary> ���� </summary>
        None = 0,
        /// <summary> �S�� </summary>
        All = -1,
        /// <summary> �� </summary>
        Poison = 1,
        /// <summary> �ғ� </summary>
        SeverePoison = 2,
        /// <summary> �₯�� </summary>
        Burn = 4,
        /// <summary> ��� </summary>
        Paralysis = 8,
        /// <summary> ���� </summary>
        Sleep = 16,
        /// <summary> ���� </summary>
        Confusion = 32,
        /// <summary> �w�C�X�g,������� </summary>
        Haste = 64,
        /// <summary> �X���E,������� </summary>
        Slow = 128,
    }
}