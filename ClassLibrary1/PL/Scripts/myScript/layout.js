
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
        tableTradeManagement =  $('#trade-management-jq-table').DataTable({
            "ajax": {
                "url": "/Nav/TradeManagementTable",
                "type": "GET",
                "datatype": "json",
                "bProcessing": true,
                "data": buildSearchData,
                "error": function (xhr) {
                    alert(xhr.statusText);
                }
            },
            "columns": [
                    { "data": "Name", "autoWidth": true },
                    { "data": "SymbolName", "autoWidth": true },
                    { "data": "OpenPrice", "autoWidth": true },
                    { "data": "OpenDate", "autoWidth": true },
                    { "data": "OpenWeight", "autoWidth": true },
                    { "data": "CurrentPrice", "autoWidth": true },
                    { "data": "TradeStatus", "autoWidth": true },
                    { "data": "ClosePrice", "autoWidth": true },
                    { "data": "CloseDate", "autoWidth": true },
                    { "data": "Gain", "autoWidth": true },
                    { "data": "AbsoluteGain", "autoWidth": true },
                    { "data": "MaxGain", "autoWidth": true }
            ]
        });
});

function buildSearchData(){
    return {"id" : tradeManagementIndex};
}

function ReloadTable(){
    tableTradeManagement.clear();
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
    $.ajax({
        type: 'POST',
        url: '/Nav/RefreshPortfolioDisplayIndex',
        data: { "portfolios": portfolios}
    });
}
