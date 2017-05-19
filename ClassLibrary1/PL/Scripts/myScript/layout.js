
$(function () {
    $('#panelList').sortable({
        stop: function (event, ui) {
            portfolioAllRefresh();
        }
    });
    $("#panelList").disableSelection();

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

});

var tradeManagementIndex;
var tableTradeManagement;

$(document).ready(function(){
        loadTradeManagement();
        tableTradeManagement =  $('#trade-management-jq-table')
        .on( 'processing.dt', function ( e, settings, processing ) 
            {$('#loader').css( 'display', processing ? 'block' : 'none' );})
        .DataTable({
            "processing": false,
            "serverSide": true,
            "filter": false,
            "orderMulti": false,
            "ajax": {
                "url": "/Nav/LoadData",
                "type": "POST",
                "datatype": "json",
                "data": function ( d ) {
                      $.extend(d, tradeManagementIndex);
                      d.id = tradeManagementIndex;
                      var dt_params = $('#trade-management-jq-table').data('dt_params');
                      if(dt_params){ $.extend(d, dt_params); }
                   },
                "error": function (xhr) {
                    alert(xhr.statusText);
                }
            },
            "columns": [
                    { "data": "Name", "name": "Name", "autoWidth": true },
                    { "data": "SymbolName", "name": "SymbolName", "autoWidth": true },
                    { "data": "OpenPrice", "name": "OpenPrice", "autoWidth": true },
                    { "data": "OpenDate", "name": "OpenDate", "autoWidth": true, 
                    "render": function (data) {return parseDateTime(data);} },
                    { "data": "OpenWeight", "name": "OpenWeight", "autoWidth": true },
                    { "data": "CurrentPrice", "name": "CurrentPrice", "autoWidth": true },
                    { "data": "TradeStatus", "name": "TradeStatus", "autoWidth": true , 
                    "render": function (data) {return parseTradeStatus(data);} },
                    { "data": "ClosePrice", "name": "ClosePrice", "autoWidth": true },
                    { "data": "CloseDate", "name": "CloseDate", "autoWidth": true, 
                    "render": function (data) {return parseDateTime(data);} },
                    { "data": "Gain", "name": "Gain", "autoWidth": true },
                    { "data": "AbsoluteGain", "name": "AbsoluteGain", "autoWidth": true },
                    { "data": "MaxGain", "name": "MaxGain", "autoWidth": true }
            ]
        });
});

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
            alert('error');
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
    var datet = new Date(parseInt(data.substr(6)));
    var newData = (datet.getMonth() + 1) + "/" + (datet.getDate() + 1) + "/" + datet.getFullYear();
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
