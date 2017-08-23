// Write your Javascript code.

'use strict';

$(function () {

    $('#Generate').on('click', function () {
        var amount = $('#amount').val();
        $.ajax({
            url: "/api/Anummer/" + amount,
            success: function success(result) {
                result.forEach(function (item) {
                    $('#results ol').append('<li>' + item + '</li>');
                });
            },
            error: function error(_error) {
                alert(_error.text);
            }

        });
    });
});

