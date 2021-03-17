function ajaxCall(method, api, data, successCB, errorCB) {
    $.ajax({
        type: method,
        url: api,
        data: data,
        cache: false,
		headers: {
            'resource_id': '5c78e9fa-c2e2-4771-93ff-7f400a12f7ba',
        },
        contentType: "application/json",
        dataType: "json",
        success: successCB,
        error: errorCB
    });
}