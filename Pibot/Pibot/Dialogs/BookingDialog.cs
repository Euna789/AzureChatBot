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
using System.Data.SqlClient;
using System.Text;

namespace Pibot.Dialogs
{
    public class BookingDialog : ComponentDialog
    {
        static string AdaptivePromptId = "adaptive";

        public BookingDialog(UserState userState) : base(nameof(BookingDialog))
        {
            AddDialog(new AdaptiveCardPrompt(AdaptivePromptId));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                NotesStepAsync,
                CheckStepAsync,
                PersonalInfoStepAsync,
                Center1StepAsync,
                Center2StepAsync,
                Center3StepAsync,
                DateStepAsync,
                TimeStepAsync,
                ConfirmStepAsync,
                FinalStepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
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

        private async Task<DialogTurnResult> Center1StepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
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
            var cardJson = File.ReadAllText("./Cards/center1Card.json");
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

        private async Task<DialogTurnResult> Center2StepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string json = @$"{stepContext.Result}";
            JObject jobj = JObject.Parse(json);

            stepContext.Values["center1"] = jobj["center1"].ToString();

            if ((string)stepContext.Values["center1"] == "����")
            {
                // Create the Adaptive Card
                var cardJson = File.ReadAllText("./Cards/center2_1Card.json");
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

            else
            {
                // Create the Adaptive Card
                var cardJson = File.ReadAllText("./Cards/center2_2Card.json");
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
        }

        private async Task<DialogTurnResult> Center3StepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string json = @$"{stepContext.Result}";
            JObject jobj = JObject.Parse(json);

            stepContext.Values["center2"] = jobj["center2"].ToString();

            List<Attachment> cards = new List<Attachment>();

            if ((string)stepContext.Values["center2"] == "���ϡ����������")
            {
                cards.Add(MakeMapCards("���ﵿ�����׿�", 37.64695, 127.062077, "���� ����� ���Ϸ� 1329\r\n��").ToAttachment());
                cards.Add(MakeMapCards("���طμ���", 37.654154, 127.061429, "���� ����� ���ط� 480 �������� 2��").ToAttachment());
                cards.Add(MakeMapCards("�������", 37.655998, 127.062798, "���� ����� ���� 70, ȭ������ 7��").ToAttachment());
                cards.Add(MakeMapCards("���ϼ���", 37.591916, 127.017705, "���� ���ϱ� ���ҹ��� 20�ٱ� 17, ������� 4��").ToAttachment());
                cards.Add(MakeMapCards("�����ռ���", 37.585649, 127.029497, "���� ���ϱ� ���̷�24�� 11, 2��\r\n��").ToAttachment());
                cards.Add(MakeMapCards("��������", 37.585649, 127.029497, "���� ���ϱ� ������ 325, ��������ȸ 4��").ToAttachment());
            }

            else if ((string)stepContext.Values["center2"] == "���������빮���������߶�")
            {
                cards.Add(MakeMapCards("������2����", 37.534321, 127.094419, "���� ������ �������� 50 �������͹̳γ� 1�� 114ȣ").ToAttachment());
                cards.Add(MakeMapCards("�Ǵ뿪����", 37.540734, 127.070432, "���� ������ ���Ϸ�22�� 115, 4��\r\n��").ToAttachment());
                cards.Add(MakeMapCards("ȸ�⼾��", 37.589647, 127.056803, "���� ���빮�� ȸ��� 188, �θ����� 5��").ToAttachment());
                cards.Add(MakeMapCards("�Ѿ�뿪����", 37.555806, 127.043850, "���� ������ �սʸ��� 206, ����ö2ȣ�� �Ѿ�뿪���� 209-4ȣ").ToAttachment());
                cards.Add(MakeMapCards("���쿪����", 37.598433, 127.091648, "���� �߶��� ����� 353 ��� �����̾���� C�� 302ȣ").ToAttachment());
            }

            else if ((string)stepContext.Values["center2"] == "���򡤼��빮������")
            {
                cards.Add(MakeMapCards("���ų�����", 37.619058, 126.920193, "���� ���� ���Ϸ� 855-8, ���ų���������Ÿ�� 4�� 401ȣ").ToAttachment());
                cards.Add(MakeMapCards("���̿���ռ���", 37.557314, 126.937379, "���� ���빮�� ���� 6, ���̺��� 8��").ToAttachment());
                cards.Add(MakeMapCards("���̼���", 37.555600, 126.937854, "���� ���빮�� ���̷� 107, ���κ��� 2��").ToAttachment());
                cards.Add(MakeMapCards("ȫ�뼾��", 37.555794, 126.922844, "���� ������ ��ȭ�� 152, ��ȭ���� 6��").ToAttachment());
            }

            else if ((string)stepContext.Values["center2"] == "���Ρ��߱�")
            {
                cards.Add(MakeMapCards("���зμ���", 37.583349, 127.000063, "���� ���α� ���� 26 3��").ToAttachment());
                cards.Add(MakeMapCards("��ȭ������", 37.570624, 126.979816, "���� ���α� ���� 33 �׶����� 2��").ToAttachment());
                cards.Add(MakeMapCards("���￪����", 37.557422, 126.969516, "���� �߱� û�ķ� 426").ToAttachment());
            }

            else if ((string)stepContext.Values["center2"] == "���������ġ�����������")
            {
                cards.Add(MakeMapCards("õȣ����", 37.537906, 127.126966, "���� ������ õȣ��� 1033, 8��\r\n��").ToAttachment());
                cards.Add(MakeMapCards("��������", 37.501315, 127.025498, "���� ���ʱ� ������� 437, 7��\r\n��").ToAttachment());
                cards.Add(MakeMapCards("���ﳲ�μ���", 37.481938, 127.048888, "����Ư���� ������ ������31�� 48\r\n��").ToAttachment());
                cards.Add(MakeMapCards("����2����", 37.496620, 127.028661, "���� ������ ���ﵿ ������� 378, 9��").ToAttachment());
                cards.Add(MakeMapCards("�ڿ�������", 37.511176, 127.059911, "���� ������ ������� 524, �ڿ�����R7").ToAttachment());
                cards.Add(MakeMapCards("��ǿ�����", 37.512501, 127.101131, "���� ���ı� �ø��ȷ� 240(���ϱ���7ȣ)").ToAttachment());
            }

            else if ((string)stepContext.Values["center2"] == "���ۡ����ǡ�������")
            {
                cards.Add(MakeMapCards("�̼�����", 37.486312, 126.981671, "���� ���۱� ���۴�� 109, �湮���� 3��").ToAttachment());
                cards.Add(MakeMapCards("�뷮��������", 37.513336, 126.942966, "���� ���۱� �뷮���� 154\r\n��").ToAttachment());
                cards.Add(MakeMapCards("����뿪����", 37.478676, 126.952557, "���� ���Ǳ� ���Ƿ� 152, 2��\r\n��").ToAttachment());
                cards.Add(MakeMapCards("������б�����", 37.463488, 126.949454, "���� ���Ǳ� ���Ƿ� 1, ������б� 67�� �η������� 103-1ȣ").ToAttachment());
                cards.Add(MakeMapCards("��濪����", 37.513289, 126.926285, "���� �������� ���Ǵ��� 300\r\n��").ToAttachment());
                cards.Add(MakeMapCards("����������", 37.516723, 126.906099, "���� �������� ���߷� 3\r\n��").ToAttachment());
            }

            else if ((string)stepContext.Values["center2"] == "��������õ������")
            {
                cards.Add(MakeMapCards("�����߾����׿�", 37.548028, 126.870858, "���� ������ ���״�� 591 ���������ڻ� �����߾����׿� 3��").ToAttachment());
                cards.Add(MakeMapCards("����꿪����", 37.547842, 126.835823, "���� ������ ������45�� 5, 2��\r\n��").ToAttachment());
                cards.Add(MakeMapCards("�߻꿪����", 37.559783, 126.838418, "���� ������ ������ 385, �켺SBŸ�� 507ȣ").ToAttachment());
                cards.Add(MakeMapCards("�񵿼���", 37.528160, 126.875776, "���� ��õ�� �񵿵��� 293, ����41Ÿ�� ����1�� B-02ȣ").ToAttachment());
                cards.Add(MakeMapCards("���ε����д���������", 37.485144, 126.901529, "���� ���α� ����õ�� 477\r\n��").ToAttachment());
                cards.Add(MakeMapCards("�ŵ�����ũ�븶Ʈ����", 37.507018, 126.890208, "���� ���α� ������ 97, �ŵ�����ũ�븶Ʈ ���ϱ���").ToAttachment());
            }

            else if ((string)stepContext.Values["center2"] == "����������")
            {
                cards.Add(MakeMapCards("��ž����", 37.411617, 127.127596, "��⵵ ������ �д籸 ��ž�� 69���� 24-8, 3��").ToAttachment());
                cards.Add(MakeMapCards("��������", 37.384133, 127.121495, "��⵵ ������ �д籸 �д�� 53���� 11, �������� 4��").ToAttachment());
                cards.Add(MakeMapCards("������׿�", 37.259395, 127.030294, "��⵵ ������ �Ǽ��� �Ǳ��� 129\r\n��").ToAttachment());
                cards.Add(MakeMapCards("������û������", 37.263872, 127.03227, "��� ������ �ȴޱ� �Ǳ��� 181 ������ũ 2�� 204ȣ").ToAttachment());
                cards.Add(MakeMapCards("����������", 37.266101, 127.001606, "��⵵ ������ �ȴޱ� ������� 923-1, 4��").ToAttachment());
            }

            else if ((string)stepContext.Values["center2"] == "�Ⱦ硤�������Ȼ�")
            {
                cards.Add(MakeMapCards("���̼���", 37.389693, 126.95106, "��⵵ �Ⱦ�� ���ȱ� ���ȷ� 130\r\n��").ToAttachment());
                cards.Add(MakeMapCards("�Ⱦ缾��", 37.400566, 126.921963, "��⵵ �Ⱦ�� ���ȱ� ���ȷ� 223���� 13, 3��").ToAttachment());
                cards.Add(MakeMapCards("�꺻����", 37.359666, 126.932064, "��⵵ ������ �꺻�� 323���� 16-14, 3��").ToAttachment());
                cards.Add(MakeMapCards("�Ѵ�տ�����", 37.309073, 126.853535, "��� �Ȼ�� ��ϱ� ����4�� 391, ���������� ��").ToAttachment());
                cards.Add(MakeMapCards("��ź����", 37.206879, 127.073016, "��⵵ ȭ���� ��ź�ݼ��� 204 ��ź���������� 205ȣ").ToAttachment());
            }

            else //ȭ�������Ρ�����
            {
                cards.Add(MakeMapCards("���μ���", 37.234822, 127.203591, "��⵵ ���ν� ó�α� �ݷɷ� 64, 201, 202ȣ").ToAttachment());
                cards.Add(MakeMapCards("��������", 37.323712, 127.096447, "��⵵ ���ν� ������ ǳ��õ�� 133 �ݿ��ö��� 4��").ToAttachment());
                cards.Add(MakeMapCards("���ÿ�����", 36.990951, 127.08673, "��⵵ ���ý� ���÷� 39���� 36, 2��").ToAttachment());
            }

            var reply = MessageFactory.Attachment(cards);
            reply.Attachments = cards;
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            await stepContext.Context.SendActivityAsync(reply, cancellationToken);

            var messageText = "";
            var promptMessage = MessageFactory.Text(messageText, messageText, InputHints.ExpectingInput);
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
        }

        private static HeroCard MakeMapCards(string name, double latitude, double longitude, string address)
        {
            HeroCard card = new HeroCard(
                            title: name,
                            images: new CardImage[] { new CardImage() { Url = MakeMap(latitude, longitude) } },
		tap: new CardAction() { Value = $"http://maps.google.com/maps?q={ latitude},{longitude}", Type = "openUrl", },
                            text: address,
                            buttons: new List<CardAction>
                            {new CardAction(ActionTypes.PostBack, title: "����", value: name)}
                            );

            return card;
        }

        private static String MakeMap(double latitude, double longitude)
        {
            string latitudeStr = latitude.ToString();
            string longitudeStr = longitude.ToString();
            return $"http://maps.google.com/maps/api/staticmap?center={ latitudeStr },{ longitudeStr}&zoom=16&size=512x512&maptype=roadmap&markers=color:red%7C{ latitudeStr },{ longitudeStr }&sensor=false&key=AIzaSyCUGkyf6nzMobitlUprUzDNIqb2GTDj2lk";
        }

        private async Task<DialogTurnResult> DateStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["center"] = (string)stepContext.Result;

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

            string datetime = stepContext.Values["date"] + "T" + stepContext.Values["time"] + ":00+09:00";

            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"���� ������ Ȯ���Ͻ� �� ������ Ȯ�����ּ���."), cancellationToken);

