function init_CheckInput() {
    var $input = $('input.currency');

    $input.on("keyup", function (event) {


        // When user select text in the document, also abort.
        var selection = window.getSelection().toString();
        if (selection !== '') {
            return;
        }

        // When the arrow keys are pressed, abort.
        if ($.inArray(event.keyCode, [38, 40, 37, 39]) !== -1) {
            return;
        }

        var $this = $(this);

        // Get the value.
        var input = $this.val();

        var input = input.replace(/[\D\s\._\-]+/g, "");
        input = input ? parseInt(input, 10) : 0;

        $this.val(function () {
            return (input === 0) ? "" : input.toLocaleString("en-US");
        });
    });

    /**
        * ==================================
        * When Form Submitted
        * ==================================
        */
}

function init_Value() {
    var $input = $('input.currency');
    var valCur = $input.val();
    valCur = valCur ? parseInt(valCur, 10) : 0;
    $input.val(function () {
        return (valCur === 0) ? "" : valCur.toLocaleString("en-US");
    });
}

function init_CheckForm() {
    var $form = $('.form-horizontal');

    /*
    * ==================================
    * When Form Submitted
    * ==================================
    */
    $form.on("submit", function (event) {

        $('input.currency').val($('input.currency').val().replace(/[\D\s\._\-\,]+/g, ""));
        event.preventDefault();
    });
}

$(document).ready(function () {
    init_Value();
    init_CheckInput();
    init_CheckForm();
});