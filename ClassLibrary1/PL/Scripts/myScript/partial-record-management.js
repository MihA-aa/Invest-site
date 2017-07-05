
var tradeManagementIndex;
$(document).ready(function(){
        containerIndex = $(document.body.lastChild).closest('div').attr("id");
        console.log(containerIndex);
        tradeManagementIndex = $(".recordPartialManagmentContainer #tradeManagementIndex").attr("value");
        console.log(tradeManagementIndex);
        $('#record-partial-management-jq-table-'+tradeManagementIndex)
        .on( 'processing.dt', function ( e, settings, processing ) 
            {$('#loader').css( 'display', processing ? 'block' : 'none' );})
        .DataTable({
            "processing": false,
            "serverSide": true,
            "orderMulti": false,
             "dom": '<"top"i>rt<"bottom"lp><"clear">',
            "ajax": {
                "url": "/Admin/LoadRecordData",
                "type": "POST",
                "datatype": "json",
                "data": {"id" : tradeManagementIndex},
                "error": function (xhr) {
                    MyAlert(xhr.statusText);
                }
            },
            "columns": [
                    { "data": "UserId", "name": "UserId", "autoWidth": true },
                    { "data": "Entity", "name": "Entity", "autoWidth": true,
                     "render": function (data) {return parseEntity(data);} },
                    { "data": "EntityId", "name": "EntityId", "autoWidth": true },
                    { "data": "Operation", "name": "Operation", "autoWidth": true,
                     "render": function (data) {return parseOperation(data);} },
                    { "data": "DateTime", "name": "DateTime", "autoWidth": true, 
                    "render": function (data) {return parseDateTime(data);} },
                    { "data": "Successfully", "name": "Successfully", "autoWidth": true }
            ]
        });
});

function parseOperation(data){
    switch (data) {
  case 0:
    return ( 'Create' );
  case 1:
    return( 'Update' );
  case 2:
    return( 'Delete' );
  default:
    return( ' ' );
    }
}

function parseEntity(data){
    switch (data) {
  case 0:
    return ( 'Position' );
  case 1:
    return( 'Portfolio' );
  case 2:
    return( 'View' );
  case 3:
    return( 'ViewTemplate' );
  case 4:
    return( 'ViewTemplateColumn' );
  default:
    return( ' ' );
    }
}

function parseDateTime(data){
    if(data == null)
        return "";
    var datet = new Date(parseInt(data.substr(6)));
    var newData = datet.getDate()  + "/" + (datet.getMonth() + 1) + "/" + datet.getFullYear()+ " "+ datet.getHours() + ":" + datet.getMinutes() + ":" + datet.getSeconds();
    return newData;
}