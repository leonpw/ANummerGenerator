function CompleteSendFunctionName(jqXHR, textStatus) {
    jqXHR.responseJSON.forEach((item) => {
                   $('#results ol').append('<li>' + item + '</li>')
               });
}
