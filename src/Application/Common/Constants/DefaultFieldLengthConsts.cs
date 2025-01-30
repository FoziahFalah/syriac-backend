namespace SyriacSources.Backend.Application.Common.Constants;
public class DefaultFieldLengthConsts
{

    /// <summary>
    /// Default value: 3
    /// </summary>
    public static int MinNameLength { get; set; } = 2;

    /// <summary>
    /// Default value: 128
    /// </summary>
    public static int MaxNameLength { get; set; } = 128;

    /// <summary>
    /// Default value: 3
    /// </summary>
    public static int MinEmailLength { get; set; } = 3;

    /// <summary>
    /// Default value: 254
    /// </summary>
    public static int MaxEmailLength { get; set; } = 254;

    /// <summary>
    /// Default value: 10
    /// </summary>
    public static int MinTitleLength { get; set; } = 10;

    /// <summary>
    /// Default value: 30
    /// </summary>
    public static int MaxTitleLength { get; set; } = 100;

    /// <summary>
    /// Default value: 5
    /// </summary>
    public static int MinTextboxLength{ get; set; } = 5;

    /// <summary>
    /// Default value: 5000
    /// </summary>
    public static int MaxTextboxLength { get; set; } = 5000;

    /// <summary>
    /// Default value: 5
    /// </summary>
    public static int MinDescriptionLength { get; set; } = 5;

    /// <summary>
    /// Default value: 300
    /// </summary>
    public static int MaxDescriptionLength { get; set; } = 300;


    /// <summary>
    /// Default value: 15
    /// </summary>
    public static int MaxPhoneLength { get; set; } = 15;


    /// <summary>
    /// Default value: 200
    /// </summary>
    public static int MaxShortDescription { get; set; } = 200;

}
