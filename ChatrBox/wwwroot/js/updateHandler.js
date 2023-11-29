var msgWindow = $("#msg_window");
var msgInterval = setInterval(checkMessages, messageUpdateRate);
var userWindow = $("#user_window");
var userStatusIntervaal = setInterval(checkUserStatus, statusUpdateRate);
var lastPost;
var autoScroll = true;

function checkMessages() {
    $.get("/Communities/CheckMessages", {
        topicId: topicId,
        lastPost: lastPost
    },
    function (data, error) {
        //continue only if success code was sent
        if (data.error.code != undefined)
        {
            if (data.error.code === 0)
            {
                lastPost = data.lastPost;
                msgWindow.empty();
                msgWindow.append(data.messages);

                if (autoScroll) scrollToNewest();
            }
            else if (data.error.code === 101)
            {
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

function checkUserStatus() {
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

function fetchTopics() {
    $.get("/Communities/GetTopicList", { communityId: communityId }, function (data, err) {
        if (data && data.error && data.error.code == 0)
        {
            $("#CommunityName").text(data.communityName);
            var topics = $('#TopicList');
            for (var i = 0; i < data.topics.length; i++)
            {
                var active = i === 0 ? " topic-list-active" : ""
                var listClasses = "topic-list-item " + active;
                topics.append("<li id=\"topicId_" + data.topics[i].key + "\" class=\"" + listClasses + "\">" + data.topics[i].value + "</li>");

                $('#topicId_' + data.topics[i].key).on("click",
                    function (event) {
                        event.stopPropagation();
                        var temp = event.target.attributes['id'].value;
                        temp = temp.replace("topicId_", "");

                        topicId = parseInt(temp);
                        checkMessages();

                        $('.topic-list-item').each(function (index, element)
                        {
                            $(element).removeClass("topic-list-active");
                        });

                        $(event.target).addClass("topic-list-active");
                });
            }
        }
    });
}

function scrollToNewest() {
    var chat = document.getElementById("msg_window");
    chat.scrollTop = chat.scrollHeight;
}

checkMessages();
checkUserStatus();
fetchTopics();
scrollToNewest();