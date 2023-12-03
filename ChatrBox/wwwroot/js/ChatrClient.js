class ChatrBoxClient {
    //UserId of the current ChatrBox User
    static UserId;

    //Username of the current ChatrBox User
    static Username;

    static APIRoute = "/API/"

    /**
     * Defines how the LogActivity method behaves during runtime
     */
    static LoggingBehavior = {
        /**
         *  Send activity log information to the console for local debugging
         */
        LogToConsole: false,

        /**
         * Send logs to the server for global debugging across client instances
         */
        SendToServer: true,

        /**
         * Save logs locally to the users system
         */
        SendToLocalFolder: false,

        /**
         * Sends all log messages to enabled logs, with the exception of global logs.
         */
        EnableGranular: false,

        /**
         * Path to the folder where local logs will be saved
         */
        LocalLogs: ""
    }

    /**
     * Stores the client-side settings for this client
     */
    static Settings = {

        /**
         * Id of topic currently being viewed
         */
        TopicId: 0,

        /**
         * Id of community currently being viewed
         */
        CommunityId: 0,

        /**
         * How often the client should check the server for new messages (milliseconds)
         */
        MessageCheckRate: 0,

        /**
         * How often the client should check the server for online status updates
         */
        StatusUpdateRate: 0,

        /**
         * Most recent post in the current community/topic
         */
        LastPost: Date.now()
    }

    static SetUserDetails(name, id) {
        ChatrBoxClient.Settings.UserId = id;
        ChatrBoxClient.Settings.Username = name;

        return ChatrBoxClient
    }
    static SetCommunity(commnunityId) {
        ChatrBoxClient.Settings.CommunityId = commnunityId;
        return ChatrBoxClient;
    }

    static SetTopic(topicId) {
        ChatrBoxClient.Settings.TopicId = topicId;
        return ChatrBoxClient;
    }

    static InitializeClient(communityId, topicId, userId, username, debugMode = false) {
        if (debugMode) {
            ChatrBoxClient.LoggingBehavior.EnableGranular = true;
            ChatrBoxClient.LoggingBehavior.LogToConsole = true;
            ChatrBoxClient.LogActivity(`User: ${ChatrBoxClient.Username} initialized client in debugMode.`);
        }

        ChatrBoxClient.SetUserDetails(username, userId)
            .SetCommunity(communityId)
            .GetTopics()
            .GetUserStatuses()
            .GetCommunities()
            .Interval.Set(
                "messageChecker",
                ChatrBoxClient.Settings.MessageCheckRate,
                ChatrBoxClient.GetMessages)
            .Interval.Set(
                "userStatusUpdater",
                ChatrBoxClient.Settings.StatusUpdateRate,
                ChatrBoxClient.GetUserStatuses);

        setTimeout(function () {
            ChatrBoxClient
                .SetTopic(topicId)
                .GetMessages()
                .UpdateTopicIdentifier();
        }, 2500);

        return ChatrBoxClient;
    }

    /**
     * Handles all functions that need to be run on a fixed schedule (wrapper for setInterval and clearInterval 
     * functionality).
     */
    static Interval = class {
        /**
         * set an action to repeat indefinately until the page is reloaded, navigated away 
         * from, or the action is disabled using the Clear method. 
         * @param key {string} A string based keyword used to recall the action handler.
         * @param time {number} A number representing the delay (in milliseconds).
         * @param handler {Function} Function that is called when the delay timer runs out.
         * @returns {boolean} True upon successful configuration. 
         */
        static Set(key, time, handler) {
            if (typeof (handler) !== "function") {
                return false;
            }

            let interval = setInterval(handler, time);
            this.keys.push(new DictionaryEntry(key, interval));

            ChatrBoxClient.LogActivity(`User ${ChatrBoxClient.Username} configured ChatrBoxClient.Interval with key:${key} and delay of ${time} milliseconds`);

            return ChatrBoxClient;
        }

        //locate and stop an action timer. key: name of the timer to recall. Returns 
        //true if successful
        static Clear(key) {
            for (index = 0; index < this.keys.length; index++) {

                //locate the key in keys
                if (this.keys[i].key === key) {

                    //clear the key
                    clearInterval(this.keys[i].value);

                    //remove key entry from keys
                    this.keys = this.keys.splice(index, 1);

                    //return true to indicate success
                    return ChatrBoxClient;
                }
            }

            //this indicates the interval key was not found
            return false;
        }

        //stores dictionary entries of all intervals
        static keys = [];
    }

    /**
     * checks for messages in the given community/topic combination
     */
    static GetMessages(force) {
        ChatrBoxClient.LogActivity("Checked Messages", true);

        if (force) ChatrBoxClient.Settings.LastPost = Date.now;

        $.get(ChatrBoxClient.APIRoute + "Communities/CheckMessages", {
            topicId: ChatrBoxClient.Settings.TopicId,
            lastPost: ChatrBoxClient.Settings.LastPost
        },
            function (data, error) {
                //capture the message window
                var msgWindow = $("#msg_window");

                //continue only if success code was sent
                if (data.error.code != undefined) {
                    if (data.error.code === 0) {
                        ChatrBoxClient.Settings.LastPost = data.lastPost;
                        msgWindow.empty();
                        msgWindow.append(data.messages);

                        if (true) ChatrBoxClient.ScrollMessages();
                    }
                    else if (data.error.code === 101) {
                        msgWindow.empty();
                        msgWindow.append("Access Denied: You do not have permission to view " +
                            "this content. Please contact the community manager to be added to " +
                            "this commnunity.");
                    }
                }

                $('.edit-msg-link').on("click", function (event) {
                    event.preventDefault();
                    var id = event.target.attributes['id'].value.replace("messageId_", "");
                    id = parseInt(id);
                    var contentSelector = `#${event.target.attributes['id'].value}_Content`;
                    $('#EditMessage').val($(contentSelector).val());
                    $('#EditMessageId').val(id);
                    $('#EditModal').dialog("open");
                });
            });

        return ChatrBoxClient;
    }

    /**
     * Requests a list of topics from the user and displays them on the client window.
     */
    static GetTopics(explicitTopic) {
        $.get(ChatrBoxClient.APIRoute + "Communities/GetTopicList", { communityId: ChatrBoxClient.Settings.CommunityId }, function (data, err) {
            if (data && data.error && data.error.code == 0) {
                $("#CommunityName").text(data.communityName);
                var topics = $('#TopicList');
                topics.empty();
                for (var i = 0; i < data.topics.length; i++) {

                    if (i === 0 && !explicitTopic) ChatrBoxClient.Settings.TopicId = data.topics[i].key;
                    else ChatrBoxClient.Settings.TopicId = explicitTopic;

                    var active = data.topics[i].key === ChatrBoxClient.Settings.TopicId ? " topic-list-active" : ""
                    var listClasses = "topic-list-item " + active;
                    topics.append("<li id=\"topicId_" + data.topics[i].key + "\" class=\"" + listClasses + "\">" + data.topics[i].value + "</li>");

                    $('#topicId_' + data.topics[i].key).on("click",
                        function (event) {

                            event.stopPropagation();
                            var temp = event.target.attributes['id'].value;
                            temp = temp.replace("topicId_", "");

                            ChatrBoxClient.LogActivity(`User clicked on topic#${temp}`, true);

                            ChatrBoxClient.Settings.TopicId = parseInt(temp);
                            ChatrBoxClient.GetMessages();

                            ChatrBoxClient.UpdateTopicIdentifier();
                        });
                }
            }
        });

        return ChatrBoxClient;
    }

    static UpdateTopicIdentifier() {
        ChatrBoxClient.LogActivity("updating topic identifier", true);
        $('.topic-list-item').each(function (index, element) {
            $(element).removeClass("topic-list-active");
        });

        $(`#topicId_${ChatrBoxClient.Settings.TopicId}`).addClass("topic-list-active");

        return ChatrBoxClient;
    }

    static GetCommunities() {
        ChatrBoxClient.LogActivity("Requesting community list", true);

        $.get(`${ChatrBoxClient.APIRoute}Communities/GetCommunityList`, function (data, err) {
            ChatrBoxClient.LogActivity(data.error.description, true);
            ChatrBoxClient.Settings.CommunityId = data.communityId;

            var communityList = $('#community_list');
            for (var i = 0; i < data.userCommunities.length; i++) {
                communityList.append(data.userCommunities[i]);
            }

            $('.community-list-btn').each(function (index, element) {
                $(element).on("click", function (event) {
                    event.stopPropagation();
                    var id = event.target.attributes['id'].value.replace("communityId_", "");
                    ChatrBoxClient.Settings.CommunityId = id;
                    ChatrBoxClient.GetTopics();
                    ChatrBoxClient.Settings.LastPost = new Date(2000, 1, 1, 0, 0, 0, 0);
                    ChatrBoxClient.GetMessages(true);
                })
            });
        });

        return ChatrBoxClient;
    }

    /**
     * Retrieves online user status from the server and updates the client display. Posts to enabled logs upon failure.
     */
    static GetUserStatuses() {

        var userWindow = $("#user_window");

        $.get(ChatrBoxClient.APIRoute + "Communities/GetUsersOnline", { communityId: ChatrBoxClient.Settings.CommunityId },
            function (data, error) {
                userWindow.empty();

                userWindow.append("<div id=\"onlineUsers\"></div>");
                $("#onlineUsers").append("<div class=\"onlineUsersHeading\">Online:</div>");
                $("#onlineUsers").append(data.usersOnline);

                userWindow.append("<div id=\"offlineUsers\"></div>");
                $("#offlineUsers").append("<div class=\"offlineUsersHeading\">Offline:</div>");
                $("#offlineUsers").append(data.usersOffline);
            });

        //perform user checkin
        $.get("/Home/UserCheckIn");


        return ChatrBoxClient;
    }

    static ScrollMessages() {
        var chat = document.getElementById("msg_window");
        chat.scrollTop = chat.scrollHeight;
    }

    /**
     * Logs client activity using the settings in LoggingBehavior
     * @param {string} actionTaken describes the action taken by the client
     * @param {boolean} granular used to prevent more granular messages log messages from cluttering log files
     */
    static LogActivity(actionTaken, granular = false) {
        var timestamp = new Date(Date.now());
        var logMsg = `ChatrBoxClient Log ${granular ? "(granular)" : ""}: ${actionTaken} at ${timestamp}`;
        if ((granular && ChatrBoxClient.LoggingBehavior.EnableGranular) ||
            !granular) {

            if (ChatrBoxClient.LoggingBehavior.LogToConsole) {
                console.log(logMsg);
            }
        }

        if (ChatrBoxClient.LoggingBehavior.SendToServer && !granular) {
            $.post("/API/Admin/Log", {
                logMsg: logMsg,
                file: "clientSide.log"
            });
        }

        return ChatrBoxClient;
    }

    /**
     * Send a message in the currently active community.
     * @param {string} msg content of the message to be sent to the server.
     */
    static PostMessage(msg) {

        $.post(ChatrBoxClient.APIRoute + "Communities/SendMessage",
            {
                topicId: ChatrBoxClient.Settings.TopicId,
                content: msg
            },
            function (data, err) {
            if (data && data.error && data.error.code == 0) {
                ChatrBoxClient.GetMessages();
            }
            else {
                ChatrBoxClient.LogActivity(`User ${ChatrBoxClient.Username}'s message failed to post in topic#${ChatrBoxClient.Settings.TopicId} with description ${data.error.description}`);
                ChatrBoxClient.DisplayBannerNotification(data.error.description, 5000, "bg-danger")
            }
        });

        return ChatrBoxClient;
    }

    static EditMessage(messageId, messageContent) {
        $.post(ChatrBoxClient.APIRoute + "Communities/EditMessage",
            {
                topicId: ChatrBoxClient.Settings.TopicId,
                messageId: messageId,
                content: messageContent
            },
            function (data, err) {
                if (data && data.error && data.error.code == 0) {
                    ChatrBoxClient.GetMessages(true);
                }
                else {
                    ChatrBoxClient.LogActivity(`User ${ChatrBoxClient.Username}'s message failed to post in topic#${ChatrBoxClient.Settings.TopicId} with description ${data.error.description}`);
                    ChatrBoxClient.DisplayBannerNotification(data.error.description, 5000, "bg-danger")
                }
            }
        );

        return ChatrBoxClient;
    }

    static DeleteMessage(messageId) {

    }

    /**
     * Displays a banner message at the top of the screen for a fixed amount of time.
     * @param {string} content body of the banner notification.
     * @param {number} time time the notification will be displayed
     * @param {any} classes css classes to apply to the banner
     * @param {any} styles inline styles to apply to the banner
     */
    static DisplayBannerNotification(content, time, classes, styles) {
        //not implemented yet


        return ChatrBoxClient;
    }
}

class DictionaryEntry {
    constructor(key, value) {
        this.key = key;
        this.value = value;
    }

    key;
    value;
}