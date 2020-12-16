
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
//Disable send button until connection is established
//document.getElementById("sendButton").disabled = true;
connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " " + msg;
    console.log(encodedMsg);
    if(user.includes("_chbx"))
    {
        if(msg=='High')
            document.getElementById(user).checked=true;
        else
            document.getElementById(user).checked=false;
    }
    else
    {
        var id=user.replace(':','').trim().replace(' ','');
        document.getElementById(id).innerHTML=encodedMsg;
    }
    /*var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);*/
});
connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});
document.getElementById("GPIO18_chbx").addEventListener("change", function (event) {
    var user = 18;
    var message = document.getElementById("GPIO18_chbx").checked==true ? 'High':'Low';
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    connection.invoke("setPin",parseInt(user),(document.getElementById("GPIO18_chbx").checked)).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
document.getElementById("GPIO23_chbx").addEventListener("change", function (event) {
    var user = 23;
    var message = document.getElementById("GPIO23_chbx").checked==true ? 'High':'Low';
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    connection.invoke("setPin",parseInt(user),(document.getElementById("GPIO23_chbx").checked)).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
document.getElementById("GPIO24_chbx").addEventListener("change", function (event) {
    var user = 24;
    var message = document.getElementById("GPIO24_chbx").checked==true ? 'High':'Low';
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    connection.invoke("setPin",parseInt(user),(document.getElementById("GPIO24_chbx").checked)).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
