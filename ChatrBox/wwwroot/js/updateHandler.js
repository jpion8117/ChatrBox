var msgWindow = $("#msg_window");
var msgInterval = setInterval(checkMessages, messageUpdateRate);
var userWindow = $("#user_window");
var userStatusIntervaal = setInterval(checkUserStatus, statusUpdateRate);
var lastPost;
var autoScroll = true;

//These will be moved to an script tag on page that will be updated dynamically based on what community/topic the user is browsing
var topicId = 2;
var communityId = 1;

scrollToNewest();

function checkMessages() {
    console.log(new Date().toString() + ": checked for new messages");
    $.get("/Communities/CheckMessages", {
        topicId: topicId,
        lastPost: lastPost
    },
        function (data, error) {
            //continue only if success code was sent
            if (data.error.code != undefined && data.error.code < 100) {
                if (data.error.code === 0) {
                    lastPost = data.lastPost;
                    msgWindow.empty();
                    msgWindow.append(data.messages);

                    if (autoScroll) scrollToNewest();
                }
            }
    });

    //perform user checkin
    $.get("/Home/UserCheckIn");
}

function checkUserStatus() {
    console.log(new Date().toString() + ": checked community users' online status");
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

function scrollToNewest() {
    var chat = document.getElementById("msg_window");
    chat.scrollTop = chat.scrollHeight;
}

checkMessages();
checkUserStatus();
