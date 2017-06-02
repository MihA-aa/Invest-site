$(document).ready(function(){
        tableViewTampleteManagement =  $('#view-template-management-jq-table')
        .on( 'processing.dt', function ( e, settings, processing ) 
            {$('#loader').css( 'display', processing ? 'block' : 'none' );})
        .DataTable({
            "processing": false,
            "serverSide": true,
            "orderMulti": false,
             "dom": '<"top"i>rt<"bottom"lp><"clear">',
            "ajax": {
                "url": "/Nav/LoadViewTampleteData",
                "type": "POST",
                "datatype": "json",
                "error": function (xhr) {
                    MyAlert(xhr.statusText);
                }
            },
            "columns": [
                    { "data": "Id", "name": "Id", "width": "5px", 
                    "render": function (data) {return '<a class="popup" href="/ViewTemplate/Save/'
                     + data + '"><span class="glyphicon glyphicon-pencil" style="color: #80b78c;"></span></a>';}},
                     { "data": "Id", "name": "Id", "width": "5px", 
                    "render": function (data) {return '<a class="popup" href="/ViewTemplate/Delete/'
                     + data + '"><span class="glyphicon glyphicon-remove" style="color: #dc6c6c;"></span></a>';}},
                     { "data": "Id", "name": "Id", "width": "15px", 
                    "render": function (data) {return '<a class="popup" href="/ViewTemplate/RedactColumns/'
                     + data + '">Columns</a>';}},
                    { "data": "Name", "name": "Name", "autoWidth": true },
                    { "data": "Positions", "name": "Positions", "autoWidth": true,
                    "render": function (data) {return parseShowPositionStatus(data);} },
                    { "data": "ShowPortfolioStats", "name": "ShowPortfolioStats", "autoWidth": true },
            ]
        });
    $('.tampleteTableContainer').on('click', 'a.popup', function (e) {
        e.preventDefault();
        OpenTampleteTablePopup($(this).attr('href'));
    })

function OpenTampleteTablePopup(pageUrl) {
    var $tampleteTablePageContent = $('<div/>');
    $tampleteTablePageContent.load(pageUrl, function () {
        $('#popupViewTampleteForm', $tampleteTablePageContent).removeData('validator');
        $('#popupViewTampleteForm', $tampleteTablePageContent).removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse('form');
    });

    $viewTampletedialog = $('<div class="popupTampleteTableWindow" style="overflow:auto"></div>')
    .html($tampleteTablePageContent)
    .dialog({
        position: { my: 'top', at: 'top+150' },
      draggable : false,
      autoOpen : false,
      resizable : false,
      model : true,
      title:'Popup View Template Dialog',
      height: 'auto',
      width : 600,
      closeText: "",
      close: function () {
          $viewTampletedialog.dialog('destroy').remove();
      }
    })

    $('.popupTampleteTableWindow').on('submit', '#popupViewTampleteForm', function (e) {
        var url = $('#popupViewTampleteForm')[0].action;
        $.ajax({
            type : "POST",
            url : url,
            data: $('#popupViewTampleteForm').serialize(),
            success: function (data) {
                if (data.status) {
                    $viewTampletedialog.dialog('close');
                    tableViewTampleteManagement.ajax.reload();
                }
                else{
                    showClientError(data.prop, data.message);
                }
            }
        })

        e.preventDefault();
    })
    $viewTampletedialog.dialog('open');
    $viewTampletedialog.dialog({ closeText: "" });
}
});

$(".selectpicker").selectpicker({
    "title": "Select Options"
}).selectpicker("render");

function parseShowPositionStatus(data){
    switch (data) {
  case 0:
    return ( 'All' );
  case 1:
    return( 'ClosedOnly' );
  case 2:
    return( 'OpenAndWaiting' );
  case 3:
    return( 'OpenOnly' );
  default:
    return( ' ' );
    }
}