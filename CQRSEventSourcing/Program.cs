using System;
using System.Collections.Generic;

namespace CQRSEvenSourcing
{
    internal class Climate
    {
        private double temperature;
        private int humidity;

        readonly EventManager eventBroker;
        public Climate(EventManager evtBroker)
        {
            eventBroker = evtBroker;
            eventBroker.Commands += EventManager_Commands;
            eventBroker.Queries += EventManager_Queries;
        }

        private void EventManager_Queries(object sender, Query e)
        {
            if (e is QueryClimate queryVer && queryVer.TargetEntity == this)
            {
                queryVer.ReturnValue = temperature;
            }
        }

        private void EventManager_Commands(object sender, Command command)
        {
            if (command is UpdateTemperature changeTemperatureObj && changeTemperatureObj.TargetEntity == this)
            {
                if (!command.RollbackCommand)
                {
                    eventBroker.EventList.Add(new TemperatureUpdatedEvent(this, DateTime.Now, temperature, changeTemperatureObj.Temperature));
                }
                temperature = changeTemperatureObj.Temperature;
            }
            else if (command is UpdateHumidity updateHumidityObj && updateHumidityObj.TargetEntity == this)
            {
                if (!command.RollbackCommand)
                {
                    eventBroker.EventList.Add(new TemperatureUpdatedEvent(this, DateTime.Now, temperature, updateHumidityObj.Humidity));
                }
                temperature = updateHumidityObj.Humidity;
            }
        }
    }

    internal class EventManager
    {
        public List<ClimateEvent> EventList = new List<ClimateEvent>();
        public event EventHandler<Command> Commands;
        public event EventHandler<Query> Queries;

        public void CommandMe(Command command)
        {
            Commands?.Invoke(this, command);
        }

        public T QueryMe<T>(Query query)
        {
            Queries?.Invoke(this, query);
            return (T)query.ReturnValue;
        }

        public void UndoLastEvent()
        {
            var lastEvent = EventList[EventList.Count];
            if (lastEvent is TemperatureUpdatedEvent updateTemperatureEvent)
            {
                CommandMe(new UpdateTemperature(updateTemperatureEvent.TargetObject, updateTemperatureEvent.OldValue));
                EventList.Remove(updateTemperatureEvent);
            }
        }
    }

    internal class Query
    {
        public object ReturnValue;
    }

    class QueryClimate : Query
    {
        public Climate TargetEntity;
    }

    internal class Command : EventArgs
    {
        public bool RollbackCommand = false;
    }

    internal class UpdateTemperature : Command
    {
        public Climate TargetEntity;
        public double Temperature;
        public UpdateTemperature(Climate targetEntity, double temperature)
        {
            TargetEntity = targetEntity;
            Temperature = temperature;
        }
    }

    internal class UpdateHumidity : Command
    {
        public Climate TargetEntity;
        public double Humidity;
        public UpdateHumidity(Climate targetEntity, double humidityPercentage)
        {
            TargetEntity = targetEntity;
            Humidity = humidityPercentage;
        }
    }

    internal class ClimateEvent
    {
        private readonly DateTime dateRecorded;
        public ClimateEvent(DateTime dateTime)
        {
            dateRecorded = dateTime;
        }


    }

    class HumidityUpdatedEvent : ClimateEvent
    {
        public Climate TargetObject;
        public int OldValue, NewValue;
        public HumidityUpdatedEvent(Climate climate, DateTime dateTimeRecorded, int oldHumidity, int newHumidity) : base(dateTimeRecorded)
        {
            TargetObject = climate;
            OldValue = oldHumidity;
            NewValue = newHumidity;
        }

        public override string ToString()
        {
            return $"Humidity has been updated from {OldValue} to {NewValue}";
        }
    }

    class TemperatureUpdatedEvent : ClimateEvent
    {
        public Climate TargetObject;
        public double OldValue, NewValue;
        public TemperatureUpdatedEvent(Climate climate, DateTime dateTimeRecorded, double oldTemp, double newTemp) : base(dateTimeRecorded)
        {
            TargetObject = climate;
            OldValue = oldTemp;
            NewValue = newTemp;
        }

        public override string ToString()
        {
            return $"Temperature has been updated from {OldValue} to {NewValue}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EventManager eventManager = new EventManager();
            Climate climate = new Climate(eventManager);
            eventManager.CommandMe(new UpdateTemperature(climate, 28.56));
            double recordedTemperature = eventManager.QueryMe<double>(new QueryClimate() { TargetEntity = climate });
            Console.WriteLine(recordedTemperature.ToString());
            foreach (var item in eventManager.EventList)
            {
                Console.WriteLine(item.ToString());
            }
            //Revert all the changes
            foreach (var item in eventManager.EventList)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
