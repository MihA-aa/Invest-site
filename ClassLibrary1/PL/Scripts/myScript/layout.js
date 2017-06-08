
$(function () {
    $('#panelList').sortable({
        stop: function (event, ui) {
            portfolioAllRefresh();
        }
    });
    $("#panelList").disableSelection();

    $('#datetimepicker6').datetimepicker({
        format: 'DD/MM/YYYY'
    });
    $('#datetimepicker7').datetimepicker({
        format: 'DD/MM/YYYY',
        useCurrent: false
    });
    $("#datetimepicker6").on("dp.change", function (e) {
        $('#datetimepicker7').data("DateTimePicker").minDate(e.date);
    });
    $("#datetimepicker7").on("dp.change", function (e) {
        $('#datetimepicker6').data("DateTimePicker").maxDate(e.date);
    });

    $('#datetimepicker8').datetimepicker({
        format: 'DD/MM/YYYY'
    });
    $('#datetimepicker9').datetimepicker({
        format: 'DD/MM/YYYY',
        useCurrent: false
    });
    $("#datetimepicker8").on("dp.change", function (e) {
        $('#datetimepicker9').data("DateTimePicker").minDate(e.date);
    });
    $("#datetimepicker9").on("dp.change", function (e) {
        $('#datetimepicker8').data("DateTimePicker").maxDate(e.date);
    });

    $('#addBtn').click(function () {
        var input = $("#generalForm > #id").detach();
        $( "#generalForm" ).submit();
        input.appendTo('#generalForm');
        $('#myTab a:first').tab('show');
    });

    $('.delete').click(function () {
        var item = $(this).parent();
        var id = item.find('.PositionId').attr('value');
        deletePortfolio(id, item);
    });

    $('.edit').click(function () {
         var id = $(this).parent().find('.PositionId').attr('value');
        loadGeneralForInput(id)
        cleanActiveClass(id);
    });

    $('[href = "#general"]').click(function () {
       var inputId = getActiveInput();
       loadGeneralForInput(inputId);
   });

    $('.portfolio-item').click(function () {
        var id = $(this).parent().find('.PositionId').attr('value');
        cleanActiveClass(id);
        loadTradeManagement()
        ReloadTable()
    });
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        var target = $(e.target).attr("href");
        if(target == "#tradeManagement"){
            loadTradeManagement();
            ReloadTable()
        }
    });

    $(".viewItem").click(function(e){
        e.preventDefault();
        applyViewCheck(this);
    });

    $('#btnSearch').click(function () {
        var symbolName = $('#txtSymbolName').val().trim();
        var tradeStatus = $('#ddStatus').val().trim();
        var openDateFrom = $('#open-date-from').val().trim();
        var openDateTo = $('#open-date-to').val().trim();
        var closeDateFrom = $('#close-date-from').val().trim();
        var closeDateTo = $('#close-date-to').val().trim();
        tableTradeManagement.columns(3).search(symbolName);
        tableTradeManagement.columns(8).search(tradeStatus);
        tableTradeManagement.columns(5).search(openDateFrom);
        tableTradeManagement.columns(6).search(openDateTo);
        tableTradeManagement.columns(10).search(closeDateFrom);
        tableTradeManagement.columns(11).search(closeDateTo);
        tableTradeManagement.draw();
    });
});

var tradeManagementIndex;
var tableTradeManagement;

$(document).ready(function(){

        loadTradeManagement();
        LoadDataTable(1);

$('.tablecontainer').on('click', 'a.popup', function (e) {
    e.preventDefault();
    OpenPopup($(this).attr('href'));
})

function OpenPopup(pageUrl) {
    var $pageContent = $('<div/>');
    $pageContent.load(pageUrl, function () {
        $('#popupForm', $pageContent).removeData('validator');
        $('#popupForm', $pageContent).removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse('form');
    });

    $dialog = $('<div class="popupWindow" style="overflow:auto"></div>')
    .html($pageContent)
    .dialog({
        position: { my: 'top', at: 'top+150' },
      draggable : false,
      autoOpen : false,
      resizable : false,
      model : true,
      title:'Popup Dialog',
      height: 'auto',
      width : 600,
      closeText: "",
      close: function () {
          $dialog.dialog('destroy').remove();
      }
    })

    $('.popupWindow').on('submit', '#popupForm', function (e) {
        var url = $('#popupForm')[0].action;
        $.ajax({
            type : "POST",
            url : url,
            data: $('#popupForm').serialize()+ "&portfolioId=" + tradeManagementIndex,
            success: function (data) {
                if (data.status) {
                    $dialog.dialog('close');
                    tableTradeManagement.ajax.reload();
                }
                else{
                    showClientError(data.prop, data.message);
                }
            }
        })

        e.preventDefault();
    })
    $dialog.dialog('open');
    $dialog.dialog({ closeText: "" });
}
});

function LoadDataTable(ViewId) {
    $.ajax({
        url: "/Nav/ApplyView",
        type: "POST",
        data: { id: ViewId },
        success: function (result) {
            RenderMatching(result);
            tableTradeManagement =  $('#trade-management-jq-table')
            .on( 'processing.dt', function ( e, settings, processing ) 
                {$('#loader').css( 'display', processing ? 'block' : 'none' );})
            .DataTable({
                "processing": false,
                "serverSide": true,
                "orderMulti": false,
                "dom": '<"top"i>rt<"bottom"lp><"clear">',
                "ajax": {
                    "url": "/Nav/LoadData",
                    "type": "POST",
                    "datatype": "json",
                    "data": function ( d ){
                      $.extend(d, tradeManagementIndex);
                      d.id = tradeManagementIndex;
                      var dt_params = $('#trade-management-jq-table').data('dt_params');
                      if(dt_params){ $.extend(d, dt_params); }
                    },
                    "error": function (xhr) {
                    MyAlert(xhr.statusText);
                    }
                },
                "columns": result.columns
            });
        }
    });
}

