$(function () {
	$('#panelList').sortable({
		stop: function (event, ui) {
			portfolioRefresh();
			loadPortfolioRefresh(getPortfoliosIndex());
		}
	});
	$("#panelList").disableSelection();
});

function portfolioRefresh(){
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
