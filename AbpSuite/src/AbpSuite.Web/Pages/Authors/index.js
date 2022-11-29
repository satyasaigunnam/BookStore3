$(function () {
    var l = abp.localization.getResource("AbpSuite");
	
	var authorService = window.abpSuite.authors.authors;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Authors/CreateModal",
        scriptUrl: "/Pages/Authors/createModal.js",
        modalClass: "authorCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Authors/EditModal",
        scriptUrl: "/Pages/Authors/editModal.js",
        modalClass: "authorEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            sureName: $("#SureNameFilter").val(),
			ageMin: $("#AgeFilterMin").val(),
			ageMax: $("#AgeFilterMax").val()
        };
    };

    var dataTable = $("#AuthorsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(authorService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('AbpSuite.Authors.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('AbpSuite.Authors.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    authorService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "sureName" },
			{ data: "age" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewAuthorButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        authorService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/authors/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'sureName', value: input.sureName },
                            { name: 'ageMin', value: input.ageMin },
                            { name: 'ageMax', value: input.ageMax }
                            ]);
                            
                    var downloadWindow = window.open(url, '_blank');
                    downloadWindow.focus();
            }
        )
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reload();
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reload();
    });
    
    
});
