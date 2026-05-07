namespace MinhaAgendaBackend.DTOs
{
    public record CreateAtividadeRequest(
        string Name,
        string Start,
        string End,
        string Cat
    );
}