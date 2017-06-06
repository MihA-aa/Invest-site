
$(document).ready(function(){
	getNameOfTemplate(columnTampleteIndex);
    var curPos = $("#ColumnNameValue [selected='selected']").attr('value')
    if (curPos != undefined){
        getFormatsForColumn(curPos)
    }
})

$(".selectpicker").selectpicker({
    "title": "Select Options"
}).selectpicker("render");

function getNameOfTemplate(id){
    $.ajax({
        type: 'POST',
        url: '/Nav/GetNameByTemplateId',
        data: { "id": id},
        success: function (data) {
        	$("#ViewTemplate").val(data.templateName);
         },
    });
}

function columnNameCheck(that) {
    $("#TextBoxForColumnName").val(that.value);
    getFormatsForColumn(that.value);
    $("#DropDownListFormats option:first").attr('selected','selected');
    $('#DropDownListFormats').selectpicker('refresh');
}

function getFormatsForColumn(column){
    $.ajax({
        type: 'POST',
        url: '/Nav/GetFormatsForColumn',
        data: { "column": column},
        success: function (data) {
			var option = '';
			for (var i=0;i<data.columnFormats.length;i++){
			   option += '<option value="'+ data.columnFormats[i].Id + '">' + data.columnFormats[i].Name + '</option>';
			}
        	$("select#DropDownListFormats").empty();
			$('select#DropDownListFormats').append(option);
			$('#DropDownListFormats').selectpicker('refresh');
         },
    });
}