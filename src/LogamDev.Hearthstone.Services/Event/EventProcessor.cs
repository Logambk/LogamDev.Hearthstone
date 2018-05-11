using System;
using System.Collections.Generic;
using System.Linq;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Services.Log;
using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Enum;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.State;
using LogamDev.Hearthstone.Vo.Utility;

namespace LogamDev.Hearthstone.Services.Event
{
    public class EventProcessor : IEventProcessor
    {
        private readonly ILogger logger;
        private readonly AttackProcessor attackProcessor;

        public EventProcessor(ILogger logger)
        {
            this.logger = logger;
            attackProcessor = new AttackProcessor(logger, this);
        }
        
        /// <summary>
        /// Processes the events recursively
        /// </summary>
        /// <param name="state"></param>
        /// <param name="ev"></param>
        /// <returns>Returned events are already processed ones</returns>
        public List<EventBase> ProcessEvent(ServerGameState state, EventBase ev)
        {
            logger.Log(LogType.Services, LogSeverity.Info, $"Processing event {ev.ToString()}");

            var triggeredEvents = CheckTriggers(state, ev);

            switch (ev.Type)
            {
                case GameEventType.CardPlayed:
                    return ProcessPlayCard(state, ev as EventCardPlayed);
                case GameEventType.CharacterAttacks:
                    return attackProcessor.ProcessAttack(state, ev as EventCharacterAttacks);
            }

            return new List<EventBase> { ev };
        }

        private List<EventBase> CheckTriggers(ServerGameState state, EventBase ev)
        {
            var myEventFilter = new PredicatedEvent()
            {
                Event = ev,
                Filter = ev.GetFilter(),
                Side = PredicateSide.Friendly
            };

            var oppEventFilter = new PredicatedEvent()
            {
                Event = ev,
                Filter = ev.GetFilter(),
                Side = PredicateSide.Enemy
            };

            var myTriggeredEvents = state.Me.Triggers.GetAssociatedEvents(myEventFilter);
            var oppTriggeredEvents = state.Opp.Triggers.GetAssociatedEvents(oppEventFilter);

            if (myTriggeredEvents.Any() || oppTriggeredEvents.Any())
            {
                // TODO: form and re-query chain of events
                // TODO: check the event orders / orders of minion appearances
                // TODO: re-call ProcessEvent() for each triggered event and return the resulting events
                throw new NotImplementedException();
            }

            return new List<EventBase>();
        }

        #region TODO: move it out

        private List<EventBase> ProcessPlayCard(ServerGameState state, EventCardPlayed playCardEvent)
        {
            var events = new List<EventBase>();
            events.Add(playCardEvent);

            var card = playCardEvent.Card;
            switch (card.Type)
            {
                case CardType.Spell:
                    break;
                case CardType.Minion:
                    var minion = new Minion(card as CardMinion, state.Me.MinionOrderNumber++);
                    logger.Log(LogType.Services, LogSeverity.Info, $"{minion.Card.Name} is summoned");
                    state.Me.Minions.Insert(playCardEvent.MinionPosition.Value, minion);
                    events.AddRange(ProcessEvent(state, new EventMinionSummoned() { MinionId = minion.Id }));
                    break;
                case CardType.Weapon:
                    break;
                case CardType.Hero:
                    break;
            }

            return events;
        }

        #endregion
    }
}
