using FluentValidation.Results;

namespace Canvia.Facturacion.Application.Commons;

public class BaseResponse<T>
{
    public bool IsSucces { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public IEnumerable<ValidationFailure>? Errors { get; set; }
}
