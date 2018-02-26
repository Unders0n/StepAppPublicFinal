using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;

namespace StepApp.BotExtensions.ActivityExtensions
{
    public static class ActivityExtensions
    {


        /// <summary>
        /// Create blank proactive direct message from bot to user (to create dialog for example)
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public static async Task<IMessageActivity> CreateFakeProactiveDirectMessage(this IActivity activity, string userSlackId = "", string userSlackName = "")
        {
            //filter service calls
            if (activity.From.Name == null)
            {
                return null;
            }
            

            var connector = new ConnectorClient(new Uri(activity.ServiceUrl));

            var botChannelAccount = activity.Recipient;
            var userChannelAccount = string.IsNullOrWhiteSpace(userSlackId) ? activity.From : new ChannelAccount(userSlackId, userSlackName);

            MicrosoftAppCredentials.TrustServiceUrl(activity.ServiceUrl);
            // Create Conversation


            var conversation = await connector.Conversations.CreateDirectConversationAsync(botChannelAccount,
                userChannelAccount);

            
            var fakeMessage = Activity.CreateMessageActivity();
            fakeMessage.From = userChannelAccount;
            fakeMessage.Recipient = botChannelAccount;
        //    fakeMessage.ChannelId = conversation.Id;
            fakeMessage.ChannelId = activity.ChannelId;
            fakeMessage.Conversation = new ConversationAccount(id: conversation.Id);

            

            fakeMessage.ServiceUrl = activity.ServiceUrl;
            fakeMessage.Id = Guid.NewGuid().ToString();
            return fakeMessage;
        }

        /// <summary>
        /// Start connversation from bot to user proactively from Dialog
        /// </summary>
        /// <param name="fakeMessage"></param>
        /// <param name="dialog"></param>
        /// <returns></returns>
        public static async Task StartConversationWithUserFromDialog(this IMessageActivity fakeMessage, IDialog<object> dialog, ResumeAfter<IMessageActivity> resumeDelegate = null, object[] customParams = null)
        {
            using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, fakeMessage))
            {
               


                var botData = scope.Resolve<IBotData>();

                await botData.LoadAsync(CancellationToken.None);

                ///extra attachments
                if (customParams != null)
                {
                    int i = 0;
                    foreach (var customParam in customParams)
                    {
                        botData.UserData.SetValue(customParam.GetType().Name, customParam);
                        i++;
                        //  context.UserData.SetValue<MyModel>("AttachmentsModel", instanceOfModel);
                    }
                    
                }

                

                //This is our dialog stack
                var task = scope.Resolve<IDialogTask>();

                //interrupt the stack. This means that we're stopping whatever conversation that is currently happening with the user
                //Then adding this stack to run and once it's finished, we will be back to the original conversation
               
                // var dialog = new WelcomePollDialog();
                 task.Call(dialog.Void<object, IMessageActivity>(), null);
              //  task.Call(dialog, null);


                await task.PollAsync(CancellationToken.None);

                //flush dialog stack
                await botData.FlushAsync(CancellationToken.None);
            }
        }

        public static async Task SaveUserDataAsync(this Activity activity, string propertyName, object data)
        {
           /* StateClient stateClient = activity.GetStateClient();
            BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
            userData.SetProperty(propertyName, data);
            await stateClient.BotState.SetUserDataAsync(activity.ChannelId, activity.From.Id, userData);*/
        }

        public static async Task GetUserDataAsync<T>(this Activity activity, string propertyName)
        {
         
         /*  var state = Conversation.Container.Resolve<IBotDataStore<BotData>>();
            state.LoadAsync().Result.Data

            StateClient stateClient = activity.GetStateClient();
            BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
            return userData.GetProperty<T>(propertyName);*/
        }
    }
}