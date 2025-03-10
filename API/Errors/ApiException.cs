using System;

namespace API.Errors;

public class ApiException(int stausCode, string message, string? details)
{
    public int StausCode { get; set; } = stausCode;
    public string Message { get; set; } = message;
    public string? Details { get; set; } = details;

}
