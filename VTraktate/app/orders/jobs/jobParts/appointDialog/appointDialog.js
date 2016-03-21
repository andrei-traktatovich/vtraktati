(function () {
    angular.module('order.list')
        .controller('appointDialogController', appointDialogController);

    function appointDialogController($scope, $http, model, config, GlobalsService, $modalInstance, constants, User) {

        $scope.editing = model.id; // if model has id, it is being edited. if no id then model is new, we're creating a jobpart.
        var currentOfficeId = User.currentOfficeId();
        $scope.isJobTypeLinguistic = false;
        // todo: change that!!! 
        $scope.onJobTypeChanged = function onJobTypeChanged($item, $model) {
            $scope.isJobTypeLinguistic = $item.isLinguistic;
        };

        $scope.copyFinalFromInitial = function () {
            $scope.model.final = angular.copy($scope.model.initial);
        }
        

        $scope.dateOptions = {
            startingDay: 1,
            showWeeks: false
        };

        $scope.view = {};
        if ($scope.editing) {
            $scope.view.providerName = config.providerName;
        }
        else {
            $scope.createMode = config.appointmentSource;
            if (config.appointmentSource === 'myOffice')
                getMyEmployees(currentOfficeId);
        }

        function getMyEmployees(officeId) {
            var params = {
                officeId: officeId
            };

            $http.get(
                'api/provider/autosuggest', { params: params }
                ).success(function (providers) {
                        $scope.myEmployees = providers.result.result;
                });
        }
        
        $scope.shouldShowFinals = shouldShowFinals;
        $scope.save = save;
        $scope.cancel = cancel;
        $scope.uomRequiresCharsAndWords = uomRequiresCharsAndWords;
        $scope.model = model;

        $scope.refreshProviders = refreshProviders;
        $scope.getProfile = getProfile;

        populateLists();

        function shouldShowFinals() {
            return $scope.editing; // temp!!!!
        }

        // obliterated? 
        //$scope.getBusyStatusName = function (statusId) {
        //    var status = _.findWhere($scope.lists.availabilityStatuses, { id: statusId });
        //    return status ? status.title : null;

        //}

        
        function getProfile($item, model) {
            $scope.view.providerName = $item.name;
            var params = {
                jobTypeId: $scope.model.jobTypeId,
                languageId: $scope.model.languageId
            }
            $http.get('api/provider/' + model + '/appointProfile', { params: params })
                .success(function (data) {
                    if (data) {
                        $scope.model.UOMId = data.uomId || $scope.model.UOMId;
                        $scope.model.currencyId = data.currencyId || $scope.model.currencyId;
                        $scope.model.initial.pricing.rate = data.minRate;

                        // also show some message ... 
                    }
                    else {
                        // nothing came from the server 
                    }
                });
        }

        function uomRequiresCharsAndWords() {
            // for now !!!
            return true;
        }

        function refreshProviders(providerNameSubstring) {

            var params = {
                search: providerNameSubstring,
                jobTypeId: $scope.showAllJobTypes ? null : $scope.model.jobTypeId,
                languageId: $scope.showAllLanguages ? null : $scope.model.languageId,
                domain1Id: $scope.showAllDomains ? null : $scope.model.domain1Id,
                domain2Id: $scope.showAllDomains ? null : $scope.model.domain2Id
            };

            return $http.get(
                'api/provider/autosuggest', { params: params }
                ).success(function (providers) {
                    $scope.providers = providers.result.result;
                });
        }

        function populateLists() {
            $scope.lists = {
                officeProviders: GlobalsService.get('officeProviders'),
                jobTypes: GlobalsService.get('jobTypes'),
                languages: GlobalsService.get('languages'),
                availabilityStatuses: GlobalsService.get('availabilityStatuses'),
                currencies: GlobalsService.get('currencies'),
                uoms: GlobalsService.get('serviceUoms'),
            };
        }

        function save() {
            if (!$scope.editing) {
                var url = 'api/job/' + $scope.model.jobId + '/jobPart',
                    data = $scope.model;

                $http.post(url, data).success(closeDialog).error(displayError);
            }
            else {
                var url = 'api/jobPart/' + $scope.model.id;
                $http.put(url, $scope.model).success(closeDialog).error(displayError);
            }
            function closeDialog() {
                $modalInstance.close();
            }
        }

        function cancel() {
            $modalInstance.dismiss();
        }

    }

    

    

})();