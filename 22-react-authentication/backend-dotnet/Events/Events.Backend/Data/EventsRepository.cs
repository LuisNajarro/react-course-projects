using System.Text;
using System.Text.Json;
using Events.Backend.Util;

namespace Events.Backend.Data;

public static class EventsRepository
{
    private static async Task<EventsList?> ReadData()
    {
        var data = await File.ReadAllTextAsync("./events.json", Encoding.UTF8);
        return JsonSerializer.Deserialize<EventsList>(data);
    }
    
    private static async Task WriteData(EventsList events)
    {
        var data = JsonSerializer.Serialize(events);
        await File.WriteAllTextAsync("./events.json", data, Encoding.UTF8);
    }
    
    public static async Task<List<Event>> GetAll()
    {
        var storedData = await ReadData();
        if (storedData?.Events is null)
        {
            throw new NotFoundException("Could not find any events.");
        }

        return storedData.Events;
    }

    public static async Task<Event> Get(string id)
    {
        var storedData = await ReadData();
        if (storedData?.Events is null || storedData.Events.Count == 0)
        {
            throw new NotFoundException("Could not find any events.");
        }
        
        var @event = storedData.Events.Find(e => e.Id == id);
        if (@event is null)
        {
            throw new NotFoundException($"Could not find event for id {id}");
        }

        return @event;
    }
    
    public static async Task Add(Event data)
    {
        var storedData = await ReadData();
        if (storedData?.Events is null)
        {
            storedData = new EventsList { Events = [] };
        }

        data.Id = Guid.NewGuid().ToString();
        storedData.Events.Insert(0, data);
        await WriteData(storedData);
    }

    public static async Task Replace(string id, Event data)
    {
        var storedData = await ReadData();
        if (storedData?.Events is null || storedData.Events.Count == 0)
        {
            throw new NotFoundException("Could not find any events.");
        }
        
        var index = storedData.Events.FindIndex(e => e.Id == id);
        if (index < 0)
        {
            throw new NotFoundException($"Could not find event for id {id}");
        }
        
        data.Id = id;
        storedData.Events[index] = data;
        
        await WriteData(storedData);
    }
    
    public static async Task Remove(string id)
    {
        var storedData = await ReadData();
        var updatedData = storedData?.Events?.Where(e => e.Id != id).ToList();
        await WriteData(new EventsList { Events = updatedData });
    }
}