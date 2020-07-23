// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.9.2

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace Pibot.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        protected readonly ILogger Logger;

        // Dependency injection uses this constructor to instantiate MainDialog
        public MainDialog(BookingDialog bookingDialog, CheckAndCancelDialog checkAndCancelDialog, QnaDialog qnaDialog, QuizDialog quizDialog, ILogger<MainDialog> logger, UserState userState)
            : base(nameof(MainDialog))
        {
            Logger = logger;

            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(bookingDialog);
            AddDialog(checkAndCancelDialog);
            AddDialog(qnaDialog);
            AddDialog(quizDialog);
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                IntroStepAsync,
                ActStepAsync,
                FinalStepAsync,
                ReturnStepAsync
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> IntroStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var card = new HeroCard
            {
                Images = new List<CardImage> { new CardImage("http://drive.google.com/uc?export=view&id=1wU1TiDkOX54c_aeYEnOjNAzb0MB6JdoI") },
                Buttons = new List<CardAction>()
                {
                    new CardAction(ActionTypes.ImBack, title: "���� �����ϱ�", value: "���� �����ϱ�"),
                    new CardAction(ActionTypes.ImBack, title: "���� Ȯ�Ρ����", value: "���� Ȯ�Ρ����"),
                    new CardAction(ActionTypes.ImBack, title: "QnA", value: "QnA"),
                    new CardAction(ActionTypes.ImBack, title: "QUIZ", value: "QUIZ")
                },
            };

            var attachments = new List<Attachment>();
            var reply = MessageFactory.Attachment(attachments);
            reply.Attachments.Add(card.ToAttachment());
            await stepContext.Context.SendActivityAsync(reply, cancellationToken);

            var messageText = "";
            var promptMessage = MessageFactory.Text(messageText, messageText, InputHints.ExpectingInput);
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
        }

        private async Task<DialogTurnResult> ActStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((string)stepContext.Result == "���� �����ϱ�")
                return await stepContext.BeginDialogAsync(nameof(BookingDialog), new BookingDetails(), cancellationToken);
            else if ((string)stepContext.Result == "���� Ȯ�Ρ����")
                return await stepContext.BeginDialogAsync(nameof(CheckAndCancelDialog), null, cancellationToken);
            else if ((string)stepContext.Result == "QUIZ")
                return await stepContext.BeginDialogAsync(nameof(QuizDialog), null, cancellationToken);
            else
            {
                var msg = "������ ���� �ñ��Ͻ� ���� �˷��帱�Կ�!\r\n" +
                          "������ ���� �Է��غ�����.\r\n" +
                          "- ������ �� ��ð� �˷���.\r\n" +
                          "- ���帧 ġ���� ���� ���ε� ������ �� ������?\r\n" +
                          "- �����Ϸ� �� �� �� �ʿ���?\r\n" +
                          "- ���� �������� / ������ �������� �˷���\r\n"+
                          "�� �׸��Ͻ÷��� '����'�� �Է��ϼ���.\r\n";
                await stepContext.Context.SendActivityAsync(MessageFactory.Text(msg), cancellationToken);
                return await stepContext.BeginDialogAsync(nameof(QnaDialog), null, cancellationToken);
            }
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Result is BookingDetails result)
                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"{result.Name}���� ������ ���������� �����Ǿ����ϴ�. �����մϴ�!"), cancellationToken);

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                    Choices = ChoiceFactory.ToChoices(new List<string> { "ó������" }),
                }, cancellationToken);
        }

        private async Task<DialogTurnResult> ReturnStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.ReplaceDialogAsync(InitialDialogId, null, cancellationToken);
        }
    }
}