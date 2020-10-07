
$(document).ready(function(){
    $('form').attr('autocomplete', 'off');
	$('input[type=checkbox],input[type=radio],input[type=file]').uniform();
	
	$('select').select2();
	$('select.no-search').select2({ minimumResultsForSearch: Infinity });
	$('.datepicker').datepicker();
});
