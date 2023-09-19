let selected;
$(".delete").on("click", function () {
    let value = this.value;
    let origin = window.location.origin;
    let url = origin + "/Home/DeleteRow?NodeId=" + value;

    $.ajax({
        url: url,
        method: 'GET',
        success: function (data) {
            location.reload();
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });

});

$(".edit").on("click", function () {
    selected = this.value;
    

});

$("#addsubmit").on("click", function () {
    let nodename = $("#nodename1").val();
    let parentnodeid = $("#parentnodeid").val();
    let origin = window.location.origin;
    let url = origin + "/Home/AddRow?nodename=" + nodename + "&parentnodeid=" + parentnodeid;

    $.ajax({
        url: url,
        method: 'GET',
        success: function (data) {
            location.reload();
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });

});

$("#editsubmit").on("click", function () {
    var nodeid = selected;
    let parentid = $("#parentnodeid1").val();
    let origin = window.location.origin;
    let url = origin + "/Home/EditRow?nodeid=" + nodeid + "&parentnodeid=" + parentid;

    $.ajax({
        url: url,
        method: 'GET',
        success: function (data) {
            location.reload();
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });

});