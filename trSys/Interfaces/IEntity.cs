using System;

namespace trSys.Interfaces;

// Интерфейс для обеспечения наличия свойства Id
public interface IEntity
{
    int Id { get; set; }
}
