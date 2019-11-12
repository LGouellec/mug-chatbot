using Bot.Builder.Community.Dialogs.FormFlow;
using bot_framework_extensions.Extension;
using demo.State;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace demo.Dialogs
{
    public class AvionDialog : WaterfallDialog
    {
        public AvionDialog(string dialogId, IStatePropertyAccessor<AvionState> stateProperty, IEnumerable<WaterfallStep> steps = null) 
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
                AvionState state = await stateProperty.GetAsync(c.Context, () => new AvionState(), t);
                state.Jour = c.Result.ToString();
                return await c.PromptAsync("prompt", new PromptOptions
                {
                    Prompt = c.Context.Activity.CreateReply("Quel destination ? ")
                });
            })
            .AddStep(async (c, t) =>
            {
                AvionState state = await stateProperty.GetAsync(c.Context, () => new AvionState(), t);
                state.Destination = c.Result.ToString();
                return await c.PromptAsync("prompt", new PromptOptions
                {
                    Prompt = c.Context.Activity.CreateReply($"Vous voulez partir le {state.Jour} à {state.Destination} ")
                });
            });

            //AddStep(async (c, t) =>
            //{
            //    AvionFormBuilder builder = new AvionFormBuilder();
            //    var form = builder.Build(c.Context, t);
            //    var dialog = FormDialog.FromForm(() => form, FormOptions.PromptInStart);

            //    return await c.Call(dialog, c.Options as RecognizerResult);
            //});
        }

        public static string ID => typeof(AvionDialog).Name;
    }
}
