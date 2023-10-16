// 日本語対応
using UnityEngine;

public class ParticleDataTransferObject : MonoBehaviour
{
    private static ParticleDataTransferObject _current;
    public static ParticleDataTransferObject Current => _current;

    private void OnEnable()
    {
        _current = this;
    }

    private void OnDisable()
    {
        _current = null;
    }


}