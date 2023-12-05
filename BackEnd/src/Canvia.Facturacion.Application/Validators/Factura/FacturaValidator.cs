using Canvia.Facturacion.Application.Dtos.Request;
using FluentValidation;

namespace Canvia.Facturacion.Application.Validators.Factura;

public sealed class FacturaValidator:AbstractValidator<FacturaCabeceraRequestDto>
{
    public FacturaValidator()
    {
        RuleFor(c => c.ClienteID)
            .NotEmpty().WithMessage("El Id del cliente es requerido.")
            .NotNull().WithMessage("El Id del cliente es requerido.");
        RuleFor(c=>c.FechaEmision)
            .NotEmpty().WithMessage("La fecha de emisión es requerido.")
            .NotNull().WithMessage("La fecha de emisión es requerido.");
        RuleFor(c => c.Items)
            .NotNull().WithMessage("Los items son requeridos.");
        RuleForEach(c => c.Items).Must(x => x.Cantidad > 0).WithMessage("La cantidad debe ser mayor que 0.");
    }
}
