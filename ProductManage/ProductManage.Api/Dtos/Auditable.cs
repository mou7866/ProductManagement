﻿namespace ProductManage.Api.Dtos;

public abstract class Auditable
{
    public Guid Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
}
