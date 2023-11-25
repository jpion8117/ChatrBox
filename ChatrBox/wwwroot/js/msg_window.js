var msgWindow = $("#msg_window");
var interval = setInterval(checkMessages, messageUpdateRate);
var topicId = 2;
var lastPost;

function checkMessages() {
    console.log(new Date().toString() + ": checked for new messages")
    $.get("/Communities/CheckMessages", {
        topicId: topicId,
        lastPost: lastPost
    },
        function (data, error) {
            console.info(data);
            //continue only if success code was sent
            if (data.error.code != undefined && data.error.code < 100) {
                if (data.error.code === 0) {
                    lastPost = data.lastPost;
                    msgWindow.empty();
                    msgWindow.append(data.messages);

                    var chat = document.getElementById("msg_window");
                    chat.scrollTop = chat.scrollHeight;
                }
            }
    });
}

checkMessages();