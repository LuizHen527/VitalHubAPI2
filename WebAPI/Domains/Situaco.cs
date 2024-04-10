using System;
using System.Collections.Generic;

namespace WebAPI.Domains;

public partial class Situaco
{
    public Guid Id { get; set; }

    public string? Situacao { get; set; }

    public virtual ICollection<Consulta> Consulta { get; set; } = new List<Consulta>();
}
