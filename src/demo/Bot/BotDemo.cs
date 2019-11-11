using bot_framework_extensions.Bot;
using bot_framework_extensions.Converter;
using bot_framework_extensions.Dialog;
using bot_framework_extensions.Luis;
using bot_framework_extensions.Recognizer;
using demo.Dialogs;
using demo.State;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace demo.Bot
{
    public class BotDemo : LuisChatBot
    {
        public BotDemo(ITranslateHandler translateHandler, IDialogFactory dialogFactory, ILuisRecognizer recognizer, ITextConverter textConverter, BotAccessors accessors, AvionState avionState)
            : base(translateHandler, null, dialogFactory, recognizer, textConverter)
        {
            Dialogs = dialogFactory.UseDialogAccessor(accessors.DialogStateAccessor)
                .Create<AvionDialog>(AvionDialog.ID, new object[] { avionState })
                .Create<TextPrompt>("prompt")
                .Build();
        }

        protected override async Task OnTurn(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            if (turnContext.Activity.Type == ActivityTypes.Message)
                await turnContext.SendActivityAsync("Coucou !");
        }

        [LuisIntent("Hello")]
        public async Task HelloItent(DialogContext context, RecognizerResult result, CancellationToken token)
        {
            await context.BeginDialogAsync(AvionDialog.ID, result, token);
        }
    }
}
