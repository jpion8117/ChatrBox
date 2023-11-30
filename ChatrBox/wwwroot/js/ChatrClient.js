class ChatrBoxClient {
    //UserId of the current ChatrBox User
    static UserId;

    //Username of the current ChatrBox User
    static Username;

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

            ChatrBoxClient.LogActivity(`User ${this.Username} configured ChatrBoxClient.Interval with key:${key} and delay of ${time} milliseconds`);

            return true;
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
                    return true;
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
    static GetMessages() {
        ChatrBoxClient.LogActivity("Checked Messages", true);
        $.get("/Communities/CheckMessages", {
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
            });

        //perform user checkin
        $.get("/Home/UserCheckIn");
    }

    /**
     * Requests a list of topics from the user and displays them on the client window.
     */
    static GetTopics() {
        $.get("/Communities/GetTopicList", { communityId: communityId }, function (data, err) {
            if (data && data.error && data.error.code == 0) {
                $("#CommunityName").text(data.communityName);
                var topics = $('#TopicList');
                for (var i = 0; i < data.topics.length; i++) {
                    var active = i === 0 ? " topic-list-active" : ""
                    var listClasses = "topic-list-item " + active;
                    topics.append("<li id=\"topicId_" + data.topics[i].key + "\" class=\"" + listClasses + "\">" + data.topics[i].value + "</li>");

                    $('#topicId_' + data.topics[i].key).on("click",
                        function (event) {
                            event.stopPropagation();
                            var temp = event.target.attributes['id'].value;
                            temp = temp.replace("topicId_", "");

                            ChatrBoxClient.Settings.TopicId = parseInt(temp);
                            ChatrBoxClient.GetMessages();

                            $('.topic-list-item').each(function (index, element) {
                                $(element).removeClass("topic-list-active");
                            });

                            $(event.target).addClass("topic-list-active");
                        });
                }
            }
        });
    }

    /**
     * Retrieves online user status from the server and updates the client display. Posts to enabled logs upon failure.
     */
    static GetUserStatuses() {

        var userWindow = $("#user_window");

        $.get("/Communities/GetUsersOnline", { communityId: communityId },
            function (data, error) {
                userWindow.empty();

                userWindow.append("<div id=\"onlineUsers\"></div>");
                $("#onlineUsers").append("<div class=\"onlineUsersHeading\">Online:</div>");
                $("#onlineUsers").append(data.usersOnline);

                userWindow.append("<div id=\"offlineUsers\"></div>");
                $("#offlineUsers").append("<div class=\"offlineUsersHeading\">Offline:</div>");
                $("#offlineUsers").append(data.usersOffline);
            });
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
            $.post("/Config/Admin/Log", {
                logMsg: logMsg,
                file: "clientSide.log"
            });
        }
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