ChatrBoxClient.GetMessages();
ChatrBoxClient.GetTopics();
ChatrBoxClient.GetUserStatuses();
ChatrBoxClient.GetCommunities();
ChatrBoxClient.Settings.MessageCheckRate = 5000;
ChatrBoxClient.Interval.Set(
    "messageChecker",
    ChatrBoxClient.Settings.MessageCheckRate,
    ChatrBoxClient.GetMessages
);
ChatrBoxClient.Interval.Set(
    "userStatusUpdater",
    ChatrBoxClient.Settings.StatusUpdateRate,
    ChatrBoxClient.GetUserStatuses
);