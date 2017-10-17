
    function onBegin() {
        $('ajax-loader').show();
    }

function onComplete() {
    $('ajax-loader').hide();
    var url = "/Register/Index";
    window.location.href = url;

}

function onSuccess() {
    alert("Client Added Successfully!");

}

function onFailure() {
    alert("Failure occured while saving!");
}

    function addBusinessAreas(e) {
        var selectedId = e.val();
        var url = '';
        var isAdd = e.is(':checked');
        if (isAdd) {
            url = '/Register/AddArea';
            $.ajax({
                url: url,
                data: { id: selectedId },
                cache: false,
                success: function (html) {
                    $("#areaContacts").append(html);
                }
            });
        } else {
            $('#' + selectedId).remove();
        }

    }