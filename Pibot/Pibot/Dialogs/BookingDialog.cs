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

namespace Pibot.Dialogs
{
    public class BookingDialog : ComponentDialog
    {
        private readonly IStatePropertyAccessor<UserProfile> _userProfileAccessor;

        public BookingDialog(UserState userState)
            : base(nameof(BookingDialog))
        {
            _userProfileAccessor = userState.CreateProperty<UserProfile>("UserProfile");

            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new NumberPrompt<int>(nameof(NumberPrompt<int>), AgePromptValidatorAsync));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new DateResolverDialog());
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                NotesStepAsync,
                NameStepAsync,
                SexStepAsync,
                AgeStepAsync,
                PhoneStepAsync,
                HouseChoiceStepAsync,
                DateStepAsync,
                ConfirmStepAsync,
                FinalStepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private Attachment CreateAdaptiveCardAttachment()
        {
            var cardResourcePath = GetType().Assembly.GetManifestResourceNames().First(name => name.EndsWith("welcomeCard.json"));

            using (var stream = GetType().Assembly.GetManifestResourceStream(cardResourcePath))
            {
                using (var reader = new StreamReader(stream))
                {
                    var adaptiveCard = reader.ReadToEnd();
                    return new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive",
                        Content = JsonConvert.DeserializeObject(adaptiveCard),
                    };
                }
            }
        }

        private async Task<DialogTurnResult> NotesStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var notes = $"�� ���� ���� �� ���ǻ��� ��{System.Environment.NewLine}";
            notes += $"- 6���� �ı��� ������ �� ������, ���� ������ �Ұ��մϴ�.{System.Environment.NewLine}";
            notes += $"- �ֱ� �������װ˻翡 ���� ������ ���ѵ� �� �ֽ��ϴ�.{System.Environment.NewLine}";
            notes += $"- ����ð� ��� �� ������ ��ҵǴ� ������ �ֽʽÿ�.{System.Environment.NewLine}";
            notes += $"- ������ �� ���� �� �������������� �������� ������ �ֽʽÿ�.{System.Environment.NewLine}";
            await stepContext.Context.SendActivityAsync(MessageFactory.Text(notes), cancellationToken);

            var activity = new Attachment[]
                      {
                                        new HeroCard(
                                            title: "��ü����",
                                            images: new CardImage[] { new CardImage() { Url = "https://www.bloodinfo.net/image/character_img.png" } },
                                            buttons: new CardAction[]
                                            {
                                                new CardAction(title: "����", type: ActionTypes.OpenUrl, value: "https://www.bloodinfo.net/image/character_img.png")
                                            })
                                        .ToAttachment(),
                                        new HeroCard(
                                            title: "�๰",
                                            images: new CardImage[] { new CardImage() { Url = "https://www.bloodinfo.net/image/character_img.png" } },
                                            buttons: new CardAction[]
                                            {
                                                new CardAction(title: "����", type: ActionTypes.OpenUrl, value: "https://www.bloodinfo.net/image/character_img.png")
                                            })
                                        .ToAttachment(),
                                        new HeroCard(
                                            title: "��¼��",
                                            images: new CardImage[] { new CardImage() { Url = "https://www.bloodinfo.net/image/character_img.png" } },
                                            buttons: new CardAction[]
                                            {
                                                new CardAction(title: "����", type: ActionTypes.OpenUrl, value: "https://www.bloodinfo.net/image/character_img.png")
                                            })
                                        .ToAttachment()
                      };
            var reply = MessageFactory.Attachment(activity);
            reply.Attachments = activity;
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            await stepContext.Context.SendActivityAsync(reply, cancellationToken);

            return await stepContext.PromptAsync(nameof(ConfirmPrompt), new PromptOptions { Prompt = MessageFactory.Text("���ǻ��� Ȯ�� �� �ڰ� ������ �Ϸ��ϼ̳���?") }, cancellationToken);
        }

        private async Task<DialogTurnResult> NameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((bool)stepContext.Result)
            {
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("�̸��� �Է����ּ���.") }, cancellationToken);
            }
            else
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"���ǻ��� Ȯ�� �� �ڰ� ������ �Ϸ����� �����ø� ������ ������ �� �����ϴ�."), cancellationToken);
                return await stepContext.BeginDialogAsync(nameof(BookingDialog), new BookingDetails(), cancellationToken);
            }
        }

        private async Task<DialogTurnResult> SexStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["name"] = (string)stepContext.Result;

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("������ �������ּ���."),
                    Choices = ChoiceFactory.ToChoices(new List<string> { "����", "����" }),
                }, cancellationToken);
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
            return Task.FromResult(promptContext.Recognized.Succeeded && promptContext.Recognized.Value > 0 && promptContext.Recognized.Value < 150);
        }

        private async Task<DialogTurnResult> PhoneStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["age"] = (int)stepContext.Result;
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("����ó�� �Է����ּ���.") }, cancellationToken);
        }

        private async Task<DialogTurnResult> HouseChoiceStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["phone"] = (string)stepContext.Result;

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
            new PromptOptions
            {
                Prompt = MessageFactory.Text("��� �������� �湮�Ͻðھ��?"),
                Choices = ChoiceFactory.ToChoices(new List<string> { "������", "ȫ����", "������" }),
            }, cancellationToken);
        }
    

        private async Task<DialogTurnResult> DateStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["house"] = ((FoundChoice)stepContext.Result).Value;

            var bookingDetails = (BookingDetails)stepContext.Options;
            return await stepContext.BeginDialogAsync(nameof(DateResolverDialog), bookingDetails.Date, cancellationToken);
        }

        private async Task<DialogTurnResult> ConfirmStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["date"] = (string)stepContext.Result;

            var userProfile = await _userProfileAccessor.GetAsync(stepContext.Context, () => new UserProfile(), cancellationToken);
            var bookingDetails = (BookingDetails)stepContext.Options;

            userProfile.Name = (string)stepContext.Values["name"];
            userProfile.Sex = (string)stepContext.Values["sex"];
            userProfile.Age = (int)stepContext.Values["age"];
            userProfile.Phone = (string)stepContext.Values["phone"];
            bookingDetails.House = (string)stepContext.Values["house"];
            bookingDetails.Date = (string)stepContext.Values["date"];

            var bookingResult = $"{userProfile.Name}���� ���� �����Դϴ�.{System.Environment.NewLine}";
            bookingResult += $"���� : {userProfile.Sex}{System.Environment.NewLine}";
            bookingResult += $"���� : {userProfile.Age}{System.Environment.NewLine}";
            bookingResult += $"����ó : {userProfile.Phone}{System.Environment.NewLine}";
            bookingResult += $"���� : {bookingDetails.House}{System.Environment.NewLine}";
            bookingResult += $"��¥ : {bookingDetails.Date}";
            await stepContext.Context.SendActivityAsync(MessageFactory.Text(bookingResult), cancellationToken);

            var msg = $"���� ������ �´��� Ȯ�����ּ���.";
            var promptMessage = MessageFactory.Text(msg, msg, InputHints.ExpectingInput);
            return await stepContext.PromptAsync(nameof(ConfirmPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((bool)stepContext.Result)
            {
                var bookingDetails = (BookingDetails)stepContext.Options;

                return await stepContext.EndDialogAsync(bookingDetails, cancellationToken);
            }

            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"������ Ȯ���Ͻð� �ٽ� �������ּ���."), cancellationToken);
            return await stepContext.EndDialogAsync(null, cancellationToken);
        }

    }


}
