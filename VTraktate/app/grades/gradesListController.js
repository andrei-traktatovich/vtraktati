(function () {

    angular.module('grades')
    .controller('gradesListController', gradesListController)

    // TODO: tableParams to be resolved by the router to persist state? 
    function gradesListController(/*tableParams, */$scope, gradeDialog, gradesService, ngTableParams, defaultTableParams, confirmer, domainAddDialog) {
        var grades = this;

        var params = /*tableParams || */defaultTableParams.get();

        grades.tableParams = params;

        grades.add = add;
        grades.edit = edit;
        grades.remove = remove;
        grades.addDomain = addDomain;

        grades.toggleAdditionalFilters = toggleAdditionalFilters;
        grades.toggleStartDate = toggleStartDate;
        grades.toggleEndDate = toggleEndDate;

        function addDomain(providerName, languageName, languageInfoGradedId, domain) {

            var config = {
                providerName: providerName,
                languageName: languageName,
                languageInfoId: languageInfoGradedId,
                domain: domain
            };

            domainAddDialog.open(config)
            .then(updateView);

            function updateView() {
                grades.tableParams.reload();
                toastr.success('Тематика ' + domain.name + ' добавлена')
            }
        }

        function add(provider) {
            var providerName = provider && provider.name,
                providerKnown = !!provider,
                providerId = provider && provider.id;

            var config = {
                providerName: providerName,
                providerKnown: !!providerName
            };

            var model = gradesService.createModel({ providerId: providerId });

            gradeDialog.open(model, config)
            .then(updateView);

            function updateView(data) {
                /// NOT IMPLEMENTED ... 
                grades.tableParams.reload();
            }
        }

        function updateView() {
            grades.tableParams.reload();
        }

        function edit(item) {
            var config = {
                providerName: item.provider.name,
                providerKnown: true
            },
            model = gradesService.makeModel(item);
            console.log(model);
            gradeDialog.open(model, config)
            .then(updateView);
            function updateView(data) {
                // not implemented }
                grades.tableParams.reload();
            }
        }

        function remove(item) {
            if (confirmer.yes('')) {
                gradesService.remove(item.id)
                .then(updateView);

            }

            function updateView() {
                // not implemented 
                grades.tableParams.reload();
            }
        }

        function toggleAdditionalFilters() {
            if (!grades.useAdditionalFilters) {

                grades.tableParams.$params.filter.startDate = null;
                grades.tableParams.$params.filter.endDate = null;
                grades.additionaFiltersVisible = false;
                updateView();
            } else {
                grades.additionaFiltersVisible = true;
            }


        }

        function toggleStartDate() {
            if (!grades.showStartDate) {

                grades.tableParams.$params.filter.startDate = null;

                updateView();
            }
        }

        function toggleEndDate() {
            if (!grades.showEndDate) {

                grades.tableParams.$params.filter.endDate = null;

                updateView();
            }
        }


    }
})();