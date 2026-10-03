namespace SunamoMsSqlLegacy;

/// <summary>
/// Podporované typy sloupců MS SQL Serveru.
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