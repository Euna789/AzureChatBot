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
using Microsoft.Recognizers.Text.DataTypes.TimexExpression;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AdaptiveCards;

using Bot.AdaptiveCard.Prompt;

namespace Pibot.Dialogs
{
    public class BookingDialog : ComponentDialog
    {
        private readonly IStatePropertyAccessor<UserProfile> _userProfileAccessor;
        static string AdaptivePromptId = "adaptive";

        public BookingDialog(UserState userState)
            : base(nameof(BookingDialog))
        {
            _userProfileAccessor = userState.CreateProperty<UserProfile>("UserProfile");

            AddDialog(new AdaptiveCardPrompt(AdaptivePromptId));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new NumberPrompt<int>(nameof(NumberPrompt<int>), AgePromptValidatorAsync));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new DateResolverDialog());
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                NotesStepAsync,
                CheckStepAsync,
                NameStepAsync,
                SexStepAsync,
                AgeStepAsync,
                PhoneStepAsync,
                HouseChoiceStepAsync,
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
                              "������ �Ϸ��� �ڰ������� ��� �����ؾ� �մϴ�.\r\n" +
                              "�ڰ������� �Ĳ��� �о�ƿ�.\r\n" +
                              "��� ������ ������ ������ �ڰ������� Ȯ���غ�����.\r\n" +
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
                              "��ݻ����� �ϳ��� �ִٸ� ������ �����Ͻ� �� �����.\r\n" +
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

        private async Task<DialogTurnResult> NameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((string)stepContext.Result == "����")
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"�����մϴ�! ���� ������ �����ҰԿ�."), cancellationToken);
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("�̸��� �Է����ּ���.") }, cancellationToken);
            }
            else
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"�˼��մϴ�. ��� ������ �����ø� ������ �����Ͻ� �� �����."), cancellationToken);
                return await stepContext.EndDialogAsync(null, cancellationToken);
            }
        }

        
        private async Task<DialogTurnResult> SexStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["name"] = (string)stepContext.Result;

            var choices = new[] { "����", "����" };
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
                Text = "������ �������ּ���.",
                Size = AdaptiveTextSize.Default
            }) ;

            // Prompt
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

        private async Task<DialogTurnResult> AgeStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["sex"] = ((FoundChoice)stepContext.Result).Value;

            var promptOptions = new PromptOptions
            {
                Prompt = MessageFactory.Text("���̸� �Է����ּ���."),
                RetryPrompt = MessageFactory.Text("�ٽ� �Է����ּ���."),
            };
            return await stepContext.PromptAsync(nameof(NumberPrompt<int>), promptOptions, cancellationToken);
        }

        private static Task<bool> AgePromptValidatorAsync(PromptValidatorContext<int> promptContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(promptContext.Recognized.Succeeded && promptContext.Recognized.Value > 0);
        }

        private async Task<DialogTurnResult> PhoneStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((int)stepContext.Result >= 16 && (int)stepContext.Result <= 69)
            {
                stepContext.Values["age"] = (int)stepContext.Result;
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("����ó�� �Է����ּ���.") }, cancellationToken);
            }
            else
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"�˼��մϴ�. ���̰� 16�� �̸�, 69�� �ʰ��� ��� ������ �Ͻ� �� �����ϴ�."), cancellationToken);
                return await stepContext.EndDialogAsync(null, cancellationToken);
            }
        }

        private async Task<DialogTurnResult> HouseChoiceStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["phone"] = (string)stepContext.Result;

            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"���������� �������ּ���."), cancellationToken);

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


        private async Task<DialogTurnResult> DateStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            //await stepContext.Context.SendActivityAsync($"{stepContext.Result}"); //������ Ȯ�� ��¿�

            string json = @$"{stepContext.Result}";
            JObject jobj = JObject.Parse(json);

            stepContext.Values["house"] = jobj["center"].ToString();

            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"��¥�� �������ּ���."), cancellationToken);

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

            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"�ð��� �������ּ���."), cancellationToken);

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

            var userProfile = await _userProfileAccessor.GetAsync(stepContext.Context, () => new UserProfile(), cancellationToken);
            var bookingDetails = (BookingDetails)stepContext.Options;

            userProfile.Name = (string)stepContext.Values["name"];
            userProfile.Sex = (string)stepContext.Values["sex"];
            userProfile.Age = (int)stepContext.Values["age"];
            userProfile.Phone = (string)stepContext.Values["phone"];
            bookingDetails.House = (string)stepContext.Values["house"];
            bookingDetails.Date = (string)stepContext.Values["date"];
            bookingDetails.Time = (string)stepContext.Values["time"];

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
                Text = $"{userProfile.Name}���� ��������",
                Size = AdaptiveTextSize.Medium
            });

            card.Body.Add(new AdaptiveTextBlock()
            {
                Text = "��\r\n" +
                $"- �̸� : {userProfile.Name}\r\n" +
                $"- ���� : {userProfile.Sex}\r\n" +
                $"- ���� : {userProfile.Age}\r\n" +
                $"- ����ó : {userProfile.Phone}\r\n" +
                $"- �������� : {bookingDetails.House}\r\n" +
                $"- ��¥ : {bookingDetails.Date}\r\n" +
                $"- �ð� : {bookingDetails.Time}\r\n" +
                "��\r\n",
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

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (((FoundChoice)stepContext.Result).Value == "����Ȯ��")
            {
                var bookingDetails = (BookingDetails)stepContext.Options;
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
