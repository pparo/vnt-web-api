namespace WebApi.Models;
public class Pessoa
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public DateOnly DataNascimento { get; set; }
    public string Profissao { get; set; }
}