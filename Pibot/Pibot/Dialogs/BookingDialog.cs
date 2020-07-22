// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.9.2

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AdaptiveCards;

using Bot.AdaptiveCard.Prompt;
using System;

namespace Pibot.Dialogs
{
    public class BookingDialog : ComponentDialog
    {
        static string AdaptivePromptId = "adaptive";

        public BookingDialog(UserState userState)
            : base(nameof(BookingDialog))
        {
            AddDialog(new AdaptiveCardPrompt(AdaptivePromptId));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new DateResolverDialog());
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                NotesStepAsync,
                CheckStepAsync,
                PersonalInfoStepAsync,
                HouseChoiceStepAsync,
                //Center1StepAsync,
                //Center2StepAsync,
                DateStepAsync,
                TimeStepAsync,
                ConfirmStepAsync,
                FinalStepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private static Attachment CreateAdaptiveCardAttachment(string filePath)
        {
            var adaptiveCardJson = File.ReadAllText(filePath);
            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };
            return adaptiveCardAttachment;
        }

        private async Task<DialogTurnResult> NotesStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var choices = new[] { "�Ϸ�" };
            var card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0))
            {
                Actions = choices.Select(choice => new AdaptiveSubmitAction
                {
                    Title = choice,
                    Data = choice,
                }).ToList<AdaptiveAction>(),
            };

            card.Body.Add(new AdaptiveTextBlock()
            {
                Text = "���� ������ �����ϱ� ����, ���ǻ����� Ȯ�����ּ���.\r\n" +
                        "- 6���� �ı��� ������ �� ������, ���� ������ �Ұ��մϴ�.\r\n" +
                        "- �ֱ� �������װ˻翡 ���� ������ ���ѵ� �� �ֽ��ϴ�.\r\n" +
                        "- ����ð� ��� �� ������ ��ҵǴ� ������ �ֽʽÿ�.\r\n" +
                        "- ������ �� ���� �� �������������� �������� ������ �ֽʽÿ�.",
                    Size = AdaptiveTextSize.Default
            });

            return await stepContext.PromptAsync(
                nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = (Activity)MessageFactory.Attachment(new Attachment
                    {
                        ContentType = AdaptiveCard.ContentType,
                        Content = JObject.FromObject(card),
                    }),
                    Choices = ChoiceFactory.ToChoices(choices),
                    Style = ListStyle.None,
                },
                cancellationToken); ;
        }
        
            private async Task<DialogTurnResult> CheckStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (((FoundChoice)stepContext.Result).Value == "�Ϸ�")
            {
                var checkcards = new Attachment[]
                {
                    new HeroCard(
                        title: "�ڰ����� Ȯ��",
                        text: "��\r\n" +
                              "���� ������ ���� ã���ֽ� ���� �������� ȯ���մϴ�!\r\n" +
                              "������ ������ �Ϸ��� �ڰ������� ��� �����ؾ� �Ѵ�ϴ�.\r\n" +
                              "�ڰ������� �Ĳ��� �о����.\r\n" +
                              "�׸��� ��� ������ ������ ������ �ڰ������� Ȯ�����ּ���.\r\n" +
                              "ī�带 ������ �Ѱܺ����?"
                        ).ToAttachment(),
                    new HeroCard(
                        images: new CardImage[]
                        { new CardImage() { Url = "http://drive.google.com/uc?export=view&id=1NILDL2SVEePd4maHuVNfN7rQhdmtfXTO" } }
                        ).ToAttachment(),
                    new HeroCard(
                        images: new CardImage[]
                        { new CardImage() { Url = "http://drive.google.com/uc?export=view&id=1Cb-epikRSeWtcpsJaiszoj731gWKRV-W" } }
                        ).ToAttachment(),
                    new HeroCard(
                        images: new CardImage[]
                        { new CardImage() { Url = "http://drive.google.com/uc?export=view&id=1o8tHm3TaOyCjwi0W-h1r0Mg82MXC6aKj" } }
                        ).ToAttachment(),
                   new HeroCard(
                        images: new CardImage[]
                        { new CardImage() { Url = "http://drive.google.com/uc?export=view&id=1rTUP3ikaB75EOQtjw4AMGPJ_boCBdGYZ" } }
                        ).ToAttachment(),
                    new HeroCard(
                        images: new CardImage[]
                        { new CardImage() { Url = "http://drive.google.com/uc?export=view&id=1Iw5GI4pGxfTnAitqYOrGi02tJbYZ0pvG" } }
                        ).ToAttachment(),
                    new HeroCard(
                        images: new CardImage[]
                        { new CardImage() { Url = "http://drive.google.com/uc?export=view&id=1c7Iqbm4WlSm9RaiIfLjd_omk3rXyICH9" } }
                        ).ToAttachment(),
                    new HeroCard(
                        title: "Ȯ���ϼ̳���?",
                        text: "��\r\n" +
                              "�ڰ� ������ �ϳ��� �������� ���Ѵٸ� �ƽ����� ������ �����Ͻ� �� �����...\r\n" +
                              "Ȥ�� ��� ������ �ֳ���?\r\n" +
                              "��\r\n" ,
                        buttons: new List<CardAction>
                        {
                            new CardAction(ActionTypes.ImBack, title: "����", value: "����"),
                            new CardAction(ActionTypes.ImBack, title: "����", value: "����"),
                        }
                        ).ToAttachment(),
                };

                var reply = MessageFactory.Attachment(checkcards);
                reply.Attachments = checkcards;
                reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                await stepContext.Context.SendActivityAsync(reply, cancellationToken);

                var messageText = "";
                var promptMessage = MessageFactory.Text(messageText, messageText, InputHints.ExpectingInput);
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
            }
            else
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"�˼��մϴ�. ���ǻ����� Ȯ������ �����ø� ������ ������ �� �����."), cancellationToken);
                return await stepContext.EndDialogAsync(null, cancellationToken);
            }
        }

        private async Task<DialogTurnResult> PersonalInfoStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((string)stepContext.Result == "����")
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"�����մϴ�! ���࿡ �ʿ��� ���������� �Է����ּ���."), cancellationToken);

                var cardJson = File.ReadAllText("./Cards/personalInfoCard.json");
                var cardAttachment = new Attachment()
                {
                    ContentType = "application/vnd.microsoft.card.adaptive",
                    Content = JsonConvert.DeserializeObject(cardJson),
                };

                var opts = new PromptOptions
                {
                    Prompt = new Activity
                    {
                        Attachments = new List<Attachment>() { cardAttachment },
                        Type = ActivityTypes.Message,
                    }
                };

                return await stepContext.PromptAsync(AdaptivePromptId, opts, cancellationToken);
            }
            else
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"�˼��մϴ�. ��� ������ �����ø� ������ �����Ͻ� �� �����."), cancellationToken);
                return await stepContext.EndDialogAsync(null, cancellationToken);
            }
        }

        private async Task<DialogTurnResult> HouseChoiceStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string json = @$"{stepContext.Result}";
            JObject jobj = JObject.Parse(json);

            stepContext.Values["agree"] = jobj["agree"].ToString();

            if ((string)stepContext.Values["agree"] == "false")
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"�˼��մϴ�. �������� ���� �� �̿뿡 �������� �����ø� ������ ������ �� �����."), cancellationToken);
                return await stepContext.EndDialogAsync(null, cancellationToken);
            }

            stepContext.Values["name"] = jobj["name"].ToString();
            stepContext.Values["sex"] = jobj["sex"].ToString();
            stepContext.Values["age"] = jobj["age"].ToString();
            stepContext.Values["phone"] = jobj["phone"].ToString();

            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"{(string)stepContext.Values["name"]}���� ������ �������� ������ �Ϸ�Ǿ����ϴ�."), cancellationToken);

            // Create the Adaptive Card
            var cardJson = File.ReadAllText("./Cards/houseCard.json");
            var cardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(cardJson),
            };

            // Create the text prompt
            var opts = new PromptOptions
            {
                Prompt = new Activity
                {
                    Attachments = new List<Attachment>() { cardAttachment },
                    Type = ActivityTypes.Message,
                }
            };

            // Display a Text Prompt and wait for input
            return await stepContext.PromptAsync(AdaptivePromptId, opts, cancellationToken);
        }

        /*
        private async Task<DialogTurnResult> Center1StepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string json = @$"{stepContext.Result}";
            JObject jobj = JObject.Parse(json);

            stepContext.Values["agree"] = jobj["agree"].ToString();

        }
        */

        private async Task<DialogTurnResult> DateStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            //await stepContext.Context.SendActivityAsync($"{stepContext.Result}"); //������ Ȯ�� ��¿�

            string json = @$"{stepContext.Result}";
            JObject jobj = JObject.Parse(json);

            stepContext.Values["center"] = jobj["center"].ToString();

            // Create the Adaptive Card
            var cardJson = File.ReadAllText("./Cards/dateCard.json");
            var cardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(cardJson),
            };

            // Create the text prompt
            var opts = new PromptOptions
            {
                Prompt = new Activity
                {
                    Attachments = new List<Attachment>() { cardAttachment },
                    Type = ActivityTypes.Message,
                }
            };

            // Display a Text Prompt and wait for input
            return await stepContext.PromptAsync(AdaptivePromptId, opts, cancellationToken);
        }

        private async Task<DialogTurnResult> TimeStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string json = @$"{stepContext.Result}";
            JObject jobj = JObject.Parse(json);

            stepContext.Values["date"] = jobj["date"].ToString();

            // Create the Adaptive Card
            var cardJson = File.ReadAllText("./Cards/timeCard.json");
            var cardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(cardJson),
            };

            // Create the text prompt
            var opts = new PromptOptions
            {
                Prompt = new Activity
                {
                    Attachments = new List<Attachment>() { cardAttachment },
                    Type = ActivityTypes.Message,
                }
            };

            // Display a Text Prompt and wait for input
            return await stepContext.PromptAsync(AdaptivePromptId, opts, cancellationToken);
        }

        private async Task<DialogTurnResult> ConfirmStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {   
            string json = @$"{stepContext.Result}";
            JObject jobj = JObject.Parse(json);
            
            stepContext.Values["time"] = jobj["time"].ToString();

            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"���� ������ Ȯ���Ͻ� �� ������ Ȯ�����ּ���."), cancellationToken);

            var choices = new[] { "����Ȯ��"};
            var card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0))
            {
                Actions = choices.Select(choice => new AdaptiveSubmitAction
                {
                    Title = choice,
                    Data = choice,
                }).ToList<AdaptiveAction>(),
            };

            card.Body.Add(new AdaptiveTextBlock()
            {
                Text = $"{(string)stepContext.Values["name"]}���� ��������",
                Size = AdaptiveTextSize.Medium,
                Color = AdaptiveTextColor.Accent,
                Weight = AdaptiveTextWeight.Bolder 
            });

            card.Body.Add(new AdaptiveFactSet()
            {
                Spacing = AdaptiveSpacing.Medium,
                Facts = new List<AdaptiveFact>()
                {
                    new AdaptiveFact()
                    {
                    Title = "�̸�",
                    Value = $"{(string)stepContext.Values["name"]}"
                    },
                    new AdaptiveFact()
                    {
                    Title = "����",
                    Value = $"{(string)stepContext.Values["sex"]}"
                    },
                    new AdaptiveFact()
                    {
                    Title = "����",
                    Value = $"{Convert.ToInt32(stepContext.Values["age"])}"
                    },
                    new AdaptiveFact()
                    {
                    Title = "����ó",
                    Value = $"{(string)stepContext.Values["phone"]}"
                    },
                    new AdaptiveFact()
                    {
                    Title = "��������",
                    Value = $"{(string)stepContext.Values["center"]}"
                    },
                    new AdaptiveFact()
                    {
                    Title = "��¥",
                    Value = $"{(string)stepContext.Values["date"]}"
                    },
                    new AdaptiveFact()
                    {
                    Title = "�ð�",
                    Value = $"{(string)stepContext.Values["time"]}"
                    }
                }
            });

            card.Body.Add(new AdaptiveTextBlock()
            {
                Text = "��",
                Size = AdaptiveTextSize.Medium
            });

            return await stepContext.PromptAsync(
                nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = (Activity)MessageFactory.Attachment(new Attachment
                    {
                        ContentType = AdaptiveCard.ContentType,
                        Content = JObject.FromObject(card),
                    }),
                    Choices = ChoiceFactory.ToChoices(choices),
                    Style = ListStyle.None,
                },
                cancellationToken); ;
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (((FoundChoice)stepContext.Result).Value == "����Ȯ��")
            {
                var bookingDetails = (BookingDetails)stepContext.Options;

                bookingDetails.Name = (string)stepContext.Values["name"];
                bookingDetails.Sex = (string)stepContext.Values["sex"];
                bookingDetails.Age = Convert.ToInt32(stepContext.Values["age"]);
                bookingDetails.Phone = (string)stepContext.Values["phone"];
                bookingDetails.Center = (string)stepContext.Values["center"];
                bookingDetails.Date = (string)stepContext.Values["date"];
                bookingDetails.Time = (string)stepContext.Values["time"];

                return await stepContext.EndDialogAsync(bookingDetails, cancellationToken);
            }
            else
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"�˼��մϴ�. �ٽ� �������ּ���."), cancellationToken);
                return await stepContext.EndDialogAsync(null, cancellationToken);
            }
        }

    }

}
