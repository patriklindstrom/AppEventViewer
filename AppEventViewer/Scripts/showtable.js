


/* Formatted numbers sorting */
$.fn.dataTableExt.oSort['formatted-num-asc'] = function (x, y) {
	x = parseInt(x.replace(/[^\d\-\.\/]/g, ''));
	y = parseInt(y.replace(/[^\d\-\.\/]/g, ''));
	return x - y;
};
$.fn.dataTableExt.oSort['formatted-num-desc'] = function (x, y) {
	x = parseInt(x.replace(/[^\d\-\.\/]/g, ''));
	y = parseInt(y.replace(/[^\d\-\.\/]/g, ''));
	return y - x;
};

$.fn.dataTableExt.oSort['chinese-string-asc'] = function (s1, s2) {
	return s1.localeCompare(s2);
};
$.fn.dataTableExt.oSort['chinese-string-desc'] = function (s1, s2) {
	return s2.localeCompare(s1);
};
function activeFilter() {
	var aFilter = window.location.pathname;
	if (window.location.search) {
		aFilter += window.location.search;
	}
	return activeFilter;
}
function ajaxPostUrl() {
	var ajaxpurl = '/RestFull/srvc/getdcarrival' + window.location.pathname;
	if (window.location.search) {
		ajaxpurl += window.location.search;
	}
	return ajaxpurl;
}
function downLoadExcelUrl() {
	var dwnExcel = '/Api/DownloadFundsAsExcel/?host=';
	dwnExcel += window.location.host;
	return dwnExcel;
}


/* Main Mother */
/* ajax api/sqlobjects?searchterm=select&server=Herkules&instance=Default&database=Default */
function ResultDataTablesInit(tableDivId, colDefFn, colExcludeHide) {

    var oTable = $(tableDivId).dataTable({
        "bProcessing": true,
        "bStateSave": true,
        "sDom": 'T<"clear">RC<"clear">lfrtiSp',
        "aoColumnDefs": colDefFn(),
        "oColVis": {
            "bRestore": true,
            "buttonText":"Hide-Show",
            "sToolTip": "Show/Hide Columns"
            ,
            "aiExclude": colExcludeHide()
        },

					"oTableTools": {
						"aButtons": [{
							"sExtends": "text",
							"sButtonText": "",
							"sButtonClass": "DTTT_button_rco",
							"sButtonClassHover": "DTTT_button_rco_hover",
							"sToolTip": "Reset Column Order",
							"fnClick": function (nButton, oConfig) {
								ColReorder.fnReset(oTable);
								return false;
							}
						}
						,
						{
							"sExtends": "text",
							"sButtonText": "",
							"sButtonClass": "DTTT_button_xls",
							"sButtonClassHover": "DTTT_button_xls_hover",
							"sToolTip": "Save whole table as Excelsheet",
							"fnClick": function (nButton, oConfig) {
								window.open(downLoadExcelUrl(), "DownloadEventssAsExcel");
								return false;
							}
						}
						 , {
						     "sExtends": "print",
						     "sButtonText": "",
						     "sToolTip": "Print the tableview"					    
						 }]
					},
				"fnStateSaveParams": function (oSettings, oData) {
					oData.aoSearchCols = null;} ,
				"bPaginate": true,
				"bAutoWidth": true,
				"sPaginationType": "full_numbers",
				"iDisplayLength": 10, "aLengthMenu": [[10, 25, 50, 100, 400, -1], [10, 25, 50, 100, 400, "All"]]
		
	});  
}
