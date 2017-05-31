

$(function () {

    if ($("#TradeStatus option:selected").text() == "Close") {
        $("#FromForClosePosition").css("display","block")
    }

    $('#datetimepicker1').datetimepicker({
        format: 'DD/MM/YYYY',
        widgetPositioning: {
            vertical: 'top'
        }
    })
    .on('dp.change', function(e){ 
        if( $("#SymbolIdHidden").val().length !== 0 ) {
            var date = $('#datetimepicker1').find("input").val();
            var elem = $("#SymbolOpenPrice");
            completeInputSymbolOpenDate(date, elem);
            
        }
    });

    $('#datetimepicker2').datetimepicker({
        format: 'DD/MM/YYYY',
        widgetPositioning: {
            vertical: 'top'
        }
    }).on('dp.change', function(e){ 
        if( $("#SymbolIdHidden").val().length !== 0 ) {
            var date = $('#datetimepicker2').find("input").val();
            var elem = $("#SymbolClosePrice");
            completeInputSymbolOpenDate(date, elem);
        }
    })

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


function completeInputSymbolData() {
        $.ajax({
            url: '/Nav/CheckIfExist',
            type: "POST",
            data: { "value": $("#input-symbol-search").val() },
            success: function (data) {
                if (!data.success) {
                    $("#input-symbol-search").val('');
                    $("#symbolNotFoundDialog").dialog("open");
                } 
                else {

                     $("#TextBoxForSymbolName").val(data.symbolname);
                     $("#SymbolIdHidden").val(data.symbolId);
                     if($('#datetimepicker1').find("input").val().length !== 0){
                        var date = $('#datetimepicker1').find("input").val();
                        var elem = $("#SymbolOpenPrice");
                        completeInputSymbolOpenDate(date, elem);
                     }
                     if($('#datetimepicker2').find("input").val().length !== 0){
                        var date = $('#datetimepicker2').find("input").val();
                        var elem = $("#SymbolClosePrice");
                        completeInputSymbolOpenDate(date, elem);
                     }
                }
            }
        });
    }
function completeInputSymbolOpenDate(date, elem) {
        $.ajax({
            url: '/Nav/GetSybolPriceForDate',
            type: "POST",
            data: { "date": date,
            "symbolId": $("#SymbolIdHidden").val()},
            success: function (data) {
                val = data.price.toString().replace(".", ",");
                elem.val(val);
            },
            error: function (xhr) {
                MyAlert(xhr.statusText);
            }
        });
    } 
    function tradeStatusCheck(that) {
        if (that.value == "Close") {
            $("#FromForClosePosition").slideDown();
        }
        else {
            $("#FromForClosePosition").slideUp();
        }
    }