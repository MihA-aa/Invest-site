
$(function () {
    $('#panelList').sortable({
        stop: function (event, ui) {
            portfolioAllRefresh();
        }
    });
    $("#panelList").disableSelection();

    $('#OpenDateFromDatetimepicker').datetimepicker({
        format: 'DD/MM/YYYY'
    });
    $('#OpenDateToDatetimepicker').datetimepicker({
        format: 'DD/MM/YYYY',
        useCurrent: false
    });
    $("#OpenDateFromDatetimepicker").on("dp.change", function (e) {
        $('#OpenDateToDatetimepicker').data("DateTimePicker").minDate(e.date);
    });
    $("#OpenDateToDatetimepicker").on("dp.change", function (e) {
        $('#OpenDateFromDatetimepicker').data("DateTimePicker").maxDate(e.date);
    });

    $('#CloseDateFromDatetimepicker').datetimepicker({
        format: 'DD/MM/YYYY'
    });
    $('#CloseDateToDatetimepicker').datetimepicker({
        format: 'DD/MM/YYYY',
        useCurrent: false
    });
    $("#CloseDateFromDatetimepicker").on("dp.change", function (e) {
        $('#CloseDateToDatetimepicker').data("DateTimePicker").minDate(e.date);
    });
    $("#CloseDateToDatetimepicker").on("dp.change", function (e) {
        $('#CloseDateToDatetimepicker').data("DateTimePicker").maxDate(e.date);
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
      if( !$('#panelList').find('li').length ){
        var input = $("#generalForm > #id").detach();
        $( "#generalForm" ).submit();
        input.appendTo('#generalForm');
        $('#myTab a:first').tab('show');
      }
      else{
       loadGeneralForInput(getActiveInput());
      }
   });

    $('.portfolio-item').click(function () {
        var id = $(this).parent().find('.PositionId').attr('value');
        cleanActiveClass(id);
        loadTradeManagement()
            applyViewCheck(viewManagementIndex);
    });
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        var target = $(e.target).attr("href");
        if(target == "#tradeManagement"){
            loadTradeManagement();
            applyViewCheck(viewManagementIndex);
        }
    });

    $(".viewItem").click(function(e){
        e.preventDefault();
        applyViewCheck($(this).attr('id'));
    });

    $('#btnSearch').click(function () {
        SearchSetting();
        tableTradeManagement.draw();
    });


});

