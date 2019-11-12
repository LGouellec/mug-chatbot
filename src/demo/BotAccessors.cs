using demo.State;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System;

namespace demo
{
    public class BotAccessors
    {
        public BotAccessors(ConversationState conversationState)
        {
            ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
        }
        public static string DialogStateAccessorName { get; } = $"{nameof(BotAccessors)}.DialogState";
        public IStatePropertyAccessor<DialogState> DialogStateAccessor { get; set; }
        public static string AvionStateAccessorName { get; } = $"{nameof(BotAccessors)}.AvionState";
        public IStatePropertyAccessor<AvionState> AvionStateAccessor { get; set; }
        public ConversationState ConversationState { get; }
    }
}
