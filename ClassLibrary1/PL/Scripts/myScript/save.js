function tradeStatusCheck(that) {
        if (that.value == "Close") {
            $("#FromForClosePosition").slideDown();
        }
        else {
            $("#FromForClosePosition").slideUp();
        }
    }

    $(function () {

        if ($("#TradeStatus option:selected").text() == "Close") {
            $("#FromForClosePosition").css("display","block")
        }

        $('#datetimepicker1').datetimepicker({
            format: 'DD/MM/YYYY',
            widgetPositioning: {
                vertical: 'top'
            }
        });

        $('#datetimepicker2').datetimepicker({
            format: 'DD/MM/YYYY',
            widgetPositioning: {
                vertical: 'top'
            }
        });
        $('#OpenDate').removeAttr("data-val-date");
    });

    $(".selectpicker").selectpicker({
        "title": "Select Options"
    }).selectpicker("render");

    $('#input-symbol-search').autocomplete({
        autoFocus: true,
        minLength: 2,
        source: $('#input-symbol-search').attr("data-autocomplete-source")
    });

    $("#symbolNotFoundDialog").dialog({
        autoOpen: false,
        title: "Information",
        draggable: false,
        resizable: false,
        closeText: "",
        modal: true,
        buttons: {
            'Ok': function () {
                $(this).dialog('close');
            }
        }
    });