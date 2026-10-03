namespace SunamoMsSqlLegacy;

/// <summary>
/// Singleton přístup k MSStoredProceduresIBase.
/// </summary>
public class MSStoredProceduresI
{
    private static MSStoredProceduresIBase _ci = new MSStoredProceduresIBase();

    /// <summary>
    /// Sdílená instance.
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