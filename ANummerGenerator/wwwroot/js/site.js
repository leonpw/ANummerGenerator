// Write your Javascript code.

$(function () {

    $('#Generate').on('click', () => {
        var amount = $('#amount').val();
        $.ajax({
            url: "/api/Anummer/" + amount,
            success: function (result) {
                result.forEach((item) => {
                    $('#results ol').append('<li>' + item + '</li>')
                });
            },
            error: (error) => { alert(error.text); },
            
            
        });
    });

});