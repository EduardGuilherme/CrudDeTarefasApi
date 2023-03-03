using System.ComponentModel;

namespace crudtarefas.Enums
{
    public enum StatusTarefas
    {
        [Description("A fazer")]
        Afazer = 1,
        [Description("Em Andamento")]
        EmAndamento = 2,
        [Description("Concluído")]
        Concluido = 3

    }
}
