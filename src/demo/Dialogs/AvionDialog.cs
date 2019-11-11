using demo.State;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace demo.Dialogs
{
    public class AvionDialog : WaterfallDialog
    {
        //var conversationStateAccessors = _conversationState.CreateProperty<AvionState>(nameof(ConversationData));
        //var conversationData = await conversationStateAccessors.GetAsync(turnContext, () => new ConversationData());

        public AvionDialog(string dialogId, AvionState state, IEnumerable<WaterfallStep> steps = null) 
            : base(dialogId, steps)
        {
            AddStep(async (c, t) =>
            {
                return await c.PromptAsync("prompt", new PromptOptions
                {
                    Prompt = c.Context.Activity.CreateReply("A quel jour voulez-vous partir ? ")
                });
            })
            .AddStep(async (c, t) =>
            {
                jour = c.Result.ToString();
                return await c.PromptAsync("prompt", new PromptOptions
                {
                    Prompt = c.Context.Activity.CreateReply("Quel destination ? ")
                });
            })
            .AddStep(async (c, t) =>
            {
                destination = c.Result.ToString();
                return await c.PromptAsync("prompt", new PromptOptions
                {
                    Prompt = c.Context.Activity.CreateReply($"Vous voulez partir le {jour} à {destination} ")
                });
            });
        }

        public static string ID => typeof(AvionDialog).Name;
    }
}
