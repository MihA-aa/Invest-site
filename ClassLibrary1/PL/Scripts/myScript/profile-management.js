var profileManagement;

$(document).ready(function(){
        profileManagement =  $('#profile-management-jq-table')
        .on( 'processing.dt', function ( e, settings, processing ) 
            {$('#loader').css( 'display', processing ? 'block' : 'none' );})
        .DataTable({
            "processing": false,
            "serverSide": true,
            "orderMulti": false,
             "dom": '<"top"i>rt<"bottom"lp><"clear">',
            "ajax": {
                "url": "/Admin/LoadProfileData",
                "type": "POST",
                "datatype": "json",
                "error": function (xhr) {
                    MyAlert(xhr.statusText);
                }
            },
            "columns": [                    
                    { "data": "Id", "name": "Id", "width": "5px", 
                    "render": function (data) {
                      return '<img rel="'+data+'" class="details-control" src = "/Content/img/details_open.png" style="cursor: pointer;">';}},
                    { "data": "Id", "name": "Id", "width": "5px", 
                    "render": function (data) {return '<a class="popup" href="/Profile/Save/'
                     + data + '"><span class="glyphicon glyphicon-pencil" style="color: #80b78c;"></span></a>';}},
                     { "data": "Id", "name": "Id", "width": "5px", 
                    "render": function (data) {return '<a class="popup" href="/Profile/Delete/'
                     + data + '"><span class="glyphicon glyphicon-remove" style="color: #dc6c6c;"></span></a>';}},
                     { "data": "Login", "name": "Login", "autoWidth": true }
            ]
        });
    $('.profileManagmentContainer').on('click', 'a.popup', function (e) {
        e.preventDefault();
        OpenProfileTablePopup($(this).attr('href'));
    })

$('#profile-management-jq-table tbody').on('click', 'td img.details-control', function () {
    var tr = $(this).parent().closest('tr');
    var row = profileManagement.row( tr );
 
    if ( row.child.isShown() ) {
        row.child.hide();
        tr.removeClass('shown');
        $(this).attr("src","/Content/img/details_open.png");
    }
    else {
        $(this).attr("src","/Content/img/details_close.png");
        row.child( format(row.data()) ).show();
        tr.addClass('shown');
    }
} );

function OpenProfileTablePopup(pageUrl) {
    var $profileTablePageContent = $('<div/>');
    $profileTablePageContent.load(pageUrl, function () {
        $('#popupProfileForm', $profileTablePageContent).removeData('validator');
        $('#popupProfileForm', $profileTablePageContent).removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse('form');
    });

    $profileDialog = $('<div class="popupProfileTableWindow" style="overflow:auto"></div>')
    .html($profileTablePageContent)
    .dialog({
        position: { my: 'top', at: 'top+150' },
      draggable : false,
      autoOpen : false,
      resizable : false,
      model : true,
      title:'Popup Profile Dialog',
      height: 'auto',
      width : 600,
      closeText: "",
      close: function () {
          $profileDialog.dialog('destroy').remove();
      }
    })

    $('.popupProfileTableWindow').on('submit', '#popupProfileForm', function (e) {
        var url = $('#popupProfileForm')[0].action;
        $.ajax({
            type : "POST",
            url : url,
            data: $('#popupProfileForm').serialize(),
            success: function (data) {
                if (data.status) {
                    $profileDialog.dialog('close');
                    profileManagement.ajax.reload();
                }
                else{
                    showClientError(data.prop, data.message);
                }
            }
        })

        e.preventDefault();
    })
    $profileDialog.dialog('open');
    $profileDialog.dialog({ closeText: "" });
}

});


function format ( rowData ) {
  $("#loader").show();
    var div = $('<div/>')
        .text( 'Loading...' );
    console.log(rowData);
    $.ajax( {
        type: 'POST',
        url: '/Admin/GetUserRecords',
        data: { "userId": rowData.Id},
            dataType: "html",
        success: function ( data ) {
          console.log(data);
            div.html( data )
            $("#loader").hide();
        },
        "error": function (xhr) {
          alert(xhr.statusText);
            $("#loader").hide();
        }
    } );
 
    return div;
}