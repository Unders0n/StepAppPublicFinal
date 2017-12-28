using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace StepApp.BotExtensions.DialogExtensions
{
    public static class DialogContextExtensions
    {
        public static async Task PostTextMessageToAnotherChannel(this IDialogContext context, string channelId, string text)
        {
            var botaccount = context.Activity.Recipient;
            var userAccount = new ChannelAccount(channelId);
            var connector = new ConnectorClient(new Uri(context.Activity.ServiceUrl));
            var message = Activity.CreateMessageActivity();
            // Set the address-related properties in the message and send the message.
            message.From = botaccount;
            message.Recipient = userAccount;
            message.Conversation = new ConversationAccount(id: userAccount.Id);
            message.Text = text;
            // message.Locale = "en-Us";
            await connector.Conversations.SendToConversationAsync((Activity)message);
        }

        public static async Task PostTextMessageDirectlyToUser(this IDialogContext context, string channelId, string text)
        {
            // Use the data stored previously to create the required objects.
            if (!string.IsNullOrEmpty(channelId))
            {
                var userAccount = new ChannelAccount(channelId);
                var botAccount = new ChannelAccount(context.Activity.Recipient.Id);
                var connector = new ConnectorClient(new Uri(context.Activity.ServiceUrl));

          
                IMessageActivity message = Activity.CreateMessageActivity();
                /* working good without it
             * 
             * if (!string.IsNullOrEmpty(conversationId) && !string.IsNullOrEmpty(channelId))
              {
                  // If conversation ID and channel ID was stored previously, use it.
                  message.ChannelId = channelId;
              }
              else
              {
                  // Conversation ID was not stored previously, so create a conversation. 
                  // Note: If the user has an existing conversation in a channel, this will likely create a new conversation window.
                  conversationId = (await connector.Conversations.CreateDirectConversationAsync(botAccount, userAccount)).Id;
              }*/

                var conversationId = (await connector.Conversations.CreateDirectConversationAsync(botAccount, userAccount)).Id;

                // Set the address-related properties in the message and send the message.
                message.From = botAccount;
                message.Recipient = userAccount;
                message.Conversation = new ConversationAccount(id: conversationId);
                message.Text = text;
                message.Locale = "en-Us";
                await connector.Conversations.SendToConversationAsync((Activity)message);
            }
            else throw new Exception("No channelId for recepient presented.");
            
        }

        public static async Task PostWithButtonsAsync(this IDialogContext context, string text, List<CardAction> buttons)
        {
            var mes = context.MakeMessage();
            mes.Text = text;

            var cardForButton = new ThumbnailCard { Buttons = buttons };
            mes.Attachments.Add(cardForButton.ToAttachment());

            await context.PostAsync(mes);
        }
    }
}
