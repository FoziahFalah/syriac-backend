namespace SyriacSources.Backend.Application.Common.Constants;
internal class DefaultValidationMessageConsts
{
    public static string MinLengthErrorMsg { get; set; } = "Please insert atleast {0} characters";
    public static string MaxLengthErrorMsg { get; set; } = "Maximum characters allowed is {0} characters";
    public static string IsRequiredErrorMsg { get; set; } = "'{PropertyName}' is required.";
    public static string BeUniqueErrorMsg { get; set; } = "'{PropertyName}' must be unique.";
    public static string BeEmailErrorMsg { get; set; } = "'{PropertyName}' must be a valid email address.";

}
