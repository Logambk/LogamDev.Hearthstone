using System;
using System.Collections.Generic;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Vo.Enum;
using LogamDev.Hearthstone.Vo.Interaction;
using LogamDev.Hearthstone.Vo.State;
using LogamDev.Hearthstone.Vo.Utility;

namespace LogamDev.Hearthstone.Services.Interaction
{
    public class UserInteractionProcessor : IUserInteractionProcessor
    {
        private readonly IRuleSet ruleSet;
        private readonly ILogger logger;
        private readonly AttackProcessor attackProcessor;
        private readonly PlayCardProcessor playCardProcessor;
        private readonly EndTurnProcessor endTurnProcessor;

        public UserInteractionProcessor(IRuleSet ruleSet, ILogger logger, IEventProcessor eventProcessor)
        {
            this.ruleSet = ruleSet;
            this.logger = logger;
            attackProcessor = new AttackProcessor(eventProcessor);
            playCardProcessor = new PlayCardProcessor(logger, ruleSet, eventProcessor);
            endTurnProcessor = new EndTurnProcessor();
        }

        public void ProcessInteraction(ServerGameState fullState, InteractionBase interaction)
        {
            switch (interaction.Type)
            {
                case InteractionType.Attack:
                    attackProcessor.ProcessAttack(fullState, interaction as InteractionAttack);
                    return;

                case InteractionType.PlayCard:
                    playCardProcessor.ProcessPlayCard(fullState, interaction as InteractionPlayCard);
                    return;

                case InteractionType.EndTurn:
                    endTurnProcessor.ProcessEndTurn(fullState, interaction as InteractionEndTurn);
                    return;
            }

            throw new ArgumentOutOfRangeException("interaction.Type", interaction.Type, "Unsupported interaction type");
        }

        public ValidationResult ValidateInteraction(ClientGameState currentState, InteractionBase interaction)
        {
            switch (interaction.Type)
            {
                case InteractionType.Attack:
                    return attackProcessor.ValidateAttack(currentState, interaction as InteractionAttack);

                case InteractionType.PlayCard:
                    return playCardProcessor.ValidatePlayCard(currentState, interaction as InteractionPlayCard);

                case InteractionType.EndTurn:
                    return endTurnProcessor.ValidateEndTurn(currentState);
            }

            return new ValidationResult()
            {
                IsOk = false,
                Messages = new List<string>() { $"Unknown Interaction of type {interaction.GetType()} encountered" }
            };
        }
    }
}