var tradeManagementIndex;
var viewManagementIndex;
var tableTradeManagement;
var htmlTable = '<table id="trade-management-jq-table" class="display" cellspacing="0" width="100%"><tbody></tbody></table>';
$(document).ready(function(){
  var firstViewValue = $("#ViewListUl").find(".viewItem").first().attr('id');
   viewManagementIndex = firstViewValue == undefined ? 1 : firstViewValue;
  loadTradeManagement();
  LoadDataTable(viewManagementIndex);

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
                "dom": '<"top"pl>rt<"bottom"i><"clear">',
                "order": [ [ result.sortColumnDisplayIndex, result.sortOrder.toLowerCase() ]],
                "search": {"search": result.showPosition},
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
    domTable = $('#trade-management-jq-table');
}

function applyViewCheck(value) {
    $("#tableDiv").empty();
    $('#tableDiv').append(htmlTable);
    viewManagementIndex = value;
    LoadDataTable(value);
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

function SearchSetting() {
  var symbolName = $('#txtSymbolName').val().trim();
  var TradeStatus = $('#ddStatus').val().trim();
  var openDateFrom = $('#open-date-from').val().trim();
  var openDateTo = $('#open-date-to').val().trim();
  var closeDateFrom = $('#close-date-from').val().trim();
  var closeDateTo = $('#close-date-to').val().trim();
  var columns = tableTradeManagement.settings().init().columns;
  tableTradeManagement.columns().every(function(index) {
   switch (columns[index].name) {
    case "SymbolName":
      tableTradeManagement.columns(index).search(symbolName);
      break;
    case "TradeStatus":
      tableTradeManagement.columns(index).search(TradeStatus);
      break;
    case "OpenDate":
      tableTradeManagement.columns(index).search(openDateFrom + "t" + openDateTo);
      break;
    case "CloseDate":
      tableTradeManagement.columns(index).search(closeDateFrom + "t" + closeDateTo);
      break;
    }
})
}

function RenderMatching(result) {
  console.log(result);
    $.each(result.columns, function( index, value ) {
        switch (result.columns[index].render) 
        {
          case "saveActionLink":
          result.columns[index].render = function (data) {return getSaveActionLink(data);};
          break;
          case "deleteActionLink":
          result.columns[index].render = function (data) {return getDeleteActionLink(data);};
          break;
          case "TradeStatus":
          result.columns[index].render = function (data) {return parseTradeStatus(data);};
          break;
          case "TradeType":
          result.columns[index].render = function (data) {return parseTradeType(data);};
          break;
          default:
          delete result.columns[index].render;}
      switch (result.columns[index].format) 
        {
          case "Linked":
          result.columns[index].render = function (data, type, row) 
          {
            if(result.columns[index].name == "OpenDate" || result.columns[index].name == "CloseDate"
             || result.columns[index].name =="LastUpdateDate"){
                return getActionLink(parseDate(data, result.dateFormat));
            }
            else if(result.columns[index].name == "SymbolName"){
              return getActionLinkForSymbol(data, row);
            }
            else{
                return getActionLink(data);
            }
          };
          break;
          case "Money":
          result.columns[index].render = function (data, type, row) { return row.CurrencySymbol == null ? "" : row.CurrencySymbol + data.toFixed(result.moneyPrecision); };
          break;
          case "Percent":
          result.columns[index].render = function (data) {  return data.toFixed(result.percentyPrecision) + "%"};
          break;
          case "Date":
          result.columns[index].render = function (data) {return parseDate(data, result.dateFormat);};
          break;
          case "DateAndTime":
          result.columns[index].render = function (data) {return parseDateAndTime(data, result.dateFormat);};
          break;
        }
  });
$('#ddStatus').val(result.showPosition);
$('#ddStatus').selectpicker('refresh');
}

function MyAlert(message) {
    $("#errorDialogText").text(message);
    $("#alertErrorDialog").dialog("open");
}

function showClientError(propName, message) {
    if(propName.indexOf('|') == -1){
      showValidateError(propName, message)
      return;
    }
    var properties = propName.split('|');
    var messages = message.split('|');
    console.log(properties)
    for (var i = 0; i < properties.length -1; i++) {
      showValidateError(properties[i],messages[i]);
    };
}

function showValidateError(propName, message){
    $("#"+propName).removeClass("valid")
     .addClass( "input-validation-error" );
    $("#"+propName).next().removeClass("field-validation-valid")
    .addClass( "field-validation-error" );
    $( '<span for="'+propName+'" class="">'+message+'</span>' )
    .appendTo( $("#"+propName).next() );
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

function parseDate(data, dateFormat){
  if(data == null)
    return "";
  var subData = new Date(parseInt(data.substr(6)));
  switch (dateFormat) {
    case 0:
      return ((subData.getMonth() + 1) + "/" + subData.getDate()  + "/" + subData.getFullYear());
    case 1:
      return (subData.getDate()  + "/" + (subData.getMonth() + 1) + "/" + subData.getFullYear());
    case 2:
      return (subData.getDate() + " " +subData.toLocaleString("en-us", { month: "long" })+ " " +subData.getFullYear());
    default:
      return( ' ' );
  }
}

function parseDateAndTime(data, dateFormat){
  if(data == null)
    return "";
  var subData = new Date(parseInt(data.substr(6)));
  switch (dateFormat) {
    case 0:
      return ((subData.getMonth() + 1) + "/" + subData.getDate()  + "/" + subData.getFullYear() + " "+ subData.getHours() + ":" + subData.getMinutes() + ":" + subData.getSeconds());
    case 1:
      return (subData.getDate()  + "/" + (subData.getMonth() + 1) + "/" + subData.getFullYear() + " "+ subData.getHours() + ":" + subData.getMinutes() + ":" + subData.getSeconds());
    case 2:
      return (subData.getDate() + " " +subData.toLocaleString("en-us", { month: "long" })+ " " +subData.getFullYear() + " "+ subData.getHours() + ":" + subData.getMinutes() + ":" + subData.getSeconds());
    default:
      return( ' ' );
  }
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

function getActionLinkForSymbol(data, row){
    return '<a class="popup" href="/Position/ChartOfGain/'+ row.Id +'">'+ data +'</a>';
}

function getActionLink(data){
    return data != null ? '<a href="#">' + data +'</a>' : "";
}

function getSaveActionLink(data){
    return '<a class="popup" href="/Position/Save/' + data + '"><span class="glyphicon glyphicon-pencil" style="color: #80b78c;"></span></a>';
}

function getDeleteActionLink(data){
    return '<a class="popup" href="/Position/Delete/'+ data + '"><span class="glyphicon glyphicon-remove" style="color: #dc6c6c;"></span></a>';
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