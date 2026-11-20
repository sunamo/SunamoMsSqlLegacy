namespace SunamoMsSqlLegacy.Helpers;

/// <summary>
/// Singleton access to MSStoredProceduresIBase.
/// </summary>
public class MSStoredProceduresI
{
    private static MSStoredProceduresIBase _ci = new MSStoredProceduresIBase();

    /// <summary>
    /// Shared instance.
    /// </summary>
    public static MSStoredProceduresIBase ci
    {
        get
        {
            return _ci;
        }
        private set
        {
            _ci = value;
        }
    }
}