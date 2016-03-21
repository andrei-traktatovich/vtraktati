// ACHTUNG: possible bracketing issue 

(function () {
    angular.module('orders')
        .controller('jobLineController', jobLineController);

    function jobLineController($scope, jobValidation, documents, GlobalsService, jobTypesManager, pricelistManager) {
        $scope.orderOptions = $scope.orderOptions || {};

        $scope.jobStatuses = GlobalsService.get('jobCompletionStatuses');
        $scope.jobTypes = $scope.jobTypes || GlobalsService.get('jobTypes');
        $scope.domains = GlobalsService.get('domains');
        $scope.languages = $scope.languages || GlobalsService.get('languages');

        $scope.showFinals = function () {
            if($scope.job && $scope.job.statusId)
                return jobValidation.statusRequiresFinals($scope.job.statusId);
        }

        $scope.copyInitialToFinal = function () {
            $scope.job.final = $scope.job.initial;
        }

        $scope.getDocAsync = documents.getAsync;

        $scope.isJobTypeLinguistic = function () {
            var result = jobTypesManager.isJobTypeLinguistic($scope.job.jobTypeId);
            return result;
        };
        $scope.onJobTypeChanged = function ($item, $model) {
            updateRate();
        }

        $scope.onLanguageChanged = function ($item, $model) {
            updateRate();
        }

        function updateRate() {
            pricelistManager.getPrice($scope.job.customerId, $scope.job.jobTypeId, $scope.job.languageId).then(success, fail);
            function success(result) {
                // more complex logic will be needed ?? 
                $scope.job.initial.pricing.rate = result;
                if ($scope.job.final) {
                    $scope.job.final.pricing.rate = result;
                }
                // AChtung: now that I moved this functionality into a 
                // separate directive, will it understand ? 
                //updateInitialPrice();
                //updateFinalPrice();
            }
            function fail() {
                // nope 
            }
        }
    }

})();