using Bot.Builder.Community.Dialogs.FormFlow;
using bot_framework_extensions.Extension;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace demo.Dialogs
{
    [Serializable]
    internal class PlaneBookQuery
    {
        [Prompt("Please enter the name of the city :")]
        public string City { get; set; }
        [Prompt("What day ? ")]
        public string Day { get; set; }
        [Prompt("What month ?")]
        public string Month{ get; set; }
        [Prompt("What year ?")]
        public string Year { get; set; }
    }

    class AvionFormBuilder
    {
        public IForm<PlaneBookQuery> Build(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var builder = new FormBuilder<PlaneBookQuery>()
                .Field(nameof(PlaneBookQuery.City), (s) => string.IsNullOrEmpty(s.City))
                .Field(nameof(PlaneBookQuery.Day), (s) => string.IsNullOrEmpty(s.Day))
                .Field(nameof(PlaneBookQuery.Month), (s) => string.IsNullOrEmpty(s.Month))
                .Field(nameof(PlaneBookQuery.Year), (s) => string.IsNullOrEmpty(s.Year))
                .OnCompletion(async (c, s) =>
                {
                    await c.Context.SendActivityAsync($"Vous voulez partir le {s.Day}/{s.Month}/{s.Year} à {s.City}");
                    c.ClearCache();
                });
            return builder.Build();
        }
    }
}
