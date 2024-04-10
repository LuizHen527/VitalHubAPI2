using System;
using System.Collections.Generic;

namespace WebAPI.Domains;

public partial class NiveisPrioridade
{
    public Guid Id { get; set; }

    public int Prioridade { get; set; }

    public virtual ICollection<Consulta> Consulta { get; set; } = new List<Consulta>();
}
