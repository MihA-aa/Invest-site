$(document).ready(function(){
        viewManagement =  $('#view-management-jq-table')
        .on( 'processing.dt', function ( e, settings, processing ) 
            {$('#loader').css( 'display', processing ? 'block' : 'none' );})
        .DataTable({
            "processing": false,
            "serverSide": true,
            "orderMulti": false,
             "dom": '<"top"i>rt<"bottom"lp><"clear">',
            "ajax": {
                "url": "/Nav/LoadViewData",
                "type": "POST",
                "datatype": "json",
                "error": function (xhr) {
                    MyAlert(xhr.statusText);
                }
            },
            "columns": [
                    { "data": "Id", "name": "Id", "width": "5px", 
                    "render": function (data) {return '<a class="popup" href="/View/Save/'
                     + data + '"><span class="glyphicon glyphicon-pencil" style="color: #80b78c;"></span></a>';}},
                     { "data": "Id", "name": "Id", "width": "5px", 
                    "render": function (data) {return '<a class="popup" href="/View/Delete/'
                     + data + '"><span class="glyphicon glyphicon-remove" style="color: #dc6c6c;"></span></a>';}},
                     { "data": "Name", "name": "Name", "autoWidth": true },
                     { "data": "ShowName", "name": "ShowName", "autoWidth": true},
                     { "data": "MoneyPrecision", "name": "MoneyPrecision", "autoWidth": true },
                     { "data": "PercentyPrecision", "name": "PercentyPrecision", "autoWidth": true },
                     { "data": "DateFormat", "name": "DateFormat", "autoWidth": true,
                     "render": function (data) {return parseDateFormat(data);} }
            ]
        });
    $('.viewContainer').on('click', 'a.popup', function (e) {
        e.preventDefault();
        OpenViewTablePopup($(this).attr('href'));
    })

function OpenViewTablePopup(pageUrl) {
    var $viewTablePageContent = $('<div/>');
    $viewTablePageContent.load(pageUrl, function () {
        $('#popupViewForm', $viewTablePageContent).removeData('validator');
        $('#popupViewForm', $viewTablePageContent).removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse('form');
    });

    $viewDialog = $('<div class="popupViewTableWindow" style="overflow:auto"></div>')
    .html($viewTablePageContent)
    .dialog({
        position: { my: 'top', at: 'top+150' },
      draggable : false,
      autoOpen : false,
      resizable : false,
      model : true,
      title:'Popup View Dialog',
      height: 'auto',
      width : 600,
      closeText: "",
      close: function () {
          $viewDialog.dialog('destroy').remove();
      }
    })

    $('.popupViewTableWindow').on('submit', '#popupViewForm', function (e) {
        var url = $('#popupViewForm')[0].action;
        $.ajax({
            type : "POST",
            url : url,
            data: $('#popupViewForm').serialize(),
            success: function (data) {
                if (data.status) {
                    $viewDialog.dialog('close');
                    viewManagement.ajax.reload();
                }
                else{
                    showClientError(data.prop, data.message);
                }
            }
        })

        e.preventDefault();
    })
    $viewDialog.dialog('open');
    $viewDialog.dialog({ closeText: "" });
}
});


function parseDateFormat(data){
    switch (data) {
  case 0:
    return ( 'MonthDayYear' );
  case 1:
    return( 'DayMonthYear' );
  case 2:
    return( 'DayMonthNameYear' );
  default:
    return( ' ' );
    }
}