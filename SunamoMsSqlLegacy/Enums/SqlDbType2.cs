namespace SunamoMsSqlLegacy.Enums;

/// <summary>
/// Supported MS SQL Server column types.
/// </summary>
public enum SqlDbType2
{
    #region Nikdy nepoužívat

    DateTime2,

    #endregion Nikdy nepoužívat

    UniqueIdentifier,
    UniqueIdentifierAutoNewId,
    Date,
    SmallDateTime,
    Real,
    Int,
    NVarChar,
    NChar,
    Bit,
    TinyInt,
    SmallInt,
    Binary,
    BigInt,
    Decimal,
    VarChar,
    Char
}