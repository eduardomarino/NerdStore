﻿namespace NerdStore.Core.Data.EventSourcing;

public class StoredEvent
{
    public StoredEvent(Guid id, string tipo, DateTime dataOcorrencia, string dados)
    {
        Id = id;
        Tipo = tipo;
        DataOcorrencia = dataOcorrencia;
        Dados = dados;
    }

    public Guid Id { get; private set; }

    public string Tipo { get; private set; } // Nome do evento

    public DateTime DataOcorrencia { get; set; }

    public string Dados { get; private set; } // Dados do evento serializados
}
