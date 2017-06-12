var customerManagement;

$(document).ready(function(){
        customerManagement =  $('#customer-management-jq-table')
        .on( 'processing.dt', function ( e, settings, processing ) 
            {$('#loader').css( 'display', processing ? 'block' : 'none' );})
        .DataTable({
            "processing": false,
            "serverSide": true,
            "orderMulti": false,
             "dom": '<"top"i>rt<"bottom"lp><"clear">',
            "ajax": {
                "url": "/Admin/LoadCustomerData",
                "type": "POST",
                "datatype": "json",
                "error": function (xhr) {
                    MyAlert(xhr.statusText);
                }
            },
            "columns": [
                    { "data": "Id", "name": "Id", "width": "5px", 
                    "render": function (data) {return '<a class="popup" href="/Customer/Save/'
                     + data + '"><span class="glyphicon glyphicon-pencil" style="color: #80b78c;"></span></a>';}},
                     { "data": "Id", "name": "Id", "width": "5px", 
                    "render": function (data) {return '<a class="popup" href="/Customer/Delete/'
                     + data + '"><span class="glyphicon glyphicon-remove" style="color: #dc6c6c;"></span></a>';}},
                     { "data": "Name", "name": "Name", "autoWidth": true }
            ]
        });
    $('.customerManagmentContainer').on('click', 'a.popup', function (e) {
        e.preventDefault();
        OpenCustomerTablePopup($(this).attr('href'));
    })

function OpenCustomerTablePopup(pageUrl) {
    var $customerTablePageContent = $('<div/>');
    $customerTablePageContent.load(pageUrl, function () {
        $('#popupCustomerForm', $customerTablePageContent).removeData('validator');
        $('#popupCustomerForm', $customerTablePageContent).removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse('form');
    });

    $customerDialog = $('<div class="popupCustomerTableWindow" style="overflow:auto"></div>')
    .html($customerTablePageContent)
    .dialog({
        position: { my: 'top', at: 'top+150' },
      draggable : false,
      autoOpen : false,
      resizable : false,
      model : true,
      title:'Popup Customer Dialog',
      height: 'auto',
      width : 600,
      closeText: "",
      close: function () {
          $customerDialog.dialog('destroy').remove();
      }
    })

    $('.popupCustomerTableWindow').on('submit', '#popupCustomerForm', function (e) {
        var url = $('#popupCustomerForm')[0].action;
        $.ajax({
            type : "POST",
            url : url,
            data: $('#popupCustomerForm').serialize(),
            success: function (data) {
                if (data.status) {
                    $customerDialog.dialog('close');
                    customerManagement.ajax.reload();
                }
                else{
                    showClientError(data.prop, data.message);
                }
            }
        })

        e.preventDefault();
    })
    $customerDialog.dialog('open');
    $customerDialog.dialog({ closeText: "" });
}
});