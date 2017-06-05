var columnTampleteIndex;

$(document).ready(function(){
columnTampleteIndex = $("#columnTampleteIndex").attr('value');

        tableViewTampleteColumnManagement =  $('#view-template-column-management-jq-table')
        .on( 'processing.dt', function ( e, settings, processing ) 
            {$('#loader').css( 'display', processing ? 'block' : 'none' );})
        .DataTable({
            "processing": false,
            "serverSide": true,
            "orderMulti": false,
             "dom": '<"top"i>rt<"bottom"lp><"clear">',
            "ajax": {
                "url": "/Nav/LoadViewColumnTampleteData",
                "type": "POST",
                "datatype": "json",
                "data": {"id" : columnTampleteIndex},
                "error": function (xhr) {
                    MyAlert(xhr.statusText);
                }
            },
            "columns": [
                    { "data": "DisplayIndex", "name": "DisplayIndex", "autoWidth": true },
                    { "data": "Name", "name": "Name", "width": "5px" },
                    { "data": "Id", "name": "Id", "width": "5px", 
                    "render": function (data) {return '<a class="popup" href="/ViewTemplateColumn/Save/'
                     + data + '"><span class="glyphicon glyphicon-pencil" style="color: #80b78c;"></span></a>';}},
                     { "data": "Id", "name": "Id", "width": "5px", 
                    "render": function (data) {return '<a class="popup" href="/ViewTemplateColumn/Delete/'
                     + data + '"><span class="glyphicon glyphicon-remove" style="color: #dc6c6c;"></span></a>';}}
            ]
        });
    $('.tampleteColumnTableContainer').on('click', 'a.popup', function (e) {
        e.preventDefault();
        OpenTampleteTablePopup($(this).attr('href'));
    })

function OpenTampleteTablePopup(pageUrl) {
    var $tampleteColumnTablePageContent = $('<div/>');
    $tampleteColumnTablePageContent.load(pageUrl, function () {
        $('#popupViewTampleteColumnForm', $tampleteColumnTablePageContent).removeData('validator');
        $('#popupViewTampleteColumnForm', $tampleteColumnTablePageContent).removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse('form');
    });

    $viewTampleteColumndialog = $('<div class="popupTampleteColumnTableWindow" style="overflow:auto"></div>')
    .html($tampleteColumnTablePageContent)
    .dialog({
        position: { my: 'top', at: 'top+150' },
      draggable : false,
      autoOpen : false,
      resizable : false,
      model : true,
      title:'Popup ViewTemplate Column Dialog',
      height: 'auto',
      width : 600,
      closeText: "",
      close: function () {
          $viewTampleteColumndialog.dialog('destroy').remove();
      }
    })

    $('.popupTampleteColumnTableWindow').on('submit', '#popupViewTampleteColumnForm', function (e) {
        var url = $('#popupViewTampleteColumnForm')[0].action;
        $.ajax({
            type : "POST",
            url : url,
            data: $('#popupViewTampleteColumnForm').serialize()+ "&templateId=" + columnTampleteIndex,
            success: function (data) {
                if (data.status) {
                    $viewTampleteColumndialog.dialog('close');
                    tableViewTampleteColumnManagement.ajax.reload();
                }
                else{
                    showClientError(data.prop, data.message);
                }
            }
        })

        e.preventDefault();
    })
    $viewTampleteColumndialog.dialog('open');
    $viewTampleteColumndialog.dialog({ closeText: "" });
}
});

function buildColumnSearchData(){
    return {"id" : columnTampleteIndex};
}
