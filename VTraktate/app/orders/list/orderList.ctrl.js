(function () {
    angular.module('order.list')  
    .controller('orderListController', orderListController)

    function orderListController($scope, jobsGridColumnDefs, appointDialog,
        orderTemplates, confirmer,
        jobParticipantEditDialog, jobsViewModelToGridTransformer, jobEditDialog, $http, constants, User, GlobalsService) {

        $scope.orderTemplates = orderTemplates.get();
       
        // set current office & getting options 
        $scope.currentOfficeId = User.currentOfficeId();
        $scope.canCreateOrders = function () { !isNaN($scope.currentOfficeId); };

        $scope.offices = GlobalsService.getAndInsertAll('offices');
        $scope.onCurrentOfficeChanged = function () {
            User.currentOfficeId($scope.currentOfficeId);
            getData();
        }

        $scope.appointAny = appointAny
        $scope.appointAnyOffice = appointAnyOffice;
        $scope.appointFromMyOffice = appointFromMyOffice;

        function appointAny(job) {
            appointDialog.open(job, { appointmentSource: 'any' }).success(getData);
        }
        function appointFromMyOffice(job) {
            appointDialog.open(job, { appointmentSource: 'myOffice' }).success(getData);
        }

        function appointAnyOffice(job) {
            appointDialog.open(job, { appointmentSource: 'office' }).success(getData);
        }

        $scope.editJob = editJob;
        $scope.cloneJob = cloneJob;
        $scope.editJobPart = editJobPart;
        $scope.getClass = function (entity, isRowHeader) {
            if (isRowHeader || entity.rowType === undefined)
                return null;
            var item = constants.JOB_GRID_CLASS_LOCATOR[entity.rowType];
            
            return item.prefix + ' ' + item.variable[entity.status.id];
        };
        var paginationOptions = { pageNumber: 1, pageSize : 25 };
        $scope.gridOptions = {
            rowTemplate: rowTemplate(),
            paginationPageSizes: [25, 50, 75],
            paginationPageSize: 25,
            useExternalPagination: true,
            useExternalSorting: true,
            useExternalFiltering: true,
            showTreeExpandNoChildren: false,
            data: [],
            columnDefs: jobsGridColumnDefs,
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
                $scope.gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                    paginationOptions.pageNumber = newPage;
                    paginationOptions.pageSize = pageSize;
                    getData();
                });
            }
        }
        
        function rowTemplate() {
            return '<div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.uid" class="ui-grid-cell" ng-class="[{ \'ui-grid-row-header-cell\': col.isRowHeader }, grid.appScope.getClass(row.entity, col.isRowHeader) ]"  ui-grid-cell></div>';
        }

        getData();

        function getData() {
            var param = $.param({ page: paginationOptions.pageNumber, count: paginationOptions.pageSize, filter: { officeId: $scope.currentOfficeId } });
            $http({
                method: 'GET',
                url: '/api/job?' + param

            }).success(function (data) { 
                var transformed = jobsViewModelToGridTransformer.transform(data.result);
                $scope.gridOptions.data = transformed;
                $scope.gridOptions.totalItems = data.total;
                // ISSUE : 
                //$scope.gridApi.treeBase.expandAllRows();
                // expand all rows: seems like my API doesn't allow expanding all rows 
            });
        }

        $scope.deleteJob = deleteJob;
        $scope.deleteJobPart = deleteJobPart;

        function deleteJob(job) {
            var orderDescr = job.order.name + ' (' + job.jobType.name + ')';
            if (confirmer.yes('Вы действительно хотите удалить вид работ ' + orderDescr + ' и всех его исполнителей?')) {
                var url = 'api/job/' + job.id;
                $http.delete(url).success(updateView).error(displayError);
            }
            function updateView() {
                toastr.success('Вид работ ' + orderDescr + ' успешно удален');
                getData();
            }
            function displayError() {
                toastr.error('Не удалось удалить вид работ ' + orderDescr + '. Возможно, некоторые из его исполнителей являются подразделениями, и созданы заказы по взаимозачету. Сначала удалите этих исполнителей.');
            }
        }

        function deleteJobPart(jobPart) {
            var providerName = jobPart.provider.name;
            if (confirmer.yes('Вы действительно хотите удалить исполнителя ' + providerName + '?')) {
                var url = 'api/jobPart/' + jobPart.id;
                $http.delete(url).success(updateView).error(displayError);
            }
            function updateView() {
                toastr.success('Исполнитель ' + providerName + ' успешно удален');
                getData();
            }
            function displayError() {
                toastr.error('Не удалось удалить исполнителя ' + providerName);
            }
        }

        $scope.isDaughterJob = function (entity) { return !!entity.delegatedToOfficeId; };

        $scope.setPending = function(job) { setJobStatus(job, constants.JOB_STATUSES.JOB_STATUS_PENDING); }
        $scope.setCompleted = function(job) { setJobStatus(job, constants.JOB_STATUSES.JOB_STATUS_COMPLETED); }
        $scope.setDelivered = function(job) { setJobStatus(job, constants.JOB_STATUSES.JOB_STATUS_DELIVERED); }
        $scope.setCanceled = function(job) { setJobStatus(job, constants.JOB_STATUSES.JOB_STATUS_CANCELED); }

        $scope.setJobPartPending = function (jobPart) { setJobPartStatus(jobPart, constants.JOB_PART_STATUSES.JOB_PART_STATUS_PENDING) };
        $scope.setJobPartCompleted = function (jobPart) { setJobPartStatus(jobPart, constants.JOB_PART_STATUSES.JOB_PART_STATUS_COMPLETED) };
        $scope.setJobPartCanceled = function (jobPart) { setJobPartStatus(jobPart, constants.JOB_PART_STATUSES.JOB_PART_STATUS_CANCELED) };
        
        function setJobStatus(entity, statusId) {
            var url = 'api/job/' + entity.id + '/status/' + statusId;
            $http.put(url).then(updateView, updateCanceled);
            function updateView() {
                toastr.success('Статус заказа обновлен');
                getData();
            }
            function updateCanceled(response) {
                console.log('status update canceled');
                console.log(response);
                toastr.error('Я ошибко');
            }
        }

        function setJobPartStatus(jobPart, newStatusId) {
            // we're assuming input is correct !!! 
            $http.put('api/jobPart/' + jobPart.id + '/status/' + newStatusId)
            .success(updateView)
            .error(displayError);
            function updateView() {
                toastr.success('Статус исполнителя изменен');
                getData();
            }
            function displayError() {
                toastr.error('Я ошибко');
            }
        }

        function editJobPart(jobPart) {
            jobParticipantEditDialog
            .open(jobPart)
            .then(updateView, updateCanceled);
            function updateView() {
                getData();
            }
            function updateCanceled() {
                toastr.info('Редактирование отменено')
            }
        }
        
        function cloneJob(entity) {
            jobEditDialog.open(entity, 'clone')
            then(updateView, updateCanceled);
            
            function updateView(data) {
                // should only update only one line in the grid, but for now let's update grid completely;
                getData(); // mechanically refreshes the grid ... 
            }

            function updateCanceled() {
                toastr.info('Редактирование отменено')
            }
        }
        function editJob(entity) {
            console.log('start edit job');
            jobEditDialog
                .open(entity)
                .then(updateView, updateCanceled);
            function updateView(data) {
                // should only update only one line in the grid, but for now let's update grid completely;
                getData(); // mechanically refreshes the grid ... 
            }

            function updateCanceled() {
                toastr.info('Редактирование отменено')
            }

        }
    }

})();