            var choices = new[] { "����Ȯ��" };
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
                    Value = "{{DATE("+$"{datetime}"+", SHORT)}}"
                    },
                    new AdaptiveFact()
                    {
                    Title = "�ð�",
                    Value = "{{TIME("+$"{datetime}"+")}}"
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
                string Datetimestr = bookingDetails.Date + " " + bookingDetails.Time;

                try
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                    builder.DataSource = "team19-server.database.windows.net";
                    builder.UserID = "chatbot19";
                    builder.Password = "presnacks2020!";
                    builder.InitialCatalog = "pibotDB";

                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {
                        Console.WriteLine("\nQuery data example:");
                        Console.WriteLine("=========================================\n");

                        connection.Open();
                        StringBuilder sb = new StringBuilder();
                        //Submit_Tsql_NonQuery(connection, "2 - Create-Tables", Build_2_Tsql_CreateTables()); //���̺� �ʱ�ȭ
                        Submit_Tsql_NonQuery(connection, "3 - Inserts", $@"INSERT INTO reservInfo(Name, Sex, Age, Phone, reserv_Date, Center) VALUES('{ bookingDetails.Name}','{ bookingDetails.Sex}',{ bookingDetails.Age},'{ bookingDetails.Phone}','{Datetimestr}','{ bookingDetails.Center}');");
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }

                return await stepContext.EndDialogAsync(bookingDetails, cancellationToken);
            }

            else
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"�˼��մϴ�. ������ Ȯ������ �ʾҾ��. �ٽ� �������ּ���."), cancellationToken);
                return await stepContext.EndDialogAsync(null, cancellationToken);
            }
        }

        static string Build_2_Tsql_CreateTables()
        {
            return @"DROP TABLE IF EXISTS reservInfo;
                    CREATE TABLE reservInfo
                    (
                        reserv_id  int  identity(1,1)   not null    PRIMARY KEY,
                        Name  nvarchar(128)     not null,
                        Sex nvarCHAR(30), 
                        Age INT, 
                        Phone nvarCHAR(50) not null , 
                        reserv_date DateTime, 
                        Center nVARCHAR(255)
                    );
            ";
        }

        static void Submit_Tsql_NonQuery(SqlConnection connection, string tsqlPurpose, string tsqlSourceCode, string parameterName = null, string parameterValue = null)
        {
            Console.WriteLine();
            Console.WriteLine("=================================");
            Console.WriteLine("T-SQL to {0}...", tsqlPurpose);

            using (var command = new SqlCommand(tsqlSourceCode, connection))
            {
                if (parameterName != null)
                {
                    command.Parameters.AddWithValue(  // Or, use SqlParameter class.
                        parameterName,
                        parameterValue);
                }
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine(rowsAffected + " = rows affected.");
            }
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

    }

}