$("#alertErrorDialog").dialog({
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

function RenderMatching(result) {
    $.each(result.columns, function( index, value ) {
        switch (result.columns[index].render) 
        {
          case "saveActionLink":
          result.columns[index].render = function (data) {return getSaveActionLink(data);};
          break;
          case "deleteActionLink":
          result.columns[index].render = function (data) {return getDeleteActionLink(data);};
          break;
          case "OpenDate":
          case "CloseDate":
          case "LastUpdateDate":
          result.columns[index].render = function (data) {return parseDateTime(data);};
          break;
          case "TradeStatus":
          result.columns[index].render = function (data) {return parseTradeStatus(data);};
          break;
          case "TradeType":
          result.columns[index].render = function (data) {return parseTradeType(data);};
          break;
          default:
          delete result.columns[index].render;
      }
  });
}

function MyAlert(message) {
    $("#errorDialogText").text(message);
    $("#alertErrorDialog").dialog("open");
}

function showClientError(propName, message) {
    $("#"+propName).removeClass("valid")
     .addClass( "input-validation-error" );
    $("#"+propName).next().removeClass("field-validation-valid")
    .addClass( "field-validation-error" );
    $( '<span for="'+propName+'" class="">'+message+'</span>' )
    .appendTo( $("#"+propName).next() );
}

function buildSearchData(){
    return {"id" : 1};
}

function ReloadTable(){
     tableTradeManagement.clear();
     tableTradeManagement.data('dt_params', { "name": "id" });
     tableTradeManagement.ajax.reload();
     tableTradeManagement.draw();
}


function loadTradeManagement(){
    tradeManagementIndex = getActiveInput();
    $('[href = "#tradeManagement"]').tab('show');
}

function loadGeneralForInput(id){
    $("#generalForm > #id").attr('value', id);
    $( "#generalForm" ).submit();
    $('#myTab a:first').tab('show');
}

function getActiveInput(){
    return $(".li-item.active > .PositionId").attr('value');
    }

function cleanActiveClass(id){
    $(".li-item").removeClass("active");
    $('.PositionId[value = '+id+']').parent().addClass("active");
    }

function deletePortfolio(portfolioId,item) {
	$("#loader").show();
    $.ajax({
        url: '/Portfolio/DeletePortfolio',
        type: "POST",
        data: { "id": portfolioId},
        success: function (data) {
        	deletePortfolioInView(item)
         	$("#loader").hide();
         },
         error: function (xhr) {
            MyAlert(xhr.statusText);
         	$("#loader").hide();
         }
    });
}

function deletePortfolioInView(item){
    	var flag = false; 
         if(item.hasClass("active")){
         	flag = true;
         }
         item.remove();
         if(flag){
         	$("#panelList").children(":first").addClass("active");
            var id = $("#panelList").children(":first").find('.PositionId').attr('value');
            loadGeneralForInput(id)
         }
         portfolioAllRefresh();
    }

function portfolioAllRefresh(){
    	portfolioInputRefresh();
        loadPortfolioRefresh(getPortfoliosIndex());
    }

function portfolioInputRefresh(){
    $("#panelList li").each(function (index, item) {
        $(item).find('.DisplayIndex').attr('value', index+1);
    });}

function getPortfoliosIndex(){
    var portfolios = {};
    $("#panelList li").each(function (index, item) {
        var key = $(item).find('.PositionId').attr('value');
        var value = $(item).find('.DisplayIndex').attr('value');
        portfolios[key] = value;
    });
    return portfolios;
}

function loadPortfolioRefresh(portfolios){
    $("#loader").show();
    $.ajax({
        type: 'POST',
        url: '/Nav/RefreshPortfolioDisplayIndex',
        data: { "portfolios": portfolios},
        success: function (data) {
            $("#loader").hide();
         },
    });
}

function parseDateTime(data){
    if(data == null)
        return "";
    var datet = new Date(parseInt(data.substr(6)));
    var newData = datet.getDate()  + "/" + (datet.getMonth() + 1) + "/" + datet.getFullYear();
    return newData;
}

function parseTradeStatus(data){
    switch (data) {
  case 0:
    return ( 'Open' );
  case 1:
    return( 'Close' );
  case 2:
    return( 'Wait' );
  default:
    return( ' ' );
    }
}

function getSaveActionLink(data){
    return '<a class="popup" href="/Position/Save/' + data + '"><span class="glyphicon glyphicon-pencil" style="color: #80b78c;"></span></a>';
}

function getDeleteActionLink(data){
    return '<a class="popup" href="/Position/Delete/'+ data + '"><span class="glyphicon glyphicon-remove" style="color: #dc6c6c;"></span></a>';
}

function applyViewCheck(that) {
    console.log($(that).attr('id'));
    // ReloadTable();
    LoadDataTable($(that).attr('id'));
}

function parseTradeType(data){
    switch (data) {
  case 0:
    return ( 'Long' );
  case 1:
    return( 'Short' );
  default:
    return( ' ' );
    }
}

