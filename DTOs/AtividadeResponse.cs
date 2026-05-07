namespace MinhaAgendaBackend.DTOs
{
    public record AtividadeResponse(
        int Id, 
        string Name, 
        string Start, 
        string End, 
        string Cat
    );
}