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
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace demo.Bot
{
    public class BotDemo : LuisChatBot
    {
        public BotDemo(ITranslateHandler translateHandler, IDialogFactory dialogFactory, ILuisRecognizer recognizer, ITextConverter textConverter, BotAccessors accessors)
            : base(translateHandler, null, dialogFactory, recognizer, textConverter)
        {
            Dialogs = dialogFactory.UseDialogAccessor(accessors.DialogStateAccessor)
                .Create<AvionDialog>(AvionDialog.ID, accessors.AvionStateAccessor)
                .Create<TextPrompt>("prompt")
                .Build();
        }

        private byte[] GetLogo()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "demo.logo.png";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, (int)stream.Length);
                return data;
            }
        }


        protected override async Task OnTurn(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var dialogContext = await Dialogs.CreateContextAsync(turnContext, cancellationToken);
            if (turnContext.Activity.Type == ActivityTypes.Message && dialogContext.ActiveDialog == null)
            {
                var reply = turnContext.Activity.CreateReply();
                var imageData = Convert.ToBase64String(this.GetLogo());
                var card = new HeroCard
                {
                    Title = "Bot",
                    Images = new List<CardImage> { new CardImage($"data:image/png;base64,{imageData}") },
                    Text = $"Hello, what can I do for you ?",
                };
                reply.Attachments = new List<Attachment> { card.ToAttachment() };

                await dialogContext.Context.SendActivityAsync(reply);
            }
        }

        [LuisIntent("PlaneBook")]
        public async Task PlaneBookIntent(DialogContext context, RecognizerResult result, CancellationToken token)
        {
            await context.BeginDialogAsync(AvionDialog.ID, result, token);
        }
    }
}
