using System.Text;
using EventStore.Client;
using Microsoft.Extensions.Configuration;
using NerdStore.Core.Data.EventSourcing;
using NerdStore.Core.Messages;
using Newtonsoft.Json;

namespace EventSourcing
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        private readonly string _connectionString;

        public EventSourcingRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EventStoreConnection");
        }

        public async Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event
        {
            var settings = EventStoreClientSettings.Create(_connectionString);
            var client = new EventStoreClient(settings);

            var eventos = FormatarEvento(evento);

            await client.AppendToStreamAsync(
                evento.AggregateId.ToString(),
                StreamState.Any,
                eventos);
        }

        public async Task<IEnumerable<StoredEvent>> ObterEventos(Guid aggregateId)
        {
            var listaEventos = new List<StoredEvent>();

            var settings = EventStoreClientSettings.Create(_connectionString);
            var client = new EventStoreClient(settings);

            // Lê os eventos do stream do início (posição 0) em diante
            var result = client.ReadStreamAsync(
                Direction.Forwards, // Lê na direção para frente
                aggregateId.ToString(), // Nome do stream baseado no AggregateId
                StreamPosition.Start // Posição inicial no stream (0)
            );

            await foreach (var resolvedEvent in result)
            {
                var dataEncoded = Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray());
                var jsonData = JsonConvert.DeserializeObject<BaseEvent>(dataEncoded);

                var evento = new StoredEvent(
                    resolvedEvent.Event.EventId.ToGuid(),
                    resolvedEvent.Event.EventType,
                    jsonData!.Timestamp,
                    dataEncoded
                );

                listaEventos.Add(evento);
            }

            return listaEventos.OrderBy(e => e.DataOcorrencia);
        }

        private static IEnumerable<EventData> FormatarEvento<TEvent>(TEvent evento) where TEvent : Event
        {
            yield return new EventData(
                Uuid.NewUuid(),
                evento.GetType().Name,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evento)),
                metadata: null
            );
        }
    }

    internal class BaseEvent
    {
        public DateTime Timestamp { get; set; }
    }
}