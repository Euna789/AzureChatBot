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
                CheckStepAsync,
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

        private async Task<DialogTurnResult> NotesStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var notes = $"�� ���� ���� �� ���ǻ��� ��\r\n";
            notes += $"- 6���� �ı��� ������ �� ������, ���� ������ �Ұ��մϴ�.\r\n";
            notes += $"- �ֱ� �������װ˻翡 ���� ������ ���ѵ� �� �ֽ��ϴ�.\r\n";
            notes += $"- ����ð� ��� �� ������ ��ҵǴ� ������ �ֽʽÿ�.\r\n";
            notes += $"- ������ �� ���� �� �������������� �������� ������ �ֽʽÿ�.\r\n";
            await stepContext.Context.SendActivityAsync(MessageFactory.Text(notes), cancellationToken);

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("�������! ���� ������ �����ϱ� ����, ���ǻ����� Ȯ�����ּ���."),
                    Choices = ChoiceFactory.ToChoices(new List<string> { "�Ϸ�" }),
                }, cancellationToken);
        }

        private async Task<DialogTurnResult> CheckStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (((FoundChoice)stepContext.Result).Value == "�Ϸ�")
            {
                var activity = new Attachment[]
                {
                new HeroCard(
                    images: new CardImage[] 
                    { new CardImage() { Url = "http://drive.google.com/uc?export=view&id=1aU5SjnVy_439MoR6hrXgmpHxiP32keP7" } }
                    ).ToAttachment(),
                new HeroCard(
                    images: new CardImage[]
                    { new CardImage() { Url = "http://drive.google.com/uc?export=view&id=1aU5SjnVy_439MoR6hrXgmpHxiP32keP7" } }
                    ).ToAttachment(),
                new HeroCard(
                    images: new CardImage[]
                    { new CardImage() { Url = "http://drive.google.com/uc?export=view&id=1aU5SjnVy_439MoR6hrXgmpHxiP32keP7" } }
                    ).ToAttachment()
                };

                var reply = MessageFactory.Attachment(activity);
                reply.Attachments = activity;
                reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                await stepContext.Context.SendActivityAsync(reply, cancellationToken);

                return await stepContext.PromptAsync(nameof(ChoicePrompt),
                    new PromptOptions
                    {
                        Prompt = MessageFactory.Text("�ڰ� ������ �Ĳ��� �а� ��� ������ ������ Ȯ�����ּ���."),
                        Choices = ChoiceFactory.ToChoices(new List<string> { "����" , "����" }),
                    }, cancellationToken);
            }
            else
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"�˼��մϴ�. ���ǻ����� Ȯ������ �����ø� ������ ������ �� �����."), cancellationToken);
                return await stepContext.EndDialogAsync(null, cancellationToken);
            }
        }

        private async Task<DialogTurnResult> NameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (((FoundChoice)stepContext.Result).Value == "����")
            {
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

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
            new PromptOptions
            {
                Prompt = MessageFactory.Text("�湮�Ͻ� ������ �� ���͸� �������ּ���."),
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
            bookingResult += $"���� : {userProfile.Sex}\r\n";
            bookingResult += $"���� : {userProfile.Age}\r\n";
            bookingResult += $"����ó : {userProfile.Phone}\r\n";
            bookingResult += $"���� : {bookingDetails.House}\r\n";
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
