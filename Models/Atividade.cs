namespace MinhaAgendaBackend.Models
{
    public class Atividade
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Start { get; set; } = string.Empty;
        public string End { get; set; } = string.Empty;
        public string Cat { get; set; } = string.Empty;
    }
}