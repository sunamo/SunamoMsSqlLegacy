namespace SunamoMsSqlLegacy.Enums;

/// <summary>
/// Supported MS SQL Server column types.
/// </summary>
public enum SqlDbType2
{
    #region Nikdy nepoužívat

    /// <summary>
    /// The date time 2 value.
    /// </summary>
    DateTime2,

    #endregion Nikdy nepoužívat

    /// <summary>
    /// The unique identifier value.
    /// </summary>
    UniqueIdentifier,
    /// <summary>
    /// The unique identifier auto new id value.
    /// </summary>
    UniqueIdentifierAutoNewId,
    /// <summary>
    /// The date value.
    /// </summary>
    Date,
    /// <summary>
    /// The small date time value.
    /// </summary>
    SmallDateTime,
    /// <summary>
    /// The real value.
    /// </summary>
    Real,
    /// <summary>
    /// The int value.
    /// </summary>
    Int,
    /// <summary>
    /// The n var char value.
    /// </summary>
    NVarChar,
    /// <summary>
    /// The n char value.
    /// </summary>
    NChar,
    /// <summary>
    /// The bit value.
    /// </summary>
    Bit,
    /// <summary>
    /// The tiny int value.
    /// </summary>
    TinyInt,
    /// <summary>
    /// The small int value.
    /// </summary>
    SmallInt,
    /// <summary>
    /// The binary value.
    /// </summary>
    Binary,
    /// <summary>
    /// The big int value.
    /// </summary>
    BigInt,
    /// <summary>
    /// The decimal value.
    /// </summary>
    Decimal,
    /// <summary>
    /// The var char value.
    /// </summary>
    VarChar,
    /// <summary>
    /// The char value.
    /// </summary>
    Char
}