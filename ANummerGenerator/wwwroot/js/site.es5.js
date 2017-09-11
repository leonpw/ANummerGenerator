'use strict';

function CompleteSendFunctionName(jqXHR, textStatus) {
    jqXHR.responseJSON.forEach(function (item) {
        $('#results ol').append('<li>' + item + '</li>');
    });
}

