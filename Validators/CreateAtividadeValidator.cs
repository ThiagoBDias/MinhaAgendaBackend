using FluentValidation;
using MinhaAgendaBackend.DTOs;

namespace MinhaAgendaBackend.Validators
{
    public class CreateAtividadeValidator : AbstractValidator<CreateAtividadeRequest>
    {
        public CreateAtividadeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome da atividade é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.");

            RuleFor(x => x.Start)
                .NotEmpty().WithMessage("O horário de início é obrigatório.");

            RuleFor(x => x.End)
                .NotEmpty().WithMessage("O horário de término é obrigatório.");

            RuleFor(x => x.Cat)
                .NotEmpty().WithMessage("A categoria é obrigatória.");
        }
    }
}