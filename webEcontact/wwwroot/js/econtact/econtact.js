/**declarando mi variable global */
//var carpetaDirectorioGlobal = '/Banbif_Web_v2';

function DataTableBasico(IdTableBasico) {
	var vIdTableBasico = IdTableBasico;
	$('#' + vIdTableBasico).dataTable({
		// Layout para poner el selector y los botones abajo a la izquierda
		language: {
			"decimal": "",
			"emptyTable": "No hay información",
			"info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
			"infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
			"infoFiltered": "(Filtrado de _MAX_ total entradas)",
			"infoPostFix": "",
			"thousands": ",",
			"lengthMenu": "Mostrar _MENU_ entradas",
			"loadingRecords": "Cargando...",
			"processing": "Procesando...",
			"search": "Buscar:",
			"zeroRecords": "Sin resultados encontrados",
			"paginate": {
				"first": "Primero",
				"last": "Ultimo",
				"next": "Siguiente",
				"previous": "Anterior"
			}
		},
		destroy: true,
		retrieve: true,
		responsive: true,

		// 👇 DOM personalizado: Botones y selector de filas arriba, info y paginación abajo
		dom: '<"row"<"col-sm-12 col-md-8"B><"col-sm-12 col-md-4"l>>' +
			'<"row"<"col-sm-12 col-md-6"f>>' +
			'<"row"<"col-sm-12"tr>>' +
			'<"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',

		// 👇 Configuración del selector de filas por página
		lengthMenu: [
			[10, 25, 50, 100, -1],
			[10, 25, 50, 100, "Todos"]
		],
		pageLength: 10, // Número de filas por defecto

		buttons: [
			{
				extend: 'excelHtml5',
				text: '<i class="fa fa-file-excel"></i> Exportar a Excel',
				className: 'btn btn-success'
			},
			{
				extend: 'csvHtml5',
				text: '<i class="fa fa-file-csv"></i> Exportar a CSV',
				className: 'btn btn-primary'
			},
			{
				extend: 'pdfHtml5',
				text: '<i class="fa fa-file-pdf"></i> Exportar a PDF',
				className: 'btn btn-danger'
			},
			{
				extend: 'print',
				text: '<i class="fa fa-print"></i> Imprimir',
				className: 'btn btn-secondary'
			}
		]
	});
}

//// daTatable configuration DataTable con CheckBox
function DataTableCheckBox(IdTableCheckBox) {

	var vIdTable = IdTableCheckBox;

	// datatable configuration
	var vDatatable =  $('#' + vIdTable).DataTable({
		language: {
			"decimal": "",
			"emptyTable": "No hay información",
			"info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
			"infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
			"infoFiltered": "(Filtrado de _MAX_ total entradas)",
			"infoPostFix": "",
			"thousands": ",",
			"lengthMenu": "Mostrar _MENU_ Entradas",
			"loadingRecords": "Cargando...",
			"processing": "Procesando...",
			"search": "Buscar:",
			"zeroRecords": "Sin resultados encontrados",
			"paginate": {
				"first": "Primero",
				"last": "Ultimo",
				"next": "Siguiente",
				"previous": "Anterior"
			}
		},
		responsive: true,
		
		//'columnDefs': [
		//	{
		//		'targets': 0,
		//		'type': "checkbox",
		//		'checkboxes': {
		//			'selectRow': true
		//		}
		//	}
		//],
		//'select': {
		//	'style': 'multi'
		//},
		destroy: true,
		retrieve: true

	});

	return vDatatable;
}