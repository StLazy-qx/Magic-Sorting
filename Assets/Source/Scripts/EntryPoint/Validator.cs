using System;

namespace Assets.Source.Scripts.EntryPoint
{
    public static class Validator
    {

        public static void ValidateDependencies(params (string name, object? value, string? customMessage)[] dependencies)
        {
            foreach (var (name, value, customMessage) in dependencies)
            {
                if (value == null)
                {
                    string message = customMessage ?? $"{name} не назначен в инспекторе.";
                    throw new InvalidOperationException(message);
                }
            }
        }
    }

    /*
     * private AnimationParticle _animationParticle;
    private CellRouter _cellRouter;
    private MagicColumnPool _magicColumnPool;
    private VesselPool _vesselPool;
    private WaitingPoint _waitingPoint;

    private void Awake()
    {
        Validation.ValidateDependencies(
            (nameof(_animationParticle), _animationParticle, null),
            (nameof(_cellRouter), _cellRouter, null),
            (nameof(_magicColumnPool), _magicColumnPool, null),
            (nameof(_vesselPool), _vesselPool, null),
            (nameof(_waitingPoint), _waitingPoint, "не назначен или пуст.")
        );
    }
    */
}